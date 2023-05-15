using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [Serializable]
        private class UpgradeSlot
        {
            public BuyUpgrade slot;
            public UpgradeAsset upgrade;

            public void AssignUpgrade()
            {
                slot.Initialize();
            }
        }
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private BuyUpgrade[] sales;

        private int _money;

        private void Start()
        {
            foreach (var slot in sales)
            {
                slot.Initialize();
                slot.transform.Find("Buy").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }
            UpdateMoney();
        }
        public void UpdateMoney()
        {
            _money = MapsCompletion.Instance.TotalScore;
            _money -= Upgrades.Instance.GetTotalCost();

            moneyText.text = _money.ToString();
            foreach(BuyUpgrade slot in sales)
            {
                slot.CheckCost(_money);
            }
        }
    }
}