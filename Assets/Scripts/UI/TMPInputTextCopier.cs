using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPInputTextCopier : MonoBehaviour {
	[SerializeField] private TMP_InputField inputField = null;
	[SerializeField] private TMP_Text outputField = null;

	public void UpdateText() {
		outputField.text = inputField.text;
		if (inputField.text.EndsWith("\n")) {
			outputField.text += " ";
		}
	}
}
