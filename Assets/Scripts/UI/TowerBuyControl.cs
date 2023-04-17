using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private TowerAsset towerAsset;

        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private Button button;

        [SerializeField] private Transform buildSite;

        public void SetBuildSite(Transform value)
        {
            buildSite = value;
        }
        private void Awake()
        {
            Player.OnGoldUpdate += GoldStatusCheck;
        }
        private void Start()
        {
            text.text = towerAsset.GoldCost.ToString();

            if(button != null) button.GetComponent<Image>().sprite = towerAsset.GUISprite;
        }
        private void GoldStatusCheck(int gold)
        {
            if (gold >= towerAsset.GoldCost != button.interactable & button != null)
            {
                button.interactable = !button.interactable;
                text.color = button.interactable ? Color.white : Color.red;
            }
        }
        public void Buy()
        {
            Player.Instance.TryBuild(towerAsset, buildSite);
            TowerDefense.BuildSite.HideControls();
        }
    }
}