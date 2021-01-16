using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using CasePlanner.Data.Views;
using CasePlanner.Data.Components;

namespace CasePlanner.Management {
	public class NoteManager : MonoBehaviour {
		[SerializeField] private SceneDetailView detailView = null;
		[SerializeField] private Transform spawnLocation = null;
		[SerializeField] private Transform stickyNoteParent = null;
		[SerializeField] private GameObject stickyNoteBase = null;
		[SerializeField] private bool shouldCreateScenes = true;

		private List<NoteView> notes;

		public Series ActiveSeries { get; set; }
		public Case ActiveCase => ActiveSeries.Cases[ActiveCaseIndex];
		public int ActiveCaseIndex { get; set; } = 0;
		public NoteView[] Notes => notes.ToArray();

		private void Start() {
			notes = new List<NoteView>();
		}

		public Note CreateViewNote(Note note = null) {
			Transform parent = stickyNoteParent != null ? stickyNoteParent : transform;
			Vector3 position;
			if (note == null) {
				Vector2 offset = Random.insideUnitCircle * 50;
				position = spawnLocation.position + new Vector3(offset.x, offset.y, 0);
				position.z = 0;
			} else {
				position = new Vector3(note.X, note.Y, 0);
			}

			GameObject noteGO = Instantiate(stickyNoteBase, position,
				Quaternion.identity, parent);
			NoteView view = noteGO.GetComponent<NoteView>();
			view.DetailView = detailView;
			view.IsScene = shouldCreateScenes;
			if (note != null) {
				view.Note = note;
			} else {
				if (shouldCreateScenes) {
					view.Note = new Scene();
				} else {
					view.Note = new Case();
				}
			}

			if (shouldCreateScenes) {
				ActiveCase.AddScene(view.Note as Scene);
			} else {
				ActiveSeries.AddCase(view.Note as Case);
			}

			notes.Add(view);
			return view.Note;
		}

		public void CreateNote() {
			CreateViewNote();
		}

		public void DestroyNote(NoteView view, bool immediate = false) {
			notes.Remove(view);
			if (shouldCreateScenes) {
				ActiveCase.RemoveScene(view.Note as Scene);
			} else {
				ActiveSeries.RemoveCase(view.Note as Case);
			}
			if (immediate) {
				DestroyImmediate(view.gameObject);
			} else {
				Destroy(view.gameObject);
			}
		}

		public void ClearNotes() {
			foreach (NoteView view in Notes) {
				DestroyNote(view, true);
			}
		}

		public void PopulateNotes() {
			if (shouldCreateScenes) {
				foreach (Scene scene in ActiveCase.Scenes) {
					CreateViewNote(scene);
				}
			}
		}

		public NoteView GetNoteByID(int id) {
			return notes.Where(n => n.Note.ID == id).FirstOrDefault();
		}
	}
}