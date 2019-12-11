
/// <summary>
/// UIVideo.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using DG.Tweening;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

namespace Demo
{
    public class UIVideo : MonoBehaviour
    {
        #region Fields
        private bool isPlay;
        public MediaPlayer mMedia;

        public GameObject playImage;
        public GameObject pauseImage;

        public Vector3 startPos;
        public Vector3 endPos;

        #endregion

        #region Properties

        #endregion

        #region Unity Messages
        //    void Awake()
        //    {
        //
        //    }
        void OnEnable()
        {
            transform.localPosition = startPos;
            var tempTween = transform.DOLocalMove(endPos, 0.8f).SetEase(Ease.InCubic);
            tempTween.onComplete += MoveDownFinish;

            var group = GetComponent<CanvasGroup>();
            group.DOFade(1, 0.8f);
        }
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
        void OnDisable()
        {
            isPlay = false;
            Pause();
            mMedia.Stop();
        }
        //
        //    void OnDestroy()
        //    {
        //
        //    }

        #endregion

        #region Private Methods

        #endregion

        #region Protected & Public Methods
        public void SwitchPlayOrPause()
        {
            isPlay = !isPlay;
            if (isPlay)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }

        public void Close()
        {
            var tempTween = transform.DOLocalMove(startPos, 0.8f).SetEase(Ease.OutCubic);
            tempTween.onComplete += MoveUpFinish;

            var group = GetComponent<CanvasGroup>();
            group.DOFade(0, 0.8f);
        }

        private void Play()
        {
            mMedia.Play();
            playImage.SetActive(false);
            pauseImage.SetActive(true);
        }


        private void Pause()
        {
            mMedia.Pause();
            playImage.SetActive(true);
            pauseImage.SetActive(false);
        }

        private void MoveDownFinish()
        {
            isPlay = true;
            Play();
        }

        private void MoveUpFinish()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}