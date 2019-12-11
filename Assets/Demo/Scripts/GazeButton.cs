
/// <summary>
/// GazeButton.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Demo
{
    public class GazeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerHoverHandler
    {
        #region Fields
        public Image leftImage;
        public Image rightImage;
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

        #endregion

        #region Private Methods

        #endregion

        #region Protected & Public Methods

        #endregion

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (leftImage != null)
            {
                leftImage.fillAmount = 0;
                leftImage.gameObject.SetActive(true);
            }
            if (rightImage != null)
            {
                rightImage.fillAmount = 0;
                rightImage.gameObject.SetActive(true);
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (leftImage != null)
            {
                leftImage.fillAmount = 0;
                leftImage.gameObject.SetActive(false);
            }
            if (rightImage != null)
            {
                rightImage.fillAmount = 0;
                rightImage.gameObject.SetActive(false);
            }
        }

        public virtual void OnPointerHover(PointerEventData eventData)
        {
            if (leftImage == null || rightImage == null)
            {
                return;
            }

            if (leftImage.fillAmount >= 1f && rightImage.fillAmount >= 1)
            {
                leftImage.gameObject.SetActive(false);
                rightImage.gameObject.SetActive(false);

                GazeController.GazeHoverEnd(gameObject);
                return;
            }

            leftImage.fillAmount += Time.deltaTime;
            rightImage.fillAmount += Time.deltaTime;
        }
    }
}