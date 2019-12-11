
/// <summary>
/// UGUISpriteAnimation.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Demo
{
    [RequireComponent(typeof(Image))]
    public class UGUISpriteAnimation : MonoBehaviour
    {
        public UnityEvent m_finish;
        private Image imageSource;
        private int mCurFrame = 0;
        private float mDelta = 0;

        public float FPS = 5;
        public List<Sprite> SpriteFrames;
        public bool IsPlaying = false;
        public bool Foward = true;
        public bool AutoPlay = false;
        public bool Loop = false;

        public int FrameCount
        {
            get
            {
                return SpriteFrames.Count;
            }
        }

        void Awake()
        {
            imageSource = GetComponent<Image>();
        }

        void Start()
        {
            if (AutoPlay)
            {
                Play();
            }
        }

        private void SetSprite(int idx)
        {
            if (imageSource == null)
            {
                imageSource = GetComponent<Image>();
            }

            imageSource.sprite = SpriteFrames[idx];
            //该部分为设置成原始图片大小，如果只需要显示Image设定好的图片大小，注释掉该行即可。
            //ImageSource.SetNativeSize();
        }

        public void Play()
        {
            IsPlaying = true;
            Foward = true;
        }

        public void PlayReverse()
        {
            IsPlaying = true;
            Foward = false;
        }

        void Update()
        {
            if (!IsPlaying || 0 == FrameCount)
            {
                return;
            }

            mDelta += Time.deltaTime;
            if (mDelta > 1 / FPS)
            {
                mDelta = 0;
                if (Foward)
                {
                    mCurFrame++;
                }
                else
                {
                    mCurFrame--;
                }

                if (mCurFrame >= FrameCount)
                {
                    if (Loop)
                    {
                        mCurFrame = 0;
                    }
                    else
                    {
                        IsPlaying = false;
                        if (m_finish != null)
                        {
                            m_finish.Invoke();
                        }
                        return;
                    }
                }
                else if (mCurFrame < 0)
                {
                    if (Loop)
                    {
                        mCurFrame = FrameCount - 1;
                    }
                    else
                    {
                        IsPlaying = false;
                        if (m_finish != null)
                        {
                            m_finish.Invoke();
                        }
                        return;
                    }
                }

                SetSprite(mCurFrame);
            }
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Resume()
        {
            if (!IsPlaying)
            {
                IsPlaying = true;
            }
        }

        public void Stop()
        {
            mCurFrame = 0;
            SetSprite(mCurFrame);
            IsPlaying = false;
        }

        [ContextMenu("重新播放")]
        public void Rewind()
        {
            mCurFrame = 0;
            SetSprite(mCurFrame);
            Play();
        }
    }
}