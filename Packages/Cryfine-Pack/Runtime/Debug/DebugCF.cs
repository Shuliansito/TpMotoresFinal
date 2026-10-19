using System;
using System.Net;
using UnityEngine;

namespace Cryfine
{
    public static class DebugCF
    {
        public static void Distance2GameObjects(GameObject go1, GameObject go2)
        {
            Debug.Log("[Cryfine Debug] Distance between "+ go1.name + " - " + go2.name+": "+MathCF.DisMan3D(go1.transform.position.x, go1.transform.position.y, go1.transform.position.z, go2.transform.position.x, go2.transform.position.y, go2.transform.position.z));
        }
        public static void IsAtDistance(GameObject go1, GameObject go2, float distance)
        {
            Debug.Log("[CryFine Debug] "+ go1.name + " - "+go2.name+ " are in a range of "+ distance+": "+
            (MathCF.DisSqr3D(go1.transform.position.x, go1.transform.position.y, go1.transform.position.z, go2.transform.position.x, go2.transform.position.y, go2.transform.position.z) < distance * distance));
        }
        public static void DrawDistance(Vector3 a, Vector3 b, Color color, float duracion = 0f)
        {
            Debug.DrawLine(a, b, color, duracion);
        }
        public static void DrawDistanceLabel(Vector3 a, Vector3 b, Color color)
        {
            #if UNITY_EDITOR
            Gizmos.color = color;
            Gizmos.DrawLine(a, b);
            Vector3 midpoint = (a + b) / 2f;
            UnityEditor.Handles.Label(midpoint, MathCF.DisEuc3D(a.x, a.y, a.z, b.x, b.y, b.z).ToString("F2"));
            #endif
        }
    }
}