using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Gameloops.Economy
{
    [CustomEditor(typeof(CostData))]
    public class CostDataEditor: Editor
    {
        private Vector2 _scroll;
        private Color _defaultBackgroundColor = new Color(0.278f, 0.278f, 0.278f, .5f);

        public override void OnInspectorGUI()
        {
            var costData = (CostData)target;
            
            base.OnInspectorGUI();

             if (costData.CostCalculator == null)
             {
                 EditorGUILayout.HelpBox("Add a cost calculator to initialize values", MessageType.Warning);
                 return;
             }
            
             if (costData.LevelCosts.Count == 0)
             {
                 if(GUILayout.Button("Initialize")) costData.UseCalculator();
                 return;
             }
            
             EditorGUILayout.Space();
             EditorGUILayout.LabelField("Cost Values");
             _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(300));
            
             GUI.backgroundColor = Color.gray;
             var allCostsMatch = true;
             for (var i = costData.Levels.x; i <= costData.Levels.y; i++)
             {
                 var costOnArray = Mathf.CeilToInt(costData.LevelCosts.FirstOrDefault(_ => _.level == i).cost);
                 var costOnCurve = Mathf.CeilToInt(costData.CostPerLevelCurve.Evaluate(i));
                 var costMatch = costOnArray == costOnCurve;
                 if (!costMatch) allCostsMatch = false;
                 GUI.backgroundColor = costMatch ? _defaultBackgroundColor : Color.red;
            
                 EditorGUILayout.BeginHorizontal("box");
                 EditorGUILayout.LabelField("Level " + i);
                 var cost = EditorGUILayout.DelayedFloatField(costMatch ? costOnCurve : costOnArray);
                 if (Mathf.CeilToInt(cost) != costOnArray)
                 {
                     var levelCost = costData.LevelCosts.FirstOrDefault(_ => _.level == i);
                     levelCost.cost = cost;
                     for (var j = 0; j < costData.LevelCosts.Count; j++)
                     {
                         if (costData.LevelCosts[j].level == levelCost.level)
                             costData.LevelCosts[j] = new LevelCost(levelCost.level, levelCost.cost);
                     }

                     break;
                 }
                 //EditorGUILayout.LabelField(costMatch ? costOnCurve.ToString() : costOnArray + " -> " + costOnCurve);
                 EditorGUILayout.EndHorizontal();
             }
             GUI.backgroundColor = _defaultBackgroundColor;
             EditorGUILayout.EndScrollView();
            
             EditorGUILayout.BeginHorizontal();
            
             if (!allCostsMatch)
             {
                 if (GUILayout.Button("Save")) costData.SaveCurrent();
             }
             else
             {
                 var calculatorDataChanged = false;
                 var list = costData.CostCalculator.GetLevelCosts(costData.Levels);
                 for (var i = 0; i < list.Count; i++)
                 {
                     if (Math.Abs(list[i].cost - costData.LevelCosts[i].cost) != 0f)
                     {
                         calculatorDataChanged = true;
                         break;
                     }
                 }
            
                 if (calculatorDataChanged)
                 {
                     EditorGUILayout.BeginVertical();
                     EditorGUILayout.HelpBox("Current Cost Data does not match the Cost Calculator Data which is fine if it's intended", MessageType.Warning);
                     if (GUILayout.Button("Restore Original"))
                     {
                         costData.UseCalculator();
                         EditorUtility.SetDirty(costData);
                     }
                     EditorGUILayout.EndVertical();
                 }
             }
            
             EditorGUILayout.EndHorizontal();
        }
    }
}