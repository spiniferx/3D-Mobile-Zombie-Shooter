using UnityEngine;
using UnityEditor;

public class EditorFields : Editor
{
    public static void DrawLabel(string text)
    {
        GUILayout.Label(text);
    }

    public static void DrawInt(int value)
    {
        value = EditorGUILayout.IntField(value);
    }

    public static void DrawFloat(float value)
    {
        value = EditorGUILayout.FloatField(value);
    }

    public static void DrawString(string text)
    {
        text = EditorGUILayout.TextField(text);
    }

    public static void DrawObject(Object obj, bool allowSceneObjects)
    {
        obj = (Object)EditorGUILayout.ObjectField(obj, typeof(Object), false);
    }
}
