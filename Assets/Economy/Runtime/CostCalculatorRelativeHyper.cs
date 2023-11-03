using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameloops.Economy
{
    [CreateAssetMenu(menuName = "Gameloops/Game Economy/Cost Calculator/Relative Hyper")]
    public class CostCalculatorRelativeHyper : CostCalculator
    {
        [SerializeField] private float friction = 0.2f;
        [SerializeField] private int levelOffset = 14;
        [SerializeField] private CostCalculator source;
        
        public override List<LevelCost> GetLevelCosts(Vector2Int levels)
        {
            var sourceCosts = source.GetLevelCosts(new Vector2Int(levels.x, levels.y + levelOffset));
            var levelCosts = new List<LevelCost>();
            var lastCost = 0;
            
            for (int i = levels.x; i <= levels.y; i++)
            {
                var cost = Mathf.CeilToInt(sourceCosts.FirstOrDefault(_ => _.level == i+levelOffset).cost);
                cost = Mathf.CeilToInt(Mathf.Pow(cost, friction) + lastCost);
                levelCosts.Add(new LevelCost(i, cost));
                lastCost = cost;
            }

            return levelCosts;
        }
    }
}