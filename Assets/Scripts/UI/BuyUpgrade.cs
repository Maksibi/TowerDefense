using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private TextMeshProUGUI levelText, costText;
        [SerializeField] private Image upgradeIcon;
        [SerializeField] private Button upgradeButton;

        private int upgradeCost;

        public void Initialize()
        {
            upgradeIcon.sprite = asset.sprite;
            int savedLevel = Upgrades.GetUpgradesLevel(asset);
            if (savedLevel >= asset.costByLevel.Length)
            {
                levelText.text = "Lvl: Max";
                upgradeButton.interactable = false;

                upgradeButton.transform.Find("Image").gameObject.SetActive(false);
                upgradeButton.transform.Find("Cost").gameObject.SetActive(false);
                costText.text = "X";
            }
            else
            {
                upgradeCost = asset.costByLevel[savedLevel];
                costText.text = "Buy: " + upgradeCost.ToString();
                levelText.text = "Lvl: " + (savedLevel + 1).ToString();
            }
        }
        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }

        internal void CheckCost(int money)
        {
            upgradeButton.interactable = money >= upgradeCost;
        }
    }
}