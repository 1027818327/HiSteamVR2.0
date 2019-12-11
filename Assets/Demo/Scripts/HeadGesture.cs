
/// <summary>
/// HeadGesture.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using UnityEngine;
using UnityEngine.Events;

namespace Demo
{
    public class HeadGesture : MonoBehaviour
    {
        public bool isFacingDown = false;
        public UnityEvent<bool> mHeadEvent;
        [SerializeField]
        private GameObject mTarget;

        private void Update()
        {
            isFacingDown = DetectFacingDown();
            if (mHeadEvent != null)
            {
                mHeadEvent.Invoke(isFacingDown);
            }
            if (mTarget != null)
            {
                mTarget.SetActive(isFacingDown);
            }
        }

        private bool DetectFacingDown()
        {
            return (CameraAngleFromGround() < 60f);
        }

        private float CameraAngleFromGround()
        {
            return Vector3.Angle(Vector3.down, Camera.main.transform.rotation * Vector3.forward);
        }
    }
}