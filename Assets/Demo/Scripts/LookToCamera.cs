
/// <summary>
/// LookToCamera.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using UnityEngine;

namespace Demo
{
    public class LookToCamera : MonoBehaviour
    {
        void Awake()
        {
            Update();
        }

        void Update()
        {
            Camera tempCamera = Camera.main;
            if (tempCamera == null)
            {
                Debug.LogError("相机获取异常");
                return;
            }

            //Vector3 pos = tempCamera.transform.position + tempCamera.transform.forward * 2;
            Quaternion tempQuaternion = Quaternion.LookRotation(tempCamera.transform.forward);
            //transform.position = new Vector3(pos.x, 2.5f, pos.z);

            tempQuaternion.x = 0;
            tempQuaternion.z = 0;
            transform.rotation = tempQuaternion;
        }
    }
}