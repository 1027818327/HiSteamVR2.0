
/// <summary>
/// PushDownButton.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Vive;
using UnityEngine;
using UnityEngine.Events;

namespace Demo
{
    public class PushDownButton : MonoBehaviour, IColliderEventHoverEnterHandler, IColliderEventHoverExitHandler
    {
        public Vector3 localMoveDistance = new Vector3(0, -0.1f, 0);

        public UnityEvent onButtonDown;

        private Vector3 startPosition;
        private Vector3 endPosition;

        private Vector3 handEnteredPosition;
        private ViveColliderEventCaster mCaster;
        private Transform mHand;

        private bool hovering;

        private bool buttonDown = false;
        private float curY;


        public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
        {
            hovering = true;
            mCaster = eventData.eventCaster as ViveColliderEventCaster;
            mHand = eventData.eventCaster.transform;
            handEnteredPosition = mHand.position;
            ColorSelf(Color.yellow);
        }

        public void OnColliderEventHoverExit(ColliderHoverEventData eventData)
        {
            hovering = false;
            ColorSelf(Color.white);
            mCaster = null;
            mHand = null;
            transform.localPosition = startPosition;
            buttonDown = false;
        }

        void Start()
        {
            startPosition = transform.localPosition;
            endPosition = startPosition + localMoveDistance;
            handEnteredPosition = endPosition;
        }

        void Update()
        {
            if (hovering)
            {
                Vector3 tempOffset = transform.InverseTransformPoint(mHand.position) - transform.InverseTransformPoint(handEnteredPosition);
                handEnteredPosition = mHand.position;

                if (tempOffset.y != 0)
                {
                    curY = tempOffset.y;
                }
            }
        }

        void LateUpdate()
        {
            if (hovering)
            {
                Vector3 tempPos = transform.localPosition;
                tempPos.y = Mathf.Lerp(tempPos.y, tempPos.y + curY, 10 * Time.deltaTime);
                transform.localPosition = tempPos;

                if (transform.localPosition.y <= endPosition.y)
                {
                    OnButtonDown();
                    transform.localPosition = endPosition;
                }
                else
                {
                    buttonDown = false;
                }

                if (transform.localPosition.y > startPosition.y)
                {
                    transform.localPosition = startPosition;
                }
            }
        }

        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }

        private void OnButtonDown()
        {
            if (buttonDown)
            {
                return;
            }

            buttonDown = true;
            if (mCaster != null)
            {
                ViveInput.TriggerHapticPulse((HandRole)mCaster.viveRole.roleValue, 100);
            }
            
            if (onButtonDown != null)
            {
                onButtonDown.Invoke();
            }
        }
    }
}