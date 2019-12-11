
/// <summary>
/// SimulateEvent.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo
{
    public class SimulateEvent : MonoBehaviour
    {
        [MenuItem("CONTEXT/Transform/模拟点击")]
        static void SimluateClick()
        {
            GameObject tempObj = Selection.activeGameObject;

            if (tempObj != null)
            {
                ExecuteEvents.Execute<IPointerClickHandler>(tempObj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            }
        }
    }
}