using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
#if FPS_DISPLAY
    [SerializeField]
    private int targetFPS = 60;
    [SerializeField]
    private Color color = new Color(1.0f, 1.0f, 1.5f, 1.0f);
    private float deltaTime = 0.0f;

    private void Start()
    {
        DontDestroyOnLoad(this);
        Application.targetFrameRate = targetFPS;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = color;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
#endif
}