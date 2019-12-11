
/// <summary>
/// VRGazeItemBase.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Demo
{
    public class VRGazeItemBase : GazeButton, IGaze, IPointerClickHandler
    {
        public UnityEvent clickEvent;

        public void GazeClick()
        {
            OnPointerClick(null);
        }

        public void GazeEnter()
        {
            OnPointerEnter(null);
        }

        public void GazeExit()
        {
            OnPointerExit(null);
        }

        public void GazeHover()
        {
            OnPointerHover(null);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (clickEvent != null)
            {
                clickEvent.Invoke();
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            base.OnPointerExit(eventData);
        }
    }
}