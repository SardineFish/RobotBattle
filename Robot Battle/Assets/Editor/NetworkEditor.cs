using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NetworkSystem))]
[CanEditMultipleObjects]
public class NetworkEditor : UnityEditor.Editor
{
    bool showSpawnable = true;
    [ExecuteInEditMode]
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var networkSystem = target as NetworkSystem;
        networkSystem.networkAddress = EditorGUILayout.TextField("Host", networkSystem.networkAddress);
        networkSystem.networkPort = EditorGUILayout.IntField("Port", networkSystem.networkPort);
        networkSystem.PlayerBlue =
            EditorGUILayout.ObjectField("Team Blue Player:", networkSystem.PlayerBlue, typeof(GameObject), true) as
                GameObject;
        networkSystem.PlayerRed =
            EditorGUILayout.ObjectField("Team Red Player:", networkSystem.PlayerRed, typeof(GameObject), true) as
                GameObject;
        EditorGUILayout.Space();

        showSpawnable = EditorGUILayout.Foldout(showSpawnable, "Spawnable Prefabs");
        if (showSpawnable)
        {
            GUIStyle style = new GUIStyle();
            style.margin.left = 20;
            EditorGUILayout.BeginVertical(style);

            EditorGUILayout.BeginHorizontal();
            var count = EditorGUILayout.IntField("Count:", networkSystem.spawnPrefabs.Count, GUILayout.Width(200));
            if (GUILayout.Button("+"))
                count++;
            if (GUILayout.Button("-"))
                count--;
            EditorGUILayout.EndHorizontal();

            if (count < networkSystem.spawnPrefabs.Count)
                networkSystem.spawnPrefabs.RemoveRange(count, networkSystem.spawnPrefabs.Count - count);
            else if (count > networkSystem.spawnPrefabs.Count)
                networkSystem.spawnPrefabs.AddRange((GameObject[])Array.CreateInstance(typeof(GameObject),
                    count - networkSystem.spawnPrefabs.Count));
            for (var i = 0; i < networkSystem.spawnPrefabs.Count; i++)
            {
                networkSystem.spawnPrefabs[i] =
                    EditorGUILayout.ObjectField(networkSystem.spawnPrefabs[i], typeof(GameObject), true) as GameObject;
            }
            EditorGUILayout.EndVertical();
        }
    }
}