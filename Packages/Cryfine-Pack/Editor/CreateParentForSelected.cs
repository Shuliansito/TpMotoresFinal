using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cryfine
{
    public class CreateParentForSelected
    {
        [MenuItem("Tools/Cry-Fine/Create Parent for Selected")]
        public static void CreateParent() {

            var transforms = new List<Transform>(UnityEditor.Selection.transforms);

            if (transforms.Count <= 0) { Debug.LogWarning("[Cry-Fine] Any GameObject/s selected to create a parent"); return; }

            var parentObject = new GameObject("Generic Parent");
            Undo.RegisterCreatedObjectUndo(parentObject, "Create Parent for Selected");

            transforms.Sort((a, b) => { return a.GetSiblingIndex().CompareTo(b.GetSiblingIndex()); });

            var topMostParent = transforms[0].parent;

            foreach(var t in transforms) {
                if (t.parent != topMostParent) topMostParent = null;
            }

            parentObject.transform.SetParent(topMostParent, false);

            foreach(var t in transforms) {
                Undo.SetTransformParent(t, parentObject.transform, "Create Parent for Selected");
            }

            UnityEditor.Selection.activeGameObject = parentObject;
        }
    }
}
