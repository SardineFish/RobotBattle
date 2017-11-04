using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Assets.Scripts;


namespace Assets.Editor
{
    [CustomEditor(typeof(Map))]
    public class MapEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var map = target as Map;
            if (GUILayout.Button("ClearWaypoints"))
            {
                map.ClearWaypoints();
            }
            if (GUILayout.Button("ExtendWaypoints"))
            {
                map.ExtandWaypoints(map.MaxExtend);
            }
        }
    }
}
