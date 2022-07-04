using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playermovment",menuName ="playermovmentdata" , order = 1)]
public class SchripobjectPlayerMovment : ScriptableObject
{
    public float M_ForwardAccel = 8f; // speed that it aceleerd with
    public float M_ReverseAccel = 4f;// speed that it gows back with
    public float M_MaxSpeed = 50f; // the max speed that it can have
    public float M_TurnStrength = 180f; // how fast it can turn
    public float M_GraftyFalls = 10f; // the extra garfity 
    public float M_FlyDrag = 0.1f; // de drag for in the air
    public float M_NormalDrag = 3f; // the drag if on the ground
}
