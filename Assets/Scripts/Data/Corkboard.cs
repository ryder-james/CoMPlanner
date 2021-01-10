using UnityEngine;
using UnityEngine.EventSystems;

namespace CasePlanner.Data {
	public class Corkboard : MonoBehaviour {
		[SerializeField]
		private GameObject stringCutter = null;

		private Rigidbody2D stringCutterRB;
		private bool isCutting = false;

		private void Start() {
			stringCutterRB = stringCutter.GetComponent<Rigidbody2D>();
		}

		private void Update() {
			if (isCutting) {
				Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				target.z = 0;
				stringCutterRB.velocity = (target - stringCutter.transform.position) * 70;
			} else {
				stringCutterRB.velocity = Vector3.zero;
			}
		}

		public void StartBreak(BaseEventData baseEventData) {
			var eventData = baseEventData as PointerEventData;
			if (eventData.button == PointerEventData.InputButton.Left) {
				isCutting = true;
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				stringCutter.transform.position = new Vector3(pos.x, pos.y);
				stringCutter.SetActive(true);
			}
		}

		public void StopBreak() {
			isCutting = false;
			stringCutter.SetActive(false);
		}
	}
}
