using UnityEngine;
using CasePlanner.Data.Notes;

namespace CasePlanner.UI {
	public class StickyNoteCreator : MonoBehaviour {
		[SerializeField] private Transform spawnLocation = null;
		[SerializeField] private RectTransform stickyNoteParent = null;
		[SerializeField] private GameObject stickyNoteBase = null;
		[SerializeField] private GameObject stickyNoteViewer = null;

		public int NextNoteID { get; set; } = 0;

		public StickyNote CreateNote() {
			Transform parent = stickyNoteParent != null ? stickyNoteParent.transform : transform.parent;
			Vector3 position = spawnLocation.position;
			position.z = 0;
			GameObject noteObj = Instantiate(stickyNoteBase, position, Quaternion.identity, parent);
			StickyNote note = noteObj.GetComponent<StickyNote>();
			note.Viewer = stickyNoteViewer;
			note.ID = NextNoteID++;
			noteObj.name = $"{note.ID}: ";
			return note;
		}

		public void CreateNoteButton() {
			CreateNote();
		}
	}
}
