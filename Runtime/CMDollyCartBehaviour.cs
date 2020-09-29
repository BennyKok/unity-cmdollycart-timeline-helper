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
        public CinemachineDollyCart dollyCart;
        // public bool overrideSpeed;
        public MoveDirection moveDirection;
        public float speed;
        private float GetSpeed() => speed;


        private float m_DefaultPosition, m_DefaultSpeed;
        private bool stateCached;

        public enum MoveDirection
        {
            Forward, Backward
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            CacheStartState();

            // float normalisedTime = (float)(playable.GetTime() / playable.GetDuration());
            // dollyCart.m_Position = normalisedTime * dollyCart.m_Speed;

            UpdateDollyCart(playable.GetTime());
            // Debug.Log(dollyCart.m_Position);
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            var duration = playable.GetDuration();
            var time = playable.GetTime();
            var count = time + info.deltaTime;

            if ((info.effectivePlayState == PlayState.Paused && count > duration) || Mathf.Approximately((float)time, (float)duration))
            {
                // Execute your finishing logic here:
                UpdateDollyCart(count);
            }
        }

        private void UpdateDollyCart(double time)
        {
            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    dollyCart.m_Position = m_DefaultPosition + (float)(time * GetSpeed());
                    break;
                case MoveDirection.Backward:
                    dollyCart.m_Position = m_DefaultPosition - (float)(time * GetSpeed());
                    break;
            }
        }

        public override void OnPlayableDestroy(Playable playable) => ResetState();

        //Make sure we have cache the start value in edit mode also
        public override void OnGraphStart(Playable playable) => CacheStartState();

        private void CacheStartState()
        {
            if (stateCached) return;

            stateCached = true;

            m_DefaultPosition = dollyCart.m_Position;
            m_DefaultSpeed = dollyCart.m_Speed;

            dollyCart.m_Speed = 0;
        }

        private void ResetState()
        {
            stateCached = false;
            dollyCart.m_Position = m_DefaultPosition;
            dollyCart.m_Speed = m_DefaultSpeed;
        }
    }
}