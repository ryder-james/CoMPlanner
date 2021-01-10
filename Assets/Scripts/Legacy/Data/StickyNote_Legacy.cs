using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CasePlanner.UI;
using Common.UI;
using System.Linq;

namespace CasePlanner.Data.Notes {
	public class StickyNote_Legacy : MonoBehaviour, ISerializable<StickyNote_Legacy.SerializeNote> {
		[System.Serializable]
		public struct SerializeNote {
			public int id;
			public string title, notes, description;
			public Vector3 position;
			public string[] clues;
		}

		[SerializeField]
		private TMP_InputField titleField = null;

		[SerializeField]
		private Pin_Legacy pin = null;

		private string title = "";
		private Vector3 mouseDownPos;

		[SerializeField]
		public GameObject Viewer { get; set; }

		public string Title {
			get => title;
			set {
				title = value.Trim();
				titleField.text = title;
				gameObject.name = $"{ID}: {title}";
			}
		}

		public int ID { get; set; }
		public string Notes { get; set; } = "";
		public string Description { get; set; } = "";
		public List<string> Clues { get; set; } = new List<string>();

		public List<Yarn_Legacy> Edges { get; } = new List<Yarn_Legacy>();

		public Pin_Legacy Pin { get => pin; set => pin = value; }

		public void Connect(Yarn_Legacy yarn) {
			Edges.Add(yarn);
		}

		public void Remove(Yarn_Legacy yarn) {
			Edges.Remove(yarn);
		}

		public void UpdateTitle() {
			title = titleField.text;
			gameObject.name = $"{ID}: {title}";
		}

		public void View() {
			Viewer.GetComponent<StickyNoteViewer_Legacy>().Note = this;
			Viewer.SetActive(true);
		}

		public void MouseDown() {
			mouseDownPos = Input.mousePosition;
		}

		public void MouseUp() {
			if (mouseDownPos == Input.mousePosition) {
				GetComponent<DraggableUI>().IsHeld = false;
				View();
			}
		}

		private void OnDestroy() {
			foreach (Yarn_Legacy yarn in Edges) {
				if (yarn.A == this) {
					yarn.B.Remove(yarn);
				} else {
					yarn.A.Remove(yarn);
				}
				Destroy(yarn.gameObject);
			}
		}

		public SerializeNote Serialized() {
			SerializeNote serialized = new SerializeNote() {
				id = ID,
				title = Title,
				description = Description,
				notes = Notes,
				position = transform.position,
				clues = Clues.ToArray()
			};

			return serialized;
		}

		public void Deserialize(SerializeNote obj) {
			ID = obj.id;
			Title = obj.title;
			Description = obj.description;
			Notes = obj.notes;
			transform.position = obj.position;
			Pin.Note = this;
			Clues = new List<string>(obj.clues);
		}
	}
}
