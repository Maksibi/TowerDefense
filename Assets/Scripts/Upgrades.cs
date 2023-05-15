using System;
using UnityEngine;

namespace TowerDefense
{
    public class Upgrades : MonoSingleton<Upgrades>
    {
        public const string FILENAME = "upgrades.dat";
        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset asset;
            public int level = 0;
        }
        [SerializeField] private UpgradeSave[] save;

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(FILENAME, ref save);
        }
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    upgrade.level++;
                    Saver<UpgradeSave[]>.Save(FILENAME, Instance.save);
                }
            }
        }
        public int GetTotalCost()
        {
            int result = 0;
            foreach (UpgradeSave  upgrade in Instance.save)
            {
                for (int i = 0; i < upgrade.level; i++)
                {
                    result += upgrade.asset.costByLevel[i];
                }
            }
            return result;
        }
        public static int GetUpgradesLevel(UpgradeAsset asset)
        {
            foreach (UpgradeSave upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    return upgrade.level;
                }
            }
            return 0;
        }
    }
}