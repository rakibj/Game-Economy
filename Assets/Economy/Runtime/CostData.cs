using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameloops.Economy
{
    [Serializable]
    public struct LevelCost
    {
        public int level;
        public float cost;
        public int Level => level;
        public float Cost => cost;

        public LevelCost(int level, float cost)
        {
            this.level = level;
            this.cost = cost;
        }
    }
    
    [CreateAssetMenu(menuName = "Gameloops/Game Economy/New Cost Data")]
    public class CostData : ScriptableObject
    {
        [SerializeField] private Vector2Int levels = new Vector2Int(1, 20);
        [SerializeField] private AnimationCurve costPerLevelCurve = new AnimationCurve();
        [SerializeField] private CostCalculator costCalculator;
        [SerializeField] private List<LevelCost> levelCosts = new List<LevelCost>();
        public CostCalculator CostCalculator => costCalculator;
        public Vector2Int Levels => levels;
        public AnimationCurve CostPerLevelCurve => costPerLevelCurve;
        public List<LevelCost> LevelCosts => levelCosts;

        [ContextMenu("UseCalculator")]
        public void UseCalculator()
        {
            levelCosts.Clear();
            levelCosts = costCalculator.GetLevelCosts(levels);
            SetCurveFromCosts();
        }

        [ContextMenu("UseCurve")]
        public void UseCurve()
        {
            levelCosts.Clear();
            for (int i = levels.x; i <= levels.y; i++)
            {
                var cost = Mathf.CeilToInt(costPerLevelCurve.Evaluate(i));
                levelCosts.Add(new LevelCost(i, cost));
            }
            SetCurveFromCosts();
        }
        
        [ContextMenu("SaveCurrent")]
        public void SaveCurrent()
        {
            SetCurveFromCosts();
        }

        public bool HasCostOfLevel(int level)
        {
            if (levels.y < level)
            {
                Debug.LogError("Trying to get cost of level that is less than the max defined level on CostData");
                return false;
            }

            if (!levelCosts.Exists(x => x.level == level))
            {
                Debug.LogError("The specified level is not found on CostData");
                return false;
            }
            return true;
        }
        
        public float GetCostOfLevel(int level)
        {
            return levelCosts.First(x => x.level == level).cost;
        }

        private void SetCurveFromCosts()
        {
            costPerLevelCurve.keys = null;
            foreach (var levelCost in levelCosts)
            {
                costPerLevelCurve.AddKey(levelCost.level, levelCost.cost);
            }
        }
    }
}