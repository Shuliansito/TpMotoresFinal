using System;
using UnityEngine;

namespace Cryfine
{
    public static class DebugC
    {
        public static void Distance2GameObjects(GameObject point1, GameObject point2)
        {
            Debug.Log("[Cryfine Debug] Distance between "+ point1.name + " - " + point2.name+": "+MathC.DisMan3D(point1.transform.position.x, point1.transform.position.y, point1.transform.position.z, point2.transform.position.x, point2.transform.position.y, point2.transform.position.z));
        }
        public static void IsAtDistance(GameObject go1, GameObject go2, float distance)
        {
            Debug.Log("[CryFine Debug] "+ go1.name + " - "+go2.name+ " are in a range of "+ distance+": "+
            (MathC.DisSqr3D(go1.transform.position.x, go1.transform.position.y, go1.transform.position.z, go2.transform.position.x, go2.transform.position.y, go2.transform.position.z) < distance * distance));
        }
    }
}