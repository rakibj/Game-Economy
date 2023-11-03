using System.Collections.Generic;
using UnityEngine;

namespace Gameloops.Economy
{
    [CreateAssetMenu(menuName = "Gameloops/Game Economy/Cost Calculator/Soft")]
    public class CostCalculatorSoftStart : CostCalculator
    {
        [SerializeField] private float baseCost = 20f;
        [SerializeField] private float friction = 0.2f;
        public override List<LevelCost> GetLevelCosts(Vector2Int levels)
        {
            var levelCosts = new List<LevelCost>();
            var lastCost = 0f;
            var index = 0;
            for (var level = levels.x; level <= levels.y; level++)
            {
                var cost = Mathf.CeilToInt(Mathf.Pow(baseCost, friction * index+1) + lastCost);
                levelCosts.Add(new LevelCost(level, cost));
                lastCost = cost;
                index++;
            }


            return levelCosts;
        }
    }
}