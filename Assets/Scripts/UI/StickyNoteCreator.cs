using UnityEngine;
using CasePlanner.Data.Notes;

namespace CasePlanner.UI {
	public class StickyNoteCreator : MonoBehaviour {
		[SerializeField] private Transform spawnLocation = null;
		[SerializeField] private Canvas stickyNoteParent = null;
		[SerializeField] private GameObject stickyNoteBase = null;
		[SerializeField] private GameObject stickyNoteViewer = null;

		public int NoteCount { get; set; } = 0;

		public StickyNote CreateNote() {
			Transform parent = stickyNoteParent != null ? stickyNoteParent.transform : transform.parent;
			Vector3 position = spawnLocation.position;
			position.z = 0;
			GameObject noteObj = Instantiate(stickyNoteBase, position, Quaternion.identity, parent);
			StickyNote note = noteObj.GetComponent<StickyNote>();
			note.Viewer = stickyNoteViewer;
			note.ID = NoteCount++;
			return note;
		}

		public void CreateNoteButton() {
			CreateNote();
		}
	}
}
