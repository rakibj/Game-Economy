using System.Collections.Generic;
using UnityEngine;

namespace Gameloops.Economy
{
    [CreateAssetMenu(menuName = "Gameloops/Game Economy/Cost Calculator/Exponential")]
    public class CostCalculatorExponential : CostCalculator
    {
        [SerializeField] private float baseCost = 20f;
        [SerializeField] private float friction = 0.2f;
        public override List<LevelCost> GetLevelCosts(Vector2Int levels)
        {
            var levelCosts = new List<LevelCost>();
            var index = 0;
            for (int level = levels.x; level <= levels.y; level++)
            {
                var cost = Mathf.CeilToInt(baseCost * Mathf.Exp(friction * index));
                levelCosts.Add(new LevelCost(level, cost));
                index++;
            }

            return levelCosts;
        }
    }
}