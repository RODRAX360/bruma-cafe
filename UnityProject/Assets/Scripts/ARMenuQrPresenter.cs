using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>Places the menu QR as a real-world panel once ARCore starts tracking.</summary>
public sealed class ARMenuQrPresenter : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject panel;
    private string menuUrl;
    private GUIStyle titleStyle;
    private GUIStyle noteStyle;

    public void Configure(Camera camera, GameObject qrPanel)
    {
        arCamera = camera;
        panel = qrPanel;
    }

    private void Awake()
    {
        var url = Resources.Load<TextAsset>("menu-url");
        menuUrl = url != null ? url.text.Trim() : "";
        if (panel == null)
        {
            Debug.LogError("Falta el panel QR. Vuelve a crear la escena desde Cafetería > Crear escena AR.");
            return;
        }
        panel.SetActive(false);
    }

    private void Update()
    {
        if (panel != null && !panel.activeSelf && ARSession.state == ARSessionState.SessionTracking)
            Recenter();
    }

    private void Recenter()
    {
        if (arCamera == null || panel == null) return;
        Transform cameraTransform = arCamera.transform;
        panel.transform.position = cameraTransform.position + cameraTransform.forward * 0.85f;
        panel.transform.rotation = Quaternion.LookRotation(cameraTransform.position - panel.transform.position, Vector3.up);
        panel.SetActive(true);
    }

    private void OnGUI()
    {
        if (arCamera == null) return;
        int font = Mathf.Clamp(Screen.width / 27, 18, 38);
        if (titleStyle == null)
        {
            titleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, wordWrap = true };
            titleStyle.normal.textColor = Color.white;
            noteStyle = new GUIStyle(titleStyle) { fontStyle = FontStyle.Normal };
        }
        titleStyle.fontSize = font;
        noteStyle.fontSize = Mathf.Max(15, font - 10);
        float margin = Screen.width * 0.05f;
        float width = Screen.width - 2 * margin;
        GUI.color = new Color(0.29f, 0.19f, 0.15f, 0.86f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height * 0.16f), Texture2D.whiteTexture);
        GUI.color = Color.white;
        GUI.Label(new Rect(margin, Screen.height * 0.025f, width, Screen.height * 0.065f), "BRUMA CAFÉ · MENÚ EN RA", titleStyle);
        string status = panel != null && panel.activeSelf
            ? "Escanea el QR desde otro móvil o abre la carta aquí."
            : "Mueve el teléfono lentamente para iniciar la cámara RA.";
        GUI.Label(new Rect(margin, Screen.height * 0.09f, width, Screen.height * 0.055f), status, noteStyle);

        float buttonHeight = Mathf.Clamp(Screen.height * 0.065f, 50, 90);
        GUI.skin.button.fontSize = Mathf.Clamp(font - 5, 16, 34);
        float bottom = Screen.height - buttonHeight - Screen.height * 0.055f;
        if (GUI.Button(new Rect(margin, bottom, width * 0.47f, buttonHeight), "Centrar QR"))
            Recenter();
        if (GUI.Button(new Rect(margin + width * 0.52f, bottom, width * 0.48f, buttonHeight), "Abrir carta") &&
            !string.IsNullOrEmpty(menuUrl))
            Application.OpenURL(menuUrl);
    }
}
