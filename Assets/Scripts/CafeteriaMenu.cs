using System;
using System.Collections.Generic;
using UnityEngine;

public class CafeteriaMenu : MonoBehaviour
{
    private readonly List<string> categories = new List<string>();
    private readonly List<List<string>> entries = new List<List<string>>();
    private Vector2 scroll;
    private int selected;
    private GUIStyle titleStyle, subtitleStyle, headingStyle, itemStyle, priceStyle, buttonStyle, noteStyle;
    private Texture2D qr;
    private string menuUrl = "";

    private void Awake()
    {
        var menu = Resources.Load<TextAsset>("menu");
        if (menu != null)
        {
            foreach (var raw in menu.text.Split('\n'))
            {
                string line = raw.Trim();
                if (line.Length == 0) continue;
                if (line.StartsWith("#"))
                {
                    categories.Add(line.Substring(1));
                    entries.Add(new List<string>());
                }
                else if (entries.Count > 0) entries[entries.Count - 1].Add(line);
            }
        }
        var config = Resources.Load<TextAsset>("menu-url");
        if (config != null) menuUrl = config.text.Trim();
        qr = Resources.Load<Texture2D>("menu-qr");
    }

    private void Styles()
    {
        if (titleStyle != null) return;
        titleStyle = new GUIStyle(GUI.skin.label) { fontSize = 48, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };
        subtitleStyle = new GUIStyle(GUI.skin.label) { fontSize = 19, alignment = TextAnchor.MiddleCenter };
        headingStyle = new GUIStyle(GUI.skin.label) { fontSize = 28, fontStyle = FontStyle.Bold };
        itemStyle = new GUIStyle(GUI.skin.label) { fontSize = 19, wordWrap = true };
        priceStyle = new GUIStyle(itemStyle) { alignment = TextAnchor.MiddleRight, fontStyle = FontStyle.Bold };
        buttonStyle = new GUIStyle(GUI.skin.button) { fontSize = 17, fixedHeight = 42 };
        noteStyle = new GUIStyle(GUI.skin.label) { fontSize = 14, wordWrap = true, alignment = TextAnchor.MiddleCenter };
        foreach (var style in new[] { titleStyle, subtitleStyle, headingStyle, itemStyle, priceStyle, noteStyle })
            style.normal.textColor = new Color(0.29f, 0.19f, 0.15f);
    }

    private void OnGUI()
    {
        Styles();
        GUI.color = new Color(0.97f, 0.94f, 0.89f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        GUI.color = Color.white;
        float width = Mathf.Min(Screen.width - 32, 820);
        Rect box = new Rect((Screen.width - width) / 2, 18, width, Screen.height - 36);
        GUILayout.BeginArea(box);
        GUILayout.Label("BRUMA", titleStyle);
        GUILayout.Label("CAFÉ  ·  NUESTRA CARTA", subtitleStyle);
        GUILayout.Space(18);
        if (categories.Count == 0) GUILayout.Label("No se encontró menu.txt en Resources.");
        else
        {
            scroll = GUILayout.BeginScrollView(scroll);
            GUILayout.BeginHorizontal();
            for (int i = 0; i < categories.Count; i++)
            {
                if (i > 0 && i % 3 == 0) { GUILayout.EndHorizontal(); GUILayout.BeginHorizontal(); }
                if (GUILayout.Button(categories[i], buttonStyle, GUILayout.ExpandWidth(true))) selected = i;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(22);
            GUILayout.Label(categories[selected].ToUpperInvariant(), headingStyle);
            GUILayout.Space(8);
            foreach (string item in entries[selected])
            {
                var parts = item.Split('|');
                if (parts.Length != 2) continue;
                GUILayout.BeginHorizontal();
                GUILayout.Label(parts[0], itemStyle, GUILayout.ExpandWidth(true));
                GUILayout.Label("S/ " + parts[1], priceStyle, GUILayout.Width(86));
                GUILayout.EndHorizontal();
                GUILayout.Space(7);
            }
            GUILayout.Space(15);
            if (!string.IsNullOrEmpty(menuUrl) && menuUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                if (GUILayout.Button("ABRIR CARTA EN LA WEB", buttonStyle))
                    Application.OpenURL(menuUrl);
                if (qr != null)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(qr, GUIStyle.none, GUILayout.Width(180), GUILayout.Height(180));
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            }
            else GUILayout.Label("El enlace público de la carta está pendiente de configurar.", noteStyle);
            GUILayout.EndScrollView();
        }
        GUILayout.EndArea();
    }
}
