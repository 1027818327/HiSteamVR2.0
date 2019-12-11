
/// <summary>
/// SmoothFollowCamera.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using UnityEngine;

namespace Demo
{
    public class SmoothFollowCamera : MonoBehaviour
    {
        #region Fields
        private Vector3 currentVelocity;
        public float smoothTime = 0.1f;

        #endregion

        #region Properties

        #endregion

        #region Unity Messages
        //    void Awake()
        //    {
        //
        //    }
        //    void OnEnable()
        //    {
        //
        //    }
        //
        //    void Start() 
        //    {
        //    
        //    }
        //    
        //    void Update() 
        //    {
        //    
        //    }
        //
        //    void OnDisable()
        //    {
        //
        //    }
        //
        //    void OnDestroy()
        //    {
        //
        //    }

        void LateUpdate()
        {
            Camera tempCamera = Camera.main;
            if (tempCamera == null)
            {
                Debug.LogError("相机获取异常");
                return;
            }
            Quaternion tempQuaternion = Quaternion.LookRotation(tempCamera.transform.forward);

            tempQuaternion.x = 0;
            tempQuaternion.z = 0;
            transform.rotation = tempQuaternion;
            transform.position = Vector3.SmoothDamp(transform.position, tempCamera.transform.position + tempCamera.transform.forward, ref currentVelocity, smoothTime);
        }

        #endregion

        #region Private Methods

        #endregion

        #region Protected & Public Methods

        #endregion
    }
}