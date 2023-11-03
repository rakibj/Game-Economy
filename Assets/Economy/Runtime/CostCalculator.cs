using System.Collections.Generic;
using UnityEngine;

namespace Gameloops.Economy
{
    //[CreateAssetMenu(menuName = "Gameloops/Game Economy/Cost Calculator/")]
    public abstract class CostCalculator : ScriptableObject
    {
        public abstract List<LevelCost> GetLevelCosts(Vector2Int levels);
    }
}