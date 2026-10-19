using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class CreateCafeScene
{
    [MenuItem("Cafetería/Crear escena del menú")]
    public static void CreateScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        var root = new GameObject("Bruma Café · Menú");
        root.AddComponent<CafeteriaMenu>();
        const string path = "Assets/Menu.unity";
        EditorSceneManager.SaveScene(scene, path);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(path, true) };
        AssetDatabase.SaveAssets();
        Debug.Log("Escena lista: " + path + ". Pulsa Play o genera WebGL.");
    }
}
