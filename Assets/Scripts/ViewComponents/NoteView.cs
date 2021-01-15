using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class NoteView : MonoBehaviour {
	[SerializeField] private TMP_InputField titleField = null;

	private Note note;
	private Vector2 mouseDownPosition;

	public Note Note {
		get => note;
		set {
			if (note != null) {
				note.OnTitleChanged -= UpdateTitle;
			}
			note = value;
			Title = note.Title;
			note.OnTitleChanged += UpdateTitle;
		}
	}

	public bool IsScene { get; set; } = true;

	public string Title {
		get => note.Title;
		set => note.Title = value;
	} 

	public SceneDetailView DetailView { get; set; }

	private void Start() {
		titleField.text = note.Title;
	}

	private void Update() {
		note.X = transform.position.x;
		note.Y = transform.position.y;
	}

	public void UpdateTitle(string oldTitle, string newTitle) {
		titleField.text = newTitle;
		gameObject.name = $"{note.ID}: {newTitle}";
	}

	public void MouseDown(BaseEventData eventData) {
		mouseDownPosition = Input.mousePosition;
	}

	public void MouseUp(BaseEventData eventData) {
		if (IsScene && Vector2.Distance(mouseDownPosition, Input.mousePosition) < 1) {
			ToggleDetailView();
		}
	}

	public void ToggleDetailView() {
		DetailView.Scene = note as Scene;
		DetailView.gameObject.SetActive(!DetailView.gameObject.activeSelf);
	}
}
