using UnityEngine;
using Cinemachine;

namespace BK.CMDollyCartTimelineHelper
{
    [ExecuteAlways]
    public class CMPathAutoGround : MonoBehaviour
    {
        public bool autoProject = true;
        public LayerMask layerMask;
        public float projectionHeight = 1;
        public float projectionMaxDistance = 5;

        private CinemachineSmoothPath path = null;
        private CinemachineSmoothPath.Waypoint[] m_WaypointsCaches;

        private void LateUpdate()
        {
            if (autoProject)
            {
                ProjectToGround();
            }
        }

        public void CacheWaypoints()
        {
            m_WaypointsCaches = new CinemachineSmoothPath.Waypoint[path.m_Waypoints.Length];
            path.m_Waypoints.CopyTo(m_WaypointsCaches, 0);
        }

        public bool EnsurePathComponent()
        {
            if (path == null) path = GetComponent<CinemachineSmoothPath>();

            if (m_WaypointsCaches == null) CacheWaypoints();

            return path;
        }

        public void ProjectToGround(bool force = false, bool useCache = true)
        {
            if (!EnsurePathComponent()) return;

            var changed = false;

            //when a point is removed
            if (m_WaypointsCaches.Length > path.m_Waypoints.Length)
            {
                if (useCache)
                    CacheWaypoints();
            }

            for (int i = 0; i < path.m_Waypoints.Length; i++)
            {
                CinemachineSmoothPath.Waypoint waypoint = path.m_Waypoints[i];

                if (useCache && !force)
                    if (m_WaypointsCaches.Length == path.m_Waypoints.Length)
                        //This waypoint hasn't changed, skipping raycast\
                        if (waypoint.position == m_WaypointsCaches[i].position)
                        {
                            continue;
                        }

                var rayOrigin = path.transform.TransformPoint(waypoint.position);
                rayOrigin.y += projectionHeight;
#if UNITY_EDITOR
                if (UnityEditor.Selection.activeGameObject == gameObject)
                    UnityEngine.Debug.DrawRay(rayOrigin, Vector3.down * projectionMaxDistance, Color.cyan, 0.1f);
#endif
                if (Physics.Raycast(rayOrigin, Vector3.down, out var hit, projectionMaxDistance, layerMask, QueryTriggerInteraction.Ignore))
                {
                    var newPos = path.transform.InverseTransformPoint(hit.point);

                    if (newPos != waypoint.position)
                    {
                        changed = true;
                        waypoint.position = newPos;
                        path.m_Waypoints[i] = waypoint;
                    }
                }
            }

            if (changed)
            {
                // Debug.Log("Changed");
                path.InvalidateDistanceCache();
            }

            if (useCache)
                CacheWaypoints();
        }
    }
}