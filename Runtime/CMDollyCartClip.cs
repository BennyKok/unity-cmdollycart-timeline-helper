using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;

namespace BK.CMDollyCartTimelineHelper
{
    [Serializable]
    public class CMDollyCartClip : PlayableAsset, ITimelineClipAsset
    {
        public CMDollyCartBehaviour template = new CMDollyCartBehaviour();
        // public ExposedReference<CinemachineDollyCart> dollyCart;

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Extrapolation; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CMDollyCartBehaviour>.Create(graph, template);
            CMDollyCartBehaviour clone = playable.GetBehaviour();
            // clone.dollyCart = dollyCart.Resolve(graph.GetResolver());
            return playable;
        }
    }
}