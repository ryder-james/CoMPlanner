using UnityEngine;
using UnityEngine.UI;
using TMPro;

using JCommon.UI;

using CasePlanner.Management;
using CasePlanner.Data.Components;

namespace CasePlanner.Data.Views {
	public class SceneDetailView : MonoBehaviour {
		[SerializeField] private ClueCreator clueCreator = null;
		[SerializeField] private TMP_InputField titleField = null;
		[SerializeField] private TMP_InputField notesField = null;
		[SerializeField] private TMP_InputField descriptionField = null;
		[SerializeField] private GameObject editButton = null;
		[SerializeField] private GameObject saveButton = null;
		[SerializeField] private GameObject cancelButton = null;
		[SerializeField] private PanCamera panCam = null;

		private Scene scene;

		public string Title {
			get => scene.Title;
			set {
				scene.Title = value;
				titleField.text = scene.Title;
			}
		}

		public Scene Scene {
			get => scene;
			set {
				scene = value;
				Title = scene.Title;
			}
		}

		public void UpdateNotes() {
			scene.Notes = notesField.text;
		}

		public void UpdateClueText(ref Clue clue, string newText) {
			scene.UpdateClueText(ref clue, newText);
		}

		public void UpdateClueColor(ref Clue clue, string newColor) {
			scene.UpdateClueColor(ref clue, newColor);
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
			scene.Description = descriptionField.text;
			SetEditMode(false);
		}

		public void CancelEdit() {
			descriptionField.text = scene.Description;
			SetEditMode(false);
		}

		private void UpdateDetails() {
			if (scene == null) {
				return;
			}

			titleField.text = scene.Title;
			descriptionField.text = scene.Description;
			notesField.text = scene.Notes;

			Clue[] clues = scene.Clues;

			clueCreator.ClearClues();

			for (int i = 0; i < clues.Length; i++) {
				clueCreator.CreateClue(ref clues[i]);
			}
		}

		private void OnEnable() {
			panCam.CamEnabled = false;
			UpdateDetails();
		}

		private void OnDisable() {
			panCam.CamEnabled = true;
		}
	}
}