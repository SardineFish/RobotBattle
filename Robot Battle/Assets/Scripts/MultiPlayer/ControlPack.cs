using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ControlPack
{
    [SerializeField]
    public Vector3 Look;
    [SerializeField]
    public Vector3 Move;
    [SerializeField]
    public bool Fire;

    [SerializeField]
    public Vector3 Position;

    public ControlPack(Vector3 position ,Vector3 look, Vector3 move, bool fire)
    {
        Position = position;
        Look = look;
        Move = move;
        Fire = fire;
    }

    public ControlPack() { }
}