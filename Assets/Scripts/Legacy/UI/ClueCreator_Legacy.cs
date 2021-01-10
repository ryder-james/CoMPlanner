using CasePlanner.Data;
using CasePlanner.Data.Notes;
using UnityEngine;
using UnityEngine.UI;

namespace CasePlanner.UI {
	public class ClueCreator_Legacy : MonoBehaviour {
		[SerializeField]
		private GameObject clueBase = null;

		[SerializeField]
		private StickyNoteViewer_Legacy viewer = null;

		public void CreateClue(string text = "") {
			GameObject go = Instantiate(clueBase, transform);
			Clue_Legacy c = go.GetComponent<Clue_Legacy>();
			c.Viewer = viewer;
			c.Text = text;
		}
	}
}
