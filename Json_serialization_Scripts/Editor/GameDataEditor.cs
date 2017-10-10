using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameDataEditor : EditorWindow 
{
	public GameData gameData;

	/*	Getting file path
			private static string gameDataFileName = "data.json";
			private string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
		or */
	private string gameDataProjectFilePath = "/StreamingAssets/data.json";

	[MenuItem("Window/Game Data Editor")]
	static void Init()
	{
		// creating a editor windows of the Game Data Editor Type (GameDataEditor:EditorWindow:ScriptableObject)
		GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow(typeof(GameDataEditor));
		// customise window title
		GUIContent title = new GUIContent();
		title.text = "Data Editor";
		window.titleContent = title;
		// show window
		window.Show();
	}

	// OnGUI runs similar to Update callback
	void OnGUI()
	{
		if (gameData != null) // if game data has already being loaded
		{
			// object representation based on it's serializable data
			SerializedObject serializedObject = new SerializedObject(this); // this == game data editor instance
			SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");

			EditorGUILayout.PropertyField(serializedProperty, true); // true == display children data too

			serializedObject.ApplyModifiedProperties();		// if user has changed anything if user has made any modification

			if (GUILayout.Button("Save Data"))
				SaveGameData();
		}

		if(GUILayout.Button("Load Data"))
			LoadGameData();
	}

	private void LoadGameData()
	{
		string filePath = Application.dataPath + gameDataProjectFilePath;

		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			gameData = JsonUtility.FromJson<GameData>(dataAsJson);
		}
		else 
			gameData = new GameData();
	}

	private void SaveGameData()
	{
		string filePath = Application.dataPath + gameDataProjectFilePath;
		string dataAsJson = JsonUtility.ToJson(gameData);
		File.WriteAllText(filePath, dataAsJson);
	}
}
