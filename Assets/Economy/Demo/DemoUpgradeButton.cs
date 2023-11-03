using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameloops.Economy
{
    public class DemoUpgradeButton : MonoBehaviour
    {
        [SerializeField] private CostData costData;
        [SerializeField] private TMP_Text levelLabel;
        [SerializeField] private TMP_Text costLabel;
        [SerializeField] private TMP_Text logLabel;
        [SerializeField] private Button upgradeButton;
        private int _currentLevel = 0;
        private float _costOfNextLevel;

        private void Start()
        {
            logLabel.text = "";
            UpdateUI();
        }

        private void UpdateUI()
        {
            levelLabel.text = "Level " + _currentLevel;
            var nextLevel = _currentLevel + 1;
            if (costData.HasCostOfLevel(nextLevel))
            {
                _costOfNextLevel = costData.GetCostOfLevel(nextLevel);
                costLabel.text = _costOfNextLevel + "Coins";
            }
            else
            {
                upgradeButton.gameObject.SetActive(false);
                logLabel.text = "Can't find the cost!";
            }
        }

        private void OnEnable()
        {
            upgradeButton.onClick.AddListener(ClickUpgrade);
        }

        private void OnDisable()
        {
            upgradeButton.onClick.RemoveListener(ClickUpgrade);
        }

        private void ClickUpgrade()
        {
            _currentLevel++;
            logLabel.text = $"Successful upgrade to level {_currentLevel} using cost {Mathf.CeilToInt(_costOfNextLevel)}";
            UpdateUI();
        }
    }
}