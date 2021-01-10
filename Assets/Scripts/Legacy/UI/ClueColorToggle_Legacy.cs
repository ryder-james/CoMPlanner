using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueColorToggle_Legacy : MonoBehaviour {
	[SerializeField] private Image buttonImage = null;
	[SerializeField] private Image clueImage = null;
	[SerializeField] private Color defaultColor = Color.white;
	[SerializeField] private Color secondaryColor = Color.black;

	private bool isDefaultColor = true;

	public void ToggleColor() {
		buttonImage.color = isDefaultColor ? defaultColor : secondaryColor;
		clueImage.color = isDefaultColor ? secondaryColor : defaultColor;

		isDefaultColor = !isDefaultColor;
	}
}
