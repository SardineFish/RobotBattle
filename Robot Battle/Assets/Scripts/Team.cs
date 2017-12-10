using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Team
{
    [SerializeField]
    public int TeamID;
    [SerializeField]
    public GameObject PlayerPrefab;
    [SerializeField]
    public GameObject SpawnPosWaypoint;
    [SerializeField]
    public Vector3 SpawnPosition;
}