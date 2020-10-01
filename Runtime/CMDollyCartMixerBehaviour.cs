using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

namespace BK.CMDollyCartTimelineHelper
{
    public class CMDollyCartMixerBehaviour : PlayableBehaviour
    {
        public CinemachineDollyCart dollyCart;
        private float m_DefaultPosition, m_DefaultSpeed;
        private bool stateCached;

        public override void OnGraphStop(Playable playable) => ResetState();

        public void CacheStartState()
        {
            if (stateCached) return;

            stateCached = true;

            m_DefaultPosition = dollyCart.m_Position;
            m_DefaultSpeed = dollyCart.m_Speed;
        }

        public void ResetState()
        {
            stateCached = false;
            dollyCart.m_Position = m_DefaultPosition;
            dollyCart.m_Speed = m_DefaultSpeed;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            dollyCart = playerData as CinemachineDollyCart;
            // float finalSpeed = 0f;
            // float finalWeight = 0f;

            if (!dollyCart)
                return;

            CacheStartState();

            int inputCount = playable.GetInputCount(); //get the number of all clips on this track


            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<CMDollyCartBehaviour> inputPlayable = (ScriptPlayable<CMDollyCartBehaviour>)playable.GetInput(i);
                CMDollyCartBehaviour input = inputPlayable.GetBehaviour();

                // finalWeight += inputWeight;

                // Use the above variables to process each frame of this playable.
                // finalSpeed += input.GetSpeed() * inputWeight;
                if (inputWeight == 1)
                {
                    // var maxTime = Mathf.Min((float)inputPlayable.GetTime(), (float)inputPlayable.GetDuration());
                    dollyCart.m_Speed = 0;
                    
                    switch (input.positionMode)
                    {
                        case CMDollyCartBehaviour.PositionMode.BaseOnOriginal:
                            dollyCart.m_Position = m_DefaultPosition + (float)(inputPlayable.GetTime() * input.speed);
                            break;
                        case CMDollyCartBehaviour.PositionMode.CustomStart:
                            dollyCart.m_Position = input.customStart + (float)(inputPlayable.GetTime() * input.speed);
                            break;
                        case CMDollyCartBehaviour.PositionMode.DeltaTime:
                            dollyCart.m_Position += (float)(info.deltaTime * input.speed);
                            break;
                    }
                }
            }

            // Debug.Log(finalWeight);

            // dollyCart.m_Speed = 0;

            // if (finalWeight > 0)
            //     dollyCart.m_Position = m_DefaultPosition + (float)(playable.GetTime() * finalSpeed);
        }
    }
}