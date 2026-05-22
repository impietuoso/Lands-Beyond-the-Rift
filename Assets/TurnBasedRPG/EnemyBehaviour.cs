using System.Collections;
using System.Linq;
using TurnBasedRPG.Data;
using UnityEngine;

namespace TurnBasedRPG {
    public class EnemyBehaviour {
        private int GetWeightedRandomIndex(float[] weights) {
            var totalWeight = weights.Sum();
            var randomValue = Random.Range(0, totalWeight);
            float currentSum = 0;

            for (var i = 0; i < weights.Length; i++) {
                currentSum += weights[i];
                if(randomValue <= currentSum) {
                    return i;
                }
            }

            return 0;
        }

        public IEnumerator EnemyTurn(CombatManager cm) {
            var currentEnemy = cm.currentCharacter;
            Debug.Log("Enemy Turn: " + currentEnemy.characterName);
            yield return new WaitForSeconds(0.25f);

            var availableSkills = currentEnemy.skills.Where(s => s.Available(currentEnemy)).ToList();
            Skill selectedSkill;

            if(availableSkills.Count > 0) {
                // Usa Skill (60%), Ataca (30%) ou Defende (10%)
                var choice = GetWeightedRandomIndex(new float[] { 60, 30, 10 });
                if(choice == 0)
                    selectedSkill = availableSkills[Random.Range(0, availableSkills.Count)];
                else if(choice == 1)
                    selectedSkill = currentEnemy.basicAttack;
                else
                    selectedSkill = cm.basicDefense;
            }
            else {
                // Ataca (80%) ou Defende (20%)
                var choice = GetWeightedRandomIndex(new float[] { 80, 20 });
                var attack = currentEnemy.basicAttack;
                selectedSkill = choice == 0 ? attack : cm.basicDefense;
            }

            yield return new WaitForSeconds(.25f);
            var eligible = selectedSkill.targeting.EligibleTargets(currentEnemy).ToArray();
            var target = eligible[Random.Range(0, eligible.Length)];
            cm.selectedAction = new()
            {
                Skill = selectedSkill,
                User = currentEnemy,
                Target = target,
            };
        }
    }
}