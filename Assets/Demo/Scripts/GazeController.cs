using HTC.UnityPlugin.VRModuleManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Demo
{
    public interface IPointerHoverHandler : IEventSystemHandler
    {
        void OnPointerHover(PointerEventData eventData);
    }

    public class GazeController : MonoBehaviour
    {
        public static GazeController instance;

        //准星容器  
        public Canvas reticleCanvas;

        //击中的当前目标  
        public GameObject target;
        //初始位置  
        private Vector3 originPos;
        //初始缩放  
        private Vector3 originScale;
        //倒计时时间  
        private float countDownTime = 3;
        //当前时间  
        private float currentTime = 0;

        private bool gazeOne;

        // Use this for initialization  
        void Start()
        {
            instance = this;

            //记录初始位置  
            originPos = reticleCanvas.transform.localPosition;
            //记录初始缩放  
            originScale = reticleCanvas.transform.localScale;
        }

        void Update()
        {
            if (gazeOne)
            {
                enabled = false;
                Invoke("OpenGaze", 1f);
                return;
            }

#if UNITY_EDITOR
            Debug.DrawRay(transform.position, transform.forward, Color.red, 1f);
#endif
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            //如果碰撞到了物体  
            if (Physics.Raycast(ray, out hit, 50))
            {
                //将碰撞的位置赋给准星  
                reticleCanvas.transform.position = hit.point;

                //视线初次进入  
                if (hit.transform.gameObject != target)
                {
                    //如果上次的目标物体不为空，进行移出的操作  
                    if (target != null)
                    {
                        GazeExit(target);
                    }

                    //将击中的目标赋给当前的目标物体  
                    target = hit.transform.gameObject;
                    GazeEnter(target);
                }
                else//视线在此停留  
                {
                    currentTime += Time.deltaTime;
                    //设定时间未结束  
                    if (countDownTime - currentTime <= 0)
                    {
                        currentTime = 0;
                        //GazeClick(target);
                    }
                }
            }
            //没有碰撞到物体  
            else
            {
                GazeExit(target);

                reticleCanvas.transform.position = originPos;
                //缩放复位  
                reticleCanvas.transform.localScale = originScale;
            }
        }

        private void GazeEnter(GameObject varObj)
        {
            if (varObj == null)
            {
                return;
            }

            Debug.Log("GazeEnter");

            if (varObj.layer != LayerMask.NameToLayer("UI"))
            {
                var tempGate = varObj.GetComponent<IGaze>();
                if (tempGate != null)
                {
                    tempGate.GazeEnter();
                }
            }
            else
            {
                ExecuteEvents.Execute(varObj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }

        private void GazeExit(GameObject varObj)
        {
            Debug.Log("GazeExit");

            if (varObj == null)
            {
                return;
            }

            if (varObj.layer != LayerMask.NameToLayer("UI"))
            {
                var tempGate = varObj.GetComponent<IGaze>();
                if (tempGate != null)
                {
                    tempGate.GazeExit();
                }
            }
            else
            {
                ExecuteEvents.Execute(varObj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
            }
        }

        private void GazeHover(GameObject varObj)
        {
            Debug.Log("GazeHover");

            if (varObj == null)
            {
                return;
            }

            if (varObj.layer != LayerMask.NameToLayer("UI"))
            {
                var tempGate = varObj.GetComponent<IGaze>();
                if (tempGate != null)
                {
                    tempGate.GazeHover();
                }
            }
            else
            {
                var tempEventData = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute<IPointerHoverHandler>(varObj, tempEventData, (handler, data) =>
                {
                    handler.OnPointerHover(tempEventData);
                });
            }
        }

        private void GazeClick(GameObject varObj)
        {
            Debug.Log("GazeClick");

            if (varObj == null)
            {
                return;
            }

            if (varObj.layer != LayerMask.NameToLayer("UI"))
            {
                var tempGate = varObj.GetComponent<IGaze>();
                if (tempGate != null)
                {
                    tempGate.GazeClick();
                }
            }
            else
            {
                ExecuteEvents.Execute(varObj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            }
        }


        private void CheckHand()
        {
            uint tempRightIndex = VRModule.GetRightControllerDeviceIndex();
            uint tempLeftIndex = VRModule.GetLeftControllerDeviceIndex();

            bool isRightConnected = VRModule.GetDeviceState(tempRightIndex).isConnected;
            bool isLeftConnected = VRModule.GetDeviceState(tempLeftIndex).isConnected;

            if (!isLeftConnected && !isRightConnected)
            {
                reticleCanvas.enabled = false;
            }
            else
            {
                reticleCanvas.enabled = true;
            }
        }

        public static void GazeHoverEnd(GameObject varObj)
        {
            if (instance != null && instance.target != null && instance.target == varObj)
            {
                instance.currentTime = 0;
                instance.GazeClick(instance.target);
                instance.GazeExit(instance.target);
                instance.target = null;
                instance.gazeOne = true;
            }
        }

        private void OpenGaze()
        {
            gazeOne = false;
            enabled = true;
        }
    }
}