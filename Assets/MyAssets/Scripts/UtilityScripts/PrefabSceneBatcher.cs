#if UNITY_EDITOR

using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

class PrefabSceneBatcher : ScriptableObject
{

    [MenuItem("Assets/Create Prefab-Scene Batcher")]
    static void CreateAssetWithSelection()
    {
        string folder = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(folder))
        {
            folder = "Assets";
        }
        else if (!Directory.Exists(folder))
        {
            folder = Path.GetDirectoryName(folder);
        }
        AssetDatabase.CreateAsset(Selection.activeObject = CreateInstance<PrefabSceneBatcher>(), folder + "/Batcher.asset");
    }

    [CustomEditor(typeof(PrefabSceneBatcher))]
    class CustomIntpector : Editor
    {

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_prefab"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_scenes"));
            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Batch") && EditorUtility.DisplayDialog("Batch", "Can't be undo!", "Batch", "Cancel"))
            {
                (target as PrefabSceneBatcher).Batch();
            }
        }
    }

    void Batch()
    {
        foreach (SceneAsset sceneAsset in m_scenes)
        {
            Scene scene = EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(sceneAsset), OpenSceneMode.Additive);
            PrefabUtility.InstantiatePrefab(m_prefab, scene);
            EditorSceneManager.SaveScene(scene);
            EditorSceneManager.CloseScene(scene, true);
        }
    }

    [SerializeField] GameObject m_prefab = null;
    [SerializeField] SceneAsset[] m_scenes = new SceneAsset[0];
}

#endif
