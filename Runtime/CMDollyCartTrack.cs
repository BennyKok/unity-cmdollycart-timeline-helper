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
    }
}