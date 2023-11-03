using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameloops.Economy
{
    [CreateAssetMenu(menuName = "Gameloops/Game Economy/Cost Calculator/Round")]
    public class CostCalculateRound : CostCalculator
    {
        [SerializeField] private CostCalculator costCalculator;
        [SerializeField] private int digitPlacesToRoundOffFrom = 2;
        public override List<LevelCost> GetLevelCosts(Vector2Int levels)
        {
            var levelCosts = costCalculator.GetLevelCosts(levels);
            var newLevelCosts = levelCosts.ToList();
            
            for (var i = 0; i < levelCosts.Count; i++)
            {
                var levelCost = levelCosts[i];
                var newCost = RoundOffValue(levelCost.cost);
                newLevelCosts[i] = new LevelCost(levelCosts[i].level, newCost);
            }

            return newLevelCosts;
        }

        private float RoundOffValue(float value)
        {
            //Count the numbers in the digit
            int digitsCount = (int)Mathf.Floor(Mathf.Log10(value) + 1);

            var digitPlaces = Mathf.Pow(10, (digitsCount - digitPlacesToRoundOffFrom+1));
            var dividedValue = value / digitPlaces;
            var scaledDownValue = Mathf.Round(dividedValue);
            var scaledUpValue = scaledDownValue * digitPlaces;

            return scaledUpValue;
        }
    }
}