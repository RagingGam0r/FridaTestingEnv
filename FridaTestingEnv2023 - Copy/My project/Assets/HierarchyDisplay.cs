using System.Text;
using UnityEngine;

// Attach to any GameObject. Renders the full scene hierarchy to an on-screen
// OnGUI panel at runtime. Toggle with the H key.
public class HierarchyDisplay : MonoBehaviour
{
    [Header("Layout")]
    public int panelWidth = 350;
    public int fontSize = 12;
    public int indentSize = 16;
    public KeyCode toggleKey = KeyCode.H;

    [Header("Options")]
    public bool includeInactive = true;
    public bool showComponents = false;

    private bool _visible = true;
    private Vector2 _scroll;
    private GUIStyle _style;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            _visible = !_visible;
    }

    void OnGUI()
    {
        if (!_visible) return;

        if (_style == null)
        {
            _style = new GUIStyle(GUI.skin.label)
            {
                fontSize = fontSize,
                richText = true,
                alignment = TextAnchor.UpperLeft
            };
        }

        var sb = new StringBuilder();
        foreach (var root in GetRootObjects())
            AppendObject(sb, root.transform, 0);

        float panelHeight = Screen.height - 20;
        GUILayout.BeginArea(new Rect(10, 10, panelWidth, panelHeight),
            GUI.skin.box);
        GUILayout.Label($"<b>Hierarchy</b>  (toggle: {toggleKey})", _style);
        _scroll = GUILayout.BeginScrollView(_scroll);
        GUILayout.Label(sb.ToString(), _style);
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void AppendObject(StringBuilder sb, Transform t, int depth)
    {
        if (!includeInactive && !t.gameObject.activeInHierarchy) return;

        string indent = new string(' ', depth * (indentSize / 4));
        string color = t.gameObject.activeInHierarchy ? "white" : "grey";
        sb.AppendLine($"{indent}<color={color}>{t.name}</color>");

        if (showComponents)
        {
            foreach (var c in t.GetComponents<Component>())
            {
                if (c == null) continue;
                sb.AppendLine($"{indent}  <color=#88ccff>• {c.GetType().Name}</color>");
            }
        }

        for (int i = 0; i < t.childCount; i++)
            AppendObject(sb, t.GetChild(i), depth + 1);
    }

    GameObject[] GetRootObjects()
    {
        return UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().GetRootGameObjects();
    }
}