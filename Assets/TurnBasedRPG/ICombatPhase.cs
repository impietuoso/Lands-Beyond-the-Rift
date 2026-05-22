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
            cm.maxSpeed = cm.everyone.Max(c => c[Stat.Speed]);
            cm.turnCount = 1;
            yield break;
        }
    }

    public class WaitActionPhase : ICombatPhase {
        public IEnumerator Execute(CombatManager cm) {
            cm.currentCharacter = null;
            var fastest = cm.everyone.First();
            while (cm.currentCharacter == null) {
                foreach (var character in cm.everyone) {
                    if(character.Health.Current == 0) continue;
                    character.actionPoints.Value += character[Stat.Speed] * Time.deltaTime;
                    if(fastest.actionPoints.Value < character.actionPoints.Value)
                        fastest = character;
                }

                if(fastest.actionPoints.Value >= cm.maxSpeed)
                    cm.currentCharacter = fastest;

                yield return null;
            }
        }
    }

    public class CharacterPhase : ICombatPhase {
        public IEnumerator Execute(CombatManager cm) {
            var character = cm.currentCharacter;
            cm.view.skillPanel.gameObject.SetActive(false);
            cm.selectedAction = cm.WaitFlag;
            character.OnStartTurn?.Invoke(character);

            var isDead = character.Health.Current == 0;
            if(isDead || character.Stun) {
                character.OnEndTurn?.Invoke(character);
                yield break;
            }

            if(cm.enemies.Contains(character)) {
                //Vez do Inimigo
                yield return cm.enemyBehaviour.EnemyTurn(cm);
            }
            else {
                //Vez do Player
                Debug.Log("Player Turn: " + character.characterName);
                cm.view.actionsPanel.SetActive(true);
                cm.view.ShowSkills(character);
                yield return new WaitUntil(() => cm.selectedAction != cm.WaitFlag);
            }

            yield return ExecuteSelectedSkill(cm);
            character.OnEndTurn?.Invoke(character);
            character.actionPoints.Value -= cm.maxSpeed;
        }

        private IEnumerator ExecuteSelectedSkill(CombatManager cm) {
            if(cm.selectedAction == null) yield break;
            if(cm.selectedAction == cm.WaitFlag) yield break;
            var a = cm.selectedAction;
            var affected = a.Skill.targeting.GetAffectedTargets(a.User, a.Target);

            // Prepare UI
            cm.view.selectTargetPanel.SetActive(false);
            cm.view.actionsPanel.SetActive(false);
            cm.view.ShowCurrentAction(a.User.characterName, a.Skill.skillName);
            cm.models.ClearTargets();
            cm.models.TargetCharacters(affected);
            Debug.Log("Target Selected: " + a.Target?.characterName);

            // Use Skill
            a.User.Mana.Current -= a.Skill.cost;
            var usedSlot = cm.consumables.slots.FirstOrDefault(s => s.item.skillEffect == a.Skill);
            if(usedSlot != null) cm.consumables.Remove(usedSlot.item, 1);
            yield return a.Skill.UseSkill(a.User, a.Target);
        }
    }

    public class CombatEvents : ICombatPhase {
        public IEnumerator Execute(CombatManager cm) {
            cm.models.ClearTargets();
            while (cm.combatEvents.TryDequeue(out var ie))
                yield return ie;
            cm.actionFlags.Clear();
        }
    }

    public class CheckResultPhase : ICombatPhase {
        public IEnumerator Execute(CombatManager cm) {
            var win = cm.enemies.All(c => c.Dead);
            var lose = cm.allies.All(c => c.Dead);
            if(!win && !lose) yield break;

            Debug.Log("Combat Ended");
            cm.view.gameOverPanel.SetActive(true);
            cm.FinishCombat(!lose);
        }
    }
}