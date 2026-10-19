using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public static class CreateCafeScene
{
    [MenuItem("Cafetería/Crear escena AR")]
    public static void CreateScene()
    {
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        Selection.activeObject = null;
        if (!EditorApplication.ExecuteMenuItem("GameObject/XR/AR Session"))
        {
            Debug.LogError("No se pudo crear AR Session. Comprueba que AR Foundation esté instalado.");
            return;
        }
        Selection.activeObject = null;
        if (!EditorApplication.ExecuteMenuItem("GameObject/XR/XR Origin (Mobile AR)"))
        {
            Debug.LogError("No se pudo crear XR Origin (Mobile AR). Comprueba AR Foundation 5.0.6.");
            return;
        }
        var camera = Camera.main;
        if (camera == null) camera = Object.FindObjectOfType<Camera>();
        if (camera == null)
        {
            Debug.LogError("XR Origin no creó la AR Camera.");
            return;
        }
        if (camera.GetComponent<ARCameraManager>() == null) camera.gameObject.AddComponent<ARCameraManager>();
        if (camera.GetComponent<ARCameraBackground>() == null) camera.gameObject.AddComponent<ARCameraBackground>();
        camera.name = "AR Camera";
        var session = Object.FindObjectOfType<ARSession>();
        if (session != null && session.GetComponent<ARInputManager>() == null)
            session.gameObject.AddComponent<ARInputManager>();

        const string materialPath = "Assets/Materials/QrUnlit.mat";
        Directory.CreateDirectory("Assets/Materials");
        var material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
        if (material == null)
        {
            var shader = Shader.Find("Unlit/Texture");
            if (shader == null) { Debug.LogError("Falta el shader Unlit/Texture."); return; }
            material = new Material(shader) { name = "QR · sin sombras" };
            AssetDatabase.CreateAsset(material, materialPath);
        }
        const string texturePath = "Assets/Resources/menu-qr.png";
        var importer = AssetImporter.GetAtPath(texturePath) as TextureImporter;
        if (importer != null)
        {
            importer.filterMode = FilterMode.Point;
            importer.mipmapEnabled = false;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.SaveAndReimport();
        }
        var presenter = new GameObject("Bruma Café · QR en realidad aumentada").AddComponent<ARMenuQrPresenter>();
        presenter.Configure(camera, material);
        PlayerSettings.productName = "Bruma Café RA";
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
        const string scenePath = "Assets/MenuAR.unity";
        var scene = EditorSceneManager.GetActiveScene();
        EditorSceneManager.SaveScene(scene, scenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(scenePath, true) };
        AssetDatabase.SaveAssets();
        Debug.Log("Escena AR creada. Activa ARCore en Project Settings > XR Plug-in Management > Android y compila para Android.");
    }
}
