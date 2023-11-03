using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameloops.Economy
{
    [CreateAssetMenu(menuName = "Gameloops/Game Economy/Cost Calculator/Relative Linear")]
    public class CostCalculatorRelativeLinear : CostCalculator
    {
        [SerializeField] private int levelOffset = 14;
        [SerializeField] private CostCalculator source;
        
        public override List<LevelCost> GetLevelCosts(Vector2Int levels)
        {
            var sourceCosts = source.GetLevelCosts(new Vector2Int(levels.x, levels.y + levelOffset));
            var levelCosts = new List<LevelCost>();
            for (int i = levels.x; i <= levels.y; i++)
            {
                var cost = Mathf.CeilToInt(sourceCosts.FirstOrDefault(_ => _.level == i+levelOffset).cost);
                levelCosts.Add(new LevelCost(i, cost));
            }

            return levelCosts;
        }
    }
}