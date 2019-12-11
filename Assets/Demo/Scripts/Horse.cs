
/// <summary>
/// Horse.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using HTC.UnityPlugin.ColliderEvent;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo
{
    public class Horse : MonoBehaviour, 
        IColliderEventHoverEnterHandler, IColliderEventHoverExitHandler, IColliderEventClickHandler
    {
        public GameObject mTarget;
        private bool doubleClick;

        public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
        {
            OnPointerEnter(null);
        }

        public void OnColliderEventHoverExit(ColliderHoverEventData eventData)
        {
            OnPointerExit(null);
        }

        public void OnColliderEventClick(ColliderButtonEventData eventData)
        {
            if (eventData.button == ColliderButtonEventData.InputButton.Trigger)
            {
                if (doubleClick)
                {
                    Debug.Log("OnColliderEventClick");
                    OnPointerClick(null);
                    doubleClick = false;
                }
                else
                {
                    doubleClick = true;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            bool show = !mTarget.activeSelf;
            mTarget.SetActive(show);
        }

        
    }
}