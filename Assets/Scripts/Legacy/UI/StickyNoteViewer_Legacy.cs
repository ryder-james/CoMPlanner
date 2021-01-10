using UnityEngine;
using TMPro;
using CasePlanner.Data.Notes;
using UnityEngine.UI;
using Common.UI;
using CasePlanner.Data;

namespace CasePlanner.UI {
	public class StickyNoteViewer_Legacy : MonoBehaviour {
		[SerializeField]
		private ClueCreator_Legacy clueCreator = null;

		[SerializeField]
		private TMP_InputField titleField = null;

		[SerializeField]
		private TMP_InputField notesField = null;

		[SerializeField]
		private TMP_InputField descriptionField = null;

		[SerializeField]
		private GameObject editButton = null;

		[SerializeField]
		private GameObject saveButton = null;

		[SerializeField]
		private GameObject cancelButton = null;

		public StickyNote_Legacy Note { get; set; }

		private PanCamera panCam;
		private PanCamera PanCam {
			get {
				if (panCam == null) {
					panCam = Camera.main.GetComponent<PanCamera>();
				}

				return panCam;
			}
		}

		private void Start() {
			gameObject.SetActive(false);
		}

		public void UpdateNoteTitle() {
			Note.Title = titleField.text;
		}

		public void UpdateNoteDescription() {
			Note.Description = descriptionField.text;
		}

		public void UpdateNotes() {
			Note.Notes = notesField.text;
		}

		public void CreateClue() {
			clueCreator.CreateClue();
		}

		public bool UpdateClue(string oldText, string newText) {
			int i = Note.Clues.IndexOf(oldText);
			if (i > -1) {
				if (newText == "") {
					Note.Clues.RemoveAt(i);
					return false;
				} else {
					Note.Clues[i] = newText;
					return true;
				}
			} else if (newText != "") {
				Note.Clues.Add(newText);
				return true;
			}

			return false;
		}

		public void SetEditMode(bool editMode) {
			if (editMode) {
				descriptionField.GetComponent<Image>().enabled = true;
				descriptionField.readOnly = false;
				descriptionField.richText = false;
				editButton.SetActive(false);
				saveButton.SetActive(true);
				cancelButton.SetActive(true);
			} else {
				descriptionField.GetComponent<Image>().enabled = false;
				descriptionField.readOnly = true;
				descriptionField.richText = true;
				editButton.SetActive(true);
				saveButton.SetActive(false);
				cancelButton.SetActive(false);
			}
		}

		public void SaveEdit() {
			Note.Description = descriptionField.text;
			SetEditMode(false);
		}

		public void CancelEdit() {
			descriptionField.text = Note.Description;
			SetEditMode(false);
		}

		private void UpdateView() {
			if (Note == null) {
				return;
			}
			titleField.text = Note.Title;
			notesField.text = Note.Notes;
			descriptionField.text = Note.Description;

			for (int i = 0; i < clueCreator.transform.childCount; i++) {
				Destroy(clueCreator.transform.GetChild(i).gameObject);
			}

			for (int i = 0; i < Note.Clues.Count; i++) {
				clueCreator.CreateClue(Note.Clues[i]);
			}
		}

		private void OnEnable() {
			PanCam.enabled = false;
			UpdateView();
		}

		private void OnDisable() {
			PanCam.enabled = true;
		}
	}
}