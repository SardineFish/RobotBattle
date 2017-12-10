using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Singleton<T>:MonoBehaviour where T: Singleton<T>, new()
{
    public static T Current { get; protected set; }
}