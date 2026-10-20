using UnityEngine;
using UnityEditor;

public class ColorizeWindow : EditorWindow
{
    Color color=Color.red;

    [MenuItem("Window/Cry-Fine/Colorize")]
    public static void ShowWindow()
    {
        GetWindow<ColorizeWindow>("Colorize");
    }

    void OnGUI()
    {
        color = EditorGUILayout.ColorField("Color", color);
        if (GUILayout.Button("Color Selected Object"))
        {
            Colorize();
        }
    }

    private void Colorize()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            Renderer renderer = go.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.sharedMaterial.color = color;
            }
        }
    }
}
