using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using CasePlanner.Data.Notes;

public class NoteView : MonoBehaviour {
	[SerializeField] private TMP_InputField titleField = null;
	[SerializeField] private Pin pin = null;

	private List<EdgeView> edges;
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
	public Pin Pin { get => pin; set => pin = value; }

	private void Awake() {
		edges = new List<EdgeView>();
	}

	private void Start() {
		titleField.text = note.Title;
		gameObject.name = $"{note.ID}: ";
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

	public void ConnectEdge(ref EdgeView edge) {
		edges.Add(edge);
	}

	public void DisconnectEdge(EdgeView edge) {
		edges.Remove(edge);
		note.Disconnect(edge.A.Note.ID == note.ID ? edge.B.Note.ID : edge.A.Note.ID);
	}

	private void OnDestroy() {
		foreach (EdgeView edge in edges) {
			if (edge.A.Note.ID == note.ID) {
				edge.B.DisconnectEdge(edge);
			} else {
				edge.A.DisconnectEdge(edge);
			}

			Destroy(edge.gameObject);
		}
	}
}
