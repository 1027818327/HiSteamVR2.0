
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
    public class VRGazeItem : VRGazeItemBase
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            Camera tempCam = Camera.main;

            Vector3 pos = tempCam.transform.position + tempCam.transform.forward * 1.5f;
            transform.position = pos;
        }
    }
}