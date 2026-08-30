using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif

// ---------- Class-level attributes ----------
[RequireComponent(typeof(Transform))]
[DisallowMultipleComponent]
[ExecuteInEditMode]
[ExecuteAlways]
[AddComponentMenu("Testing/Data Test Script")]
[SelectionBase]
[HelpURL("https://example.com/docs/DataTestScript")]
[Icon("Assets/Editor/DataTestIcon.png")]
public class DataTestScript : MonoBehaviour
{
    private static readonly System.Random _rng = new System.Random();

    // ---------- Basic / Primitive Types ----------
    [Header("Basic / Primitive Types")]
    [Space]

    [Tooltip("A plain 32-bit signed integer.")]
    [SerializeField]
    private int intProperty;

    [Tooltip("A single-precision floating point value.")]
    [Range(-100f, 100f)]
    public float floatProperty;

    [Tooltip("A double-precision floating point value.")]
    public double doubleProperty;

    [Tooltip("A simple boolean toggle.")]
    public bool boolProperty;

    [Tooltip("An arbitrary text string.")]
    [TextArea(2, 4)]
    public string stringProperty;

    [Tooltip("A multiline text blob.")]
    [Multiline(3)]
    public string multilineProperty;

    [Tooltip("A single character.")]
    public char charProperty;

    [Tooltip("A 64-bit signed integer.")]
    public long longProperty;

    [Tooltip("A delayed float field.")]
    [Delayed]
    public float delayedFloatProperty;

    [Tooltip("An integer constrained to a slider.")]
    [Range(0, 255)]
    public int rangedIntProperty;

    [Tooltip("A value with an enforced minimum.")]
    [Min(0f)]
    public float minValueProperty;

    [Tooltip("Serialized but hidden from the inspector.")]
    [HideInInspector]
    [SerializeField]
    private int hiddenSerializedProperty;

    [Tooltip("Not serialized by Unity.")]
    [NonSerialized]
    public int nonSerializedProperty;

    public enum ExampleEnum
    {
        [InspectorName("First Option")] Alpha,
        [InspectorName("Second Option")] Beta,
        [InspectorName("Third Option")] Gamma,
        [InspectorName("Fourth Option")] Delta
    }

    [Space]
    [Tooltip("A single-select enum with InspectorName-labelled members.")]
    public ExampleEnum enumProperty;

    // ---------- Vector / Math Types ----------
    [Header("Vector / Math Types")]
    [Space]

    [Tooltip("A 2D vector (x, y).")] public Vector2 vector2Property;
    [Tooltip("A 3D vector (x, y, z).")] public Vector3 vector3Property;
    [Tooltip("A 4D vector (x, y, z, w).")] public Vector4 vector4Property;
    [Tooltip("A 2D integer vector.")] public Vector2Int vector2IntProperty;
    [Tooltip("A 3D integer vector.")] public Vector3Int vector3IntProperty;
    [Tooltip("A rotation quaternion.")] public Quaternion quaternionProperty;
    [Tooltip("A full 4x4 transformation matrix.")] public Matrix4x4 matrix4x4Property;
    [Tooltip("A 2D rectangle.")] public Rect rectProperty;
    [Tooltip("A 2D integer rectangle.")] public RectInt rectIntProperty;
    [Tooltip("An axis-aligned bounding box.")] public Bounds boundsProperty;
    [Tooltip("An axis-aligned integer bounding box.")] public BoundsInt boundsIntProperty;

    // ---------- Colour Types ----------
    [Header("Colour Types")]
    [Space]

    [Tooltip("An RGBA float colour.")]
    public Color colorProperty;

    [Tooltip("An HDR colour with intensity support.")]
    [ColorUsage(true, true)]
    public Color hdrColorProperty;

    [Tooltip("A packed 32-bit RGBA colour.")]
    public Color32 color32Property;

    [Tooltip("An HDR gradient.")]
    [GradientUsage(true)]
    public Gradient gradientProperty;

    // ---------- Array / Collection Types ----------
    [Header("Array / Collection Types")]
    [Space]

    [Tooltip("A serialized integer array.")]
    public int[] arrayProperty;

    [Tooltip("A non-reorderable integer array.")]
    [NonReorderable]
    public int[] nonReorderableArray;

    [Tooltip("Not serialized by Unity (Dictionary unsupported).")]
    [NonSerialized]
    public Dictionary<string, int> dictionaryProperty;

    // ---------- Object / Reference Types ----------
    [Header("Object / Reference Types")]
    [Space]

    [Tooltip("A reference to any UnityEngine.Object.")]
    public UnityEngine.Object objectProperty;

    // ---------- Legacy / Renamed Fields ----------
    [Header("Legacy / Renamed Fields")]
    [Space]

    [Tooltip("Renamed from 'oldFloatName' - tests FormerlySerializedAs survival.")]
    [FormerlySerializedAs("oldFloatName")]
    public float renamedFloatProperty;

    private void Awake()
    {
        arrayProperty = new int[0];
        nonReorderableArray = new int[0];
        dictionaryProperty = new Dictionary<string, int>();
        gradientProperty = new Gradient();
        RandomizeAll();
    }

    // ================= Helpers =================
    private static float RandFloat(float min = -100f, float max = 100f)
        => (float)(_rng.NextDouble() * (max - min) + min);

    private static double RandDouble(double min = -1000.0, double max = 1000.0)
        => _rng.NextDouble() * (max - min) + min;

    private static int RandInt(int min = -100, int max = 100)
        => _rng.Next(min, max);

    private static byte RandByte() => (byte)_rng.Next(0, 256);

    private static float Rand01() => (float)_rng.NextDouble();

    // ================= Basic / Primitive =================
    public void RandomizeInt() => intProperty = RandInt();
    public void RandomizeFloat() => floatProperty = RandFloat(-100f, 100f);
    public void RandomizeDouble() => doubleProperty = RandDouble();
    public void RandomizeBool() => boolProperty = _rng.Next(0, 2) == 1;

    public void RandomizeString()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        int len = _rng.Next(4, 12);
        char[] buf = new char[len];
        for (int i = 0; i < len; i++)
            buf[i] = chars[_rng.Next(chars.Length)];
        stringProperty = new string(buf);
    }

    public void RandomizeChar() => charProperty = (char)('A' + _rng.Next(0, 26));

    public void RandomizeLong()
    {
        byte[] b = new byte[8];
        _rng.NextBytes(b);
        longProperty = BitConverter.ToInt64(b, 0);
    }

    public void RandomizeEnum()
    {
        var values = (ExampleEnum[])Enum.GetValues(typeof(ExampleEnum));
        enumProperty = values[_rng.Next(values.Length)];
    }

    // ================= Vector / Math =================
    public void RandomizeVector2() => vector2Property = new Vector2(RandFloat(), RandFloat());
    public void RandomizeVector3() => vector3Property = new Vector3(RandFloat(), RandFloat(), RandFloat());
    public void RandomizeVector4() => vector4Property = new Vector4(RandFloat(), RandFloat(), RandFloat(), RandFloat());
    public void RandomizeVector2Int() => vector2IntProperty = new Vector2Int(RandInt(), RandInt());
    public void RandomizeVector3Int() => vector3IntProperty = new Vector3Int(RandInt(), RandInt(), RandInt());
    public void RandomizeQuaternion() => quaternionProperty = UnityEngine.Random.rotationUniform;

    public void RandomizeMatrix4x4()
    {
        Matrix4x4 m = new Matrix4x4();
        for (int i = 0; i < 16; i++)
            m[i] = RandFloat();
        matrix4x4Property = m;
    }

    public void RandomizeRect()
        => rectProperty = new Rect(RandFloat(), RandFloat(), RandFloat(0f, 100f), RandFloat(0f, 100f));

    public void RandomizeRectInt()
        => rectIntProperty = new RectInt(RandInt(), RandInt(), RandInt(0, 100), RandInt(0, 100));

    public void RandomizeBounds()
        => boundsProperty = new Bounds(
            new Vector3(RandFloat(), RandFloat(), RandFloat()),
            new Vector3(RandFloat(0f, 50f), RandFloat(0f, 50f), RandFloat(0f, 50f)));

    public void RandomizeBoundsInt()
        => boundsIntProperty = new BoundsInt(
            new Vector3Int(RandInt(), RandInt(), RandInt()),
            new Vector3Int(RandInt(0, 50), RandInt(0, 50), RandInt(0, 50)));

    // ================= Colour =================
    public void RandomizeColor() => colorProperty = new Color(Rand01(), Rand01(), Rand01(), Rand01());
    public void RandomizeColor32() => color32Property = new Color32(RandByte(), RandByte(), RandByte(), RandByte());

    public void RandomizeGradient()
    {
        var g = new Gradient();
        var colorKeys = new GradientColorKey[2];
        colorKeys[0] = new GradientColorKey(new Color(Rand01(), Rand01(), Rand01()), 0f);
        colorKeys[1] = new GradientColorKey(new Color(Rand01(), Rand01(), Rand01()), 1f);
        var alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(Rand01(), 0f);
        alphaKeys[1] = new GradientAlphaKey(Rand01(), 1f);
        g.SetKeys(colorKeys, alphaKeys);
        gradientProperty = g;
    }

    // ================= Array / Collection =================
    public void RandomizeArray()
    {
        int len = _rng.Next(1, 8);
        arrayProperty = new int[len];
        for (int i = 0; i < len; i++)
            arrayProperty[i] = RandInt();
    }

    public void RandomizeDictionary()
    {
        dictionaryProperty = new Dictionary<string, int>();
        int count = _rng.Next(1, 6);
        for (int i = 0; i < count; i++)
            dictionaryProperty["key_" + i] = RandInt();
    }

    // ================= Object / Reference =================
    public void RandomizeObject()
    {
        var go = new GameObject("RandomObj_" + _rng.Next(0, 10000));
        objectProperty = go;
    }

    // ================= Randomize All =================
    [ContextMenu("Randomize All")]
    public void RandomizeAll()
    {
        RandomizeInt(); RandomizeFloat(); RandomizeDouble(); RandomizeBool();
        RandomizeString(); RandomizeChar(); RandomizeLong(); RandomizeEnum();
        RandomizeVector2(); RandomizeVector3(); RandomizeVector4();
        RandomizeVector2Int(); RandomizeVector3Int(); RandomizeQuaternion();
        RandomizeMatrix4x4(); RandomizeRect(); RandomizeRectInt();
        RandomizeBounds(); RandomizeBoundsInt();
        RandomizeColor(); RandomizeColor32(); RandomizeGradient();
        RandomizeArray(); RandomizeDictionary(); RandomizeObject();
        Debug.Log("[PropertyRandomizer] RandomizeAll() complete.");
    }

    [ContextMenu("Reset Int To Zero")]
    private void ResetIntToZero() => intProperty = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RuntimeInitBeforeScene()
        => Debug.Log("[DataTestScript] RuntimeInitializeOnLoadMethod (BeforeSceneLoad).");

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void RuntimeInitAfterScene()
        => Debug.Log("[DataTestScript] RuntimeInitializeOnLoadMethod (AfterSceneLoad).");

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RandomizeAll();
    }
}

// ---------- ScriptableObject to host CreateAssetMenu ----------
[CreateAssetMenu(fileName = "DataTestConfig", menuName = "Testing/Data Test Config", order = 100)]
public class DataTestConfig : ScriptableObject
{
    [Tooltip("Some serialized config value.")]
    public int configValue;
}

#if UNITY_EDITOR
// ---------- Editor-only class for InitializeOnLoad / callbacks ----------
[InitializeOnLoad]
public static class DataTestEditorHooks
{
    static DataTestEditorHooks()
    {
        Debug.Log("[DataTestEditorHooks] InitializeOnLoad static ctor.");
    }

    [InitializeOnLoadMethod]
    private static void OnInitializeOnLoadMethod()
        => Debug.Log("[DataTestEditorHooks] InitializeOnLoadMethod.");

    [MenuItem("Tools/Data Test/Log Hello")]
    private static void LogHello()
        => Debug.Log("[DataTestEditorHooks] MenuItem invoked.");

    [MenuItem("CONTEXT/DataTestScript/Randomize From Context")]
    private static void RandomizeFromContext(MenuCommand command)
    {
        var script = command.context as DataTestScript;
        if (script != null) script.RandomizeAll();
    }

    [DidReloadScripts]
    private static void OnScriptsReloaded()
        => Debug.Log("[DataTestEditorHooks] DidReloadScripts.");

    [OnOpenAsset]
    private static bool OnOpenAssetCallback(int instanceID, int line)
    {
        Debug.Log("[DataTestEditorHooks] OnOpenAsset for instanceID " + instanceID);
        return false;
    }
}
#endif