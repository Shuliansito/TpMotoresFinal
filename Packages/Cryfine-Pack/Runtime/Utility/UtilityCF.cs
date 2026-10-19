using UnityEngine;

namespace CryFine
{
    public static class UtilityCF
    {
        private static GUIStyle labelStyle;
        public static Vector3 GetCenter(Transform target)
        {
            Renderer renderer = target.GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                return renderer.bounds.center;
            }
            return target.position;
        }
        public static Vector3 GetCenter(Renderer renderer, Transform target)
        {
            if (renderer != null)
            {
                return renderer.bounds.center;
            }
            return target.position;
        }
        public static void DrawLabel(Vector3 position, string text, Color color, int fontSize = 12)
        {
#if UNITY_EDITOR
            if (labelStyle == null)
            {
                labelStyle = new GUIStyle(UnityEditor.EditorStyles.boldLabel);
                labelStyle.alignment = TextAnchor.MiddleCenter;
            }
            labelStyle.normal.textColor = color;
            labelStyle.fontSize = fontSize;
            UnityEditor.Handles.Label(position, text, labelStyle);
#endif
        }
        public static void DrawRangeFlat(Vector3 center, float radius, Color color)
        {
#if UNITY_EDITOR
            Color previous = UnityEditor.Handles.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawWireDisc(center, Vector3.up, radius);
            UnityEditor.Handles.color = previous;
#endif
        }
        public static void DrawRangeSphere(Vector3 center, float radius, Color color, bool solid = false)
        {
            Color previous = Gizmos.color;
            Gizmos.color = color;

            if (solid)
            {
                Gizmos.DrawSphere(center, radius);
            }
            else
            {
                Gizmos.DrawWireSphere(center, radius);
            }

            Gizmos.color = previous;
        }
    }
}