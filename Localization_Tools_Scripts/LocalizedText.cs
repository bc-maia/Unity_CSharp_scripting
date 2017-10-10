using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour 
{
	public string key;
	private Text text;

	void Awake() 
	{
		text = GetComponent<Text>();
		text.text = LocalizationManager.instance.GetLocalizedValue(key);
	}
}
