using UnityEditor;
using UnityEngine;
using System.IO;

public class LocalizedTextEditor : EditorWindow
{
    public LocalizationData localizationData;

    [MenuItem("Window/Localized Text Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
    }

    void OnGUI()
    {
        if (localizationData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            if(GUILayout.Button("Save Data"))
                SaveGameData();
        }
        if (GUILayout.Button("Load Data"))
            LoadGameData();
                    
        if (GUILayout.Button("Create New Data"))
            CreateNewData();
    }
        
    private void CreateNewData()
    {
        localizationData = new LocalizationData();
    }

    private void LoadGameData() 
    {
        string filePath = EditorUtility.OpenFilePanel("Save Localization data file", Application.streamingAssetsPath,"json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath); // read from external json

            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson); // data deserialization
        }
    }

    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save Localization data file", Application.streamingAssetsPath, "", "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(localizationData); // data serialization

            File.WriteAllText(filePath, dataAsJson); // write into external json
        }
    }
}