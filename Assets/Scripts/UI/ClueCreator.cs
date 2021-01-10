using CasePlanner.Data;
using CasePlanner.Data.Notes;
using UnityEngine;
using UnityEngine.UI;

namespace CasePlanner.UI {
	public class ClueCreator : MonoBehaviour {
		[SerializeField]
		private GameObject clueBase = null;

		[SerializeField]
		private StickyNoteViewer viewer = null;

		public void CreateClue(string text = "") {
			GameObject go = Instantiate(clueBase, transform);
			Clue c = go.GetComponent<Clue>();
			c.Viewer = viewer;
			c.Text = text;
		}
	}
}
