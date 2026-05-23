using System.Collections;
using System.Linq;
using TurnBasedRPG.BattleStats;
using UnityEngine;

namespace TurnBasedRPG {
    public interface ICombatPhase {
        public IEnumerator Execute(CombatManager cm);
    }

    public class SetupPhase : ICombatPhase {
        public IEnumerator Execute(CombatManager cm) {
            cm.MaxSpeed = cm.Everyone.Max(c => c[Stat.Speed]);
            cm.TurnCount = 1;
            yield break;
        }
    }

    public class WaitActionPhase : ICombatPhase {
        public IEnumerator Execute(CombatManager cm) {
            cm.CurrentCharacter = null;
            var fastest = cm.Everyone.First();
            while (cm.CurrentCharacter == null) {
                yield return null;
                if(cm.Pause) continue;

                foreach (var character in cm.Everyone) {
                    if(character.Health.Current == 0) continue;
                    character.actionPoints.Value += character[Stat.Speed] * Time.deltaTime;
                    if(fastest.actionPoints.Value < character.actionPoints.Value)
                        fastest = character;
                }

                if(fastest.actionPoints.Value >= cm.MaxSpeed)
                    cm.CurrentCharacter = fastest;
            }
        }
    }

    public class CharacterPhase : ICombatPhase {
        private EnemyBehaviour _enemyBehaviour;

        public CharacterPhase(EnemyBehaviour enemyBehaviour) {
            _enemyBehaviour = enemyBehaviour;
        }

        public IEnumerator Execute(CombatManager cm) {
            var character = cm.CurrentCharacter;
            cm.View.skillPanel.gameObject.SetActive(false);
            cm.View.consumablesView.gameObject.SetActive(false);
            cm.SelectedAction = cm.WaitFlag;
            character.OnStartTurn?.Invoke(character);

            var isDead = character.Health.Current == 0;
            if(isDead || character.Stun) {
                character.OnEndTurn?.Invoke(character);
                yield break;
            }

            if(cm.Enemies.Contains(character)) {
                //Vez do Inimigo
                yield return _enemyBehaviour.EnemyTurn(cm);
            }
            else {
                //Vez do Player
                Debug.Log("Player Turn: " + character.characterName);
                cm.View.combatPanel.SetActive(true);
                cm.View.ShowSkills(character);
                yield return new WaitUntil(() => cm.SelectedAction != cm.WaitFlag);
            }

            yield return ExecuteSelectedSkill(cm);
            character.OnEndTurn?.Invoke(character);
            character.actionPoints.Value -= cm.MaxSpeed;
        }

        private IEnumerator ExecuteSelectedSkill(CombatManager cm) {
            if(cm.SelectedAction == null) yield break;
            if(cm.SelectedAction == cm.WaitFlag) yield break;
            var a = cm.SelectedAction;
            var affected = a.Skill.targeting.GetAffectedTargets(a.User, a.Target);

            // Prepare UI
            cm.View.selectTargetPanel.SetActive(false);
            cm.View.combatPanel.SetActive(false);
            cm.View.actionsPanel.SetActive(false);
            cm.View.ShowCurrentAction(a.User.characterName, a.Skill.skillName);
            cm.Models.ClearTargets();
            cm.Models.TargetCharacters(affected);
            Debug.Log("Target Selected: " + a.Target?.characterName);

            // Use Skill
            a.User.Mana.Current -= a.Skill.cost;
            var usedSlot = cm.Consumables.slots.FirstOrDefault(s => s.item.skillEffect == a.Skill);
            if(usedSlot != null) cm.Consumables.Remove(usedSlot.item, 1);
            yield return a.Skill.UseSkill(a.User, a.Target);
        }
    }

    public class CombatEvents : ICombatPhase {
        public IEnumerator Execute(CombatManager cm) {
            cm.Models.ClearTargets();
            while (cm.CombatEvents.TryDequeue(out var ie))
                yield return ie;
            cm.ActionFlags.Clear();
        }
    }

    public class CheckResultPhase : ICombatPhase {
        public IEnumerator Execute(CombatManager cm) {
            var win = cm.Enemies.All(c => c.Dead);
            var lose = cm.Allies.All(c => c.Dead);
            if(!win && !lose) yield break;

            Debug.Log("Combat Ended");
            cm.View.gameOverPanel.SetActive(true);
            cm.FinishCombat(!lose);
        }
    }
}