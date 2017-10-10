using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour 
{
    public static LocalizationManager instance = null;

    private Dictionary<string,string> localizedText;

    private string missingTextString = "Localized text not found.";

    private bool isReady = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string,string>();

        fileName += ".json";

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            foreach (LocalizationItem item in loadedData.items)
            {
                localizedText.Add(item.key, item.value);
            }
            Debug.Log("Data Loaded from: " + fileName + ", dictionary contains:" + localizedText.Count + " entries.");
        }
        else
            Debug.LogError("Cannot find localization text file.");

        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;

        if (localizedText.ContainsKey(key))
            result = localizedText[key];

        return result;
    }

    public bool GetIsRead()
    {
        return isReady;
    }
}