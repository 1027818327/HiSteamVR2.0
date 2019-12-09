
/// <summary>
/// VRGazeItem.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo
{
    public class VRGazeItem : GazeButton, IGaze, IPointerClickHandler
    {
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

        public void OnPointerClick(PointerEventData eventData)
        {
            Camera tempCam = Camera.main;

            Vector3 pos = tempCam.transform.position + tempCam.transform.forward * 1.5f;
            transform.position = pos;
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