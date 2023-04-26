using System;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform rectTransform;

        private float timer = 3.0f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

            BuildSite.OnClickEvent += MoveTransform;
            BuildSite.OnExitEvent += HideControl;

            gameObject.SetActive(false);
        }
        private void Update()
        {
            ReduceTimer();

            if (timer <= 0) gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveTransform;
            BuildSite.OnExitEvent -= HideControl;
        }
        private void MoveTransform(Transform target)
        {
            if (target)
            {
                Vector2 position = Camera.main.WorldToScreenPoint(target.position);

                rectTransform.position = position;

                gameObject.SetActive(true);
            }
            else gameObject.SetActive(false);
            foreach (TowerBuyControl control in GetComponentsInChildren<TowerBuyControl>())
            {
                control.SetBuildSite(target);
            }
        }
        private void HideControl(Transform obj)
        {
            timer = 3.0f;
        }
        private void ReduceTimer()
        {
            timer -= Time.deltaTime;
        }
    }
}