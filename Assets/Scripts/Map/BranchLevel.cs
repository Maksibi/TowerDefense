using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private MapLevel rootLevel;
        [SerializeField] private TextMeshProUGUI pointsText;

        [SerializeField] private int requiredPoints = 3;

        internal void TryActivate()
        {
            gameObject.SetActive(rootLevel.isCompleted);
            if (requiredPoints > MapsCompletion.Instance.TotalScore)
            {
                pointsText.text = "Requires" + requiredPoints.ToString();
            }
            else
            {
                pointsText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }
        }
    }
}