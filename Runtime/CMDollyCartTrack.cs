using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;

namespace BK.CMDollyCartTimelineHelper
{
    [TrackColor(0.7238969f, 0.6117647f, 0.9058824f)]
    [TrackClipType(typeof(CMDollyCartClip))]
    [TrackBindingType(typeof(CinemachineDollyCart))]
    public class CMDollyCartTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<CMDollyCartMixerBehaviour>.Create(graph, inputCount);
        }

        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            CinemachineDollyCart trackBinding = director.GetGenericBinding(this) as CinemachineDollyCart;
            if (trackBinding == null)
                return;

            driver.AddFromName<CinemachineDollyCart>(trackBinding.gameObject, "m_Position");
            driver.AddFromName<CinemachineDollyCart>(trackBinding.gameObject, "m_Speed");
#endif
            base.GatherProperties(director, driver);
        }
    }
}