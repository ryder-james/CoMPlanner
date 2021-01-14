using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClueView : MonoBehaviour {
	[SerializeField] private TMP_InputField textField = null;
	[SerializeField] private Color primaryColor = new Color(
		0.992f, 
		0.945f, 
		0.643f);
	[SerializeField] private Color secondaryColor = new Color(
		0.819f,
		0.529f,
		0.529f);
	[SerializeField] private Image backgroundImage = null;
	[SerializeField] private Image colorToggleButtonImage = null;

	private Clue clue;
	private bool isPrimaryColor = true;

	public Clue Clue {
		get => clue;
		set {
			clue = value;
			if (ColorUtility.TryParseHtmlString(clue.Color, out Color color)) {
				Color = color;
				isPrimaryColor = Color == primaryColor;
				colorToggleButtonImage.color = (
					isPrimaryColor ? secondaryColor : primaryColor);
			}
			textField.text = clue.Text;
		}
	}

	public SceneDetailView DetailView { get; set; }
	public string Text {
		get => Clue.Text;
		set {
			DetailView.UpdateClueText(ref clue, value);
		}
	}

	public Color Color {
		get => backgroundImage.color;
		set => backgroundImage.color = value;
	}

	private void Awake() {
		Color = primaryColor;
	}

	public void ToggleColor() {
		Color = isPrimaryColor ? secondaryColor : primaryColor;
		colorToggleButtonImage.color = isPrimaryColor ? primaryColor : secondaryColor;
		isPrimaryColor = !isPrimaryColor;
		DetailView.UpdateClueColor(ref clue, "#" + ColorUtility.ToHtmlStringRGB(Color));
	}
}
