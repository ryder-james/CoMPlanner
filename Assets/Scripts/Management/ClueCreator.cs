using UnityEngine;

using CasePlanner.Data.Views;
using CasePlanner.Data.Components;

namespace CasePlanner.Management {
	public class ClueCreator : MonoBehaviour {
		[SerializeField] private Transform clueParent = null;
		[SerializeField] private GameObject clueViewPrefab = null;
		[SerializeField] private SceneDetailView detailView = null;

		public void CreateClue_GUI() {
			Clue c = new Clue();
			CreateClue(ref c);
		}

		public void ClearClues() {
			for (int i = 0; i < clueParent.childCount; i++) {
				Destroy(clueParent.GetChild(i).gameObject);
			}
		}

		public void CreateClue(ref Clue clue) {
			GameObject go = Instantiate(clueViewPrefab, clueParent);
			ClueView cv = go.GetComponent<ClueView>();

			cv.DetailView = detailView;
			cv.Clue = clue;
		}
	}
}