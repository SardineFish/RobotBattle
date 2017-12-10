using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSystem))]
[CanEditMultipleObjects]
public class GameSystemEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        var gameSys = target as GameSystem;
        foreach (var team in gameSys.AvailableTeams)
        {
            if (team.SpawnPosWaypoint)
                team.SpawnPosition = team.SpawnPosWaypoint.transform.position;
        }
    }

    
}