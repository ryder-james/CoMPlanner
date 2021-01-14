using UnityEngine;

public class NoteCreator : MonoBehaviour {
	[SerializeField] private SceneDetailView detailView = null;
	[SerializeField] private Transform spawnLocation = null;
	[SerializeField] private Transform stickyNoteParent = null;
	[SerializeField] private GameObject stickyNoteBase = null;
	[SerializeField] private bool shouldCreateScenes = true;

	public Note GetNewNote() {
		Transform parent = stickyNoteParent != null ? stickyNoteParent : transform;
		Vector2 offset = Random.insideUnitCircle * 50;
		Vector3 position = spawnLocation.position + new Vector3(offset.x, offset.y, 0);
		position.z = 0;

		GameObject noteGO = Instantiate(stickyNoteBase, position, Quaternion.identity, parent);
		NoteView view = noteGO.GetComponent<NoteView>();
		view.DetailView = detailView;
		view.IsScene = shouldCreateScenes;

		return view.Note;
	}

	public void CreateNote() {
		GetNewNote();
	}
}
