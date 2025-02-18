using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement; // EditorSceneManager�̂��߂ɕK�v
using UnityEngine.SceneManagement; // Scene�Ǘ��̋@�\�̂��߂ɕK�v
using System;

// �G�f�B�^�[�g���̃N���X
public class AutoSaveConfig : ScriptableObject
{
    public bool isEnabled = true;
    public float saveInterval = 60f; // 5���i�b�P�ʁj
}

[InitializeOnLoad]
public class AutoSave
{
    private static AutoSaveConfig config;
    private static DateTime lastSaveTime;

    // �R���X�g���N�^�i�G�f�B�^�[�N�����Ɏ��s�j
    static AutoSave()
    {
        // �ݒ�̓ǂݍ���
        config = ScriptableObject.CreateInstance<AutoSaveConfig>();
        EditorApplication.update += Update;
        lastSaveTime = DateTime.Now;
    }

    static void Update()
    {
        if (!config.isEnabled)
            return;

        // �ݒ肳�ꂽ�Ԋu�ŕۑ������s
        if ((DateTime.Now - lastSaveTime).TotalSeconds >= config.saveInterval)
        {
            SaveAll();
            lastSaveTime = DateTime.Now;
        }
    }

    // �V�[���ƃA�Z�b�g�̕ۑ������s
    static void SaveAll()
    {
        if (!EditorApplication.isPlaying)
        {
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
            Debug.Log($"[AutoSave] �v���W�F�N�g�������ۑ����܂���: {DateTime.Now}");
        } 
    }
}

// �ݒ�p�̃G�f�B�^�[�E�B���h�E
public class AutoSaveSettingsWindow : EditorWindow
{
    private static AutoSaveConfig config;

    [MenuItem("Tools/Auto Save/Settings")]
    public static void ShowWindow()
    {
        GetWindow<AutoSaveSettingsWindow>("Auto Save Settings");
    }

    void OnGUI()
    {
        if (config == null)
            config = ScriptableObject.CreateInstance<AutoSaveConfig>();

        config.isEnabled = EditorGUILayout.Toggle("Auto Save Enabled", config.isEnabled);
        config.saveInterval = EditorGUILayout.FloatField("Save Interval (seconds)", config.saveInterval);

        if (GUILayout.Button("�ݒ�̕ۑ�"))
        {
            config = ScriptableObject.CreateInstance<AutoSaveConfig>();
            Debug.Log("�ۑ�");
        }
    }
}
