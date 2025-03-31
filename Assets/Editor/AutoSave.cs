using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;
using System.IO;

// エディター拡張のクラス
public class AutoSaveConfig : ScriptableObject
{
    public bool isEnabled = true;
    public float saveInterval = 60f; // デフォルト60秒

    // 設定アセットのパス
    private const string ASSET_PATH = "Assets/Editor/AutoSaveConfig.asset";

    // 設定の読み込み
    public static AutoSaveConfig GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<AutoSaveConfig>(ASSET_PATH);
        if (settings == null)
        {
            // 設定ファイルが存在しない場合は新規作成
            settings = ScriptableObject.CreateInstance<AutoSaveConfig>();

            // ディレクトリが存在しない場合は作成
            string directoryPath = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            AssetDatabase.CreateAsset(settings, ASSET_PATH);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }

    // 設定の保存
    public void Save()
    {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
}

[InitializeOnLoad]
public class AutoSave
{
    private static AutoSaveConfig config;
    private static DateTime lastSaveTime;

    // コンストラクタ（エディター起動時に実行）
    static AutoSave()
    {
        // 設定の読み込み
        config = AutoSaveConfig.GetOrCreateSettings();
        EditorApplication.update += Update;
        lastSaveTime = DateTime.Now;
    }

    static void Update()
    {
        if (!config.isEnabled)
            return;

        // 設定された間隔で保存を実行
        if ((DateTime.Now - lastSaveTime).TotalSeconds >= config.saveInterval)
        {
            SaveAll();
            lastSaveTime = DateTime.Now;
        }
    }

    // シーンとアセットの保存を実行
    static void SaveAll()
    {
        if (!EditorApplication.isPlaying)
        {
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
            Debug.Log($"[AutoSave] プロジェクトを自動保存しました: {DateTime.Now}");
        }
    }
}

// 設定用のエディターウィンドウ
public class AutoSaveSettingsWindow : EditorWindow
{
    private AutoSaveConfig config;

    [MenuItem("Tools/Auto Save/Settings")]
    public static void ShowWindow()
    {
        GetWindow<AutoSaveSettingsWindow>("Auto Save Settings");
    }

    void OnEnable()
    {
        // ウィンドウが開かれた時に設定を読み込む
        config = AutoSaveConfig.GetOrCreateSettings();
    }

    void OnGUI()
    {
        if (config == null)
        {
            config = AutoSaveConfig.GetOrCreateSettings();
        }

        EditorGUI.BeginChangeCheck();

        config.isEnabled = EditorGUILayout.Toggle("Auto Save Enabled", config.isEnabled);
        config.saveInterval = EditorGUILayout.FloatField("Save Interval (seconds)", config.saveInterval);

        if (EditorGUI.EndChangeCheck())
        {
            // 値が変更された場合はマークを付ける
            EditorUtility.SetDirty(config);
        }
    }
}