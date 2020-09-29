using UnityEditor;
using UnityEngine;

namespace BK.CMDollyCartTimelineHelper.Editor
{
    [CustomEditor(typeof(CMPathAutoGround))]
    public class CMPathAutoGroundEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            CMPathAutoGround m_target = (CMPathAutoGround)target;
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, "m_Script");
            // using (new EditorGUI.DisabledScope(m_target.autoProject))
            // {
            if (GUILayout.Button("Project"))
            {
                ((CMPathAutoGround)target).ProjectToGround(true);
            }
            // }
            serializedObject.ApplyModifiedProperties();
        }
    }
}