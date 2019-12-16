
/// <summary>
/// Demo.cs
/// </summary>
/// <remarks>
/// #CreateTime#: 创建. 陈伟超 <br/>
/// </remarks>

using UnityEngine;

namespace Demo
{
    public class SmallMap : MonoBehaviour
    {
        public Transform player;
        public RectTransform mMapTrans;

        /// <summary>
        /// 地面
        /// </summary>
        public Transform ground;

        //小地图的尺寸
        Vector2 mipMapSize;
        //等比例映射后角色在小地图的位置
        Vector2 position;
        //人物相对于地形的坐标
        Vector3 localPos;

        float rateX, rateY;

        void Start() 
        {
            mipMapSize = mMapTrans.sizeDelta;
        }

        void Update()
        {
            if (player == null) 
            {
                return;
            }
            PlayerRate();
            MipMapWave();
        }

        //角色在地形上的比例
        public void PlayerRate()
        {
            localPos = player.position - ground.position;
            localPos = ground.InverseTransformPoint(localPos);

            rateX = localPos.x / ground.localScale.x;
            rateY = localPos.z / ground.localScale.z;
        }

        public void MipMapWave()
        {
            position.x = mipMapSize.x * rateX;
            position.y = mipMapSize.y * rateY;
            transform.localPosition = position;

            Vector3 tmpAngle = transform.localEulerAngles;
            tmpAngle.z = 90 - player.localEulerAngles.y;
            transform.localEulerAngles = tmpAngle;
        }
    }
}