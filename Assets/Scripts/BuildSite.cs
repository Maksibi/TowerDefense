using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TowerDefense
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
    {
        public static event Action<Transform> OnClickEvent;
        public static event Action<Transform> OnExitEvent;

        public static void HideControls()
        {
            OnClickEvent(null);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(transform.root);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExitEvent(transform.root);
        }
    }
}