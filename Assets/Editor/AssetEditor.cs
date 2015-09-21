using UnityEngine;
using UnityEditor;
using System.IO;


class AssetEditor : Editor
{
    public static void CheckDictory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public static void CreateAsset(ScriptableObject obj, string path, string name)
    {
        CheckDictory(path);
        AssetDatabase.CreateAsset(obj, path + "/" + name + ".asset");
    }
}

