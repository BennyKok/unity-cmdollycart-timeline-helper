using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;

namespace BK.CMDollyCartTimelineHelper
{
    [Serializable]
    public class CMDollyCartBehaviour : PlayableBehaviour
    {
        public float speed;

        public PositionMode positionMode;
        public float customStart;

        public enum PositionMode
        {
            BaseOnOriginal, DeltaTime, CustomStart
        }
    }
}