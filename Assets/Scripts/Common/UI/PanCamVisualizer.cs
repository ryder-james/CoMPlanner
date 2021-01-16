using UnityEngine;

namespace JCommon.UI {
	public class PanCamVisualizer : MonoBehaviour {
		[SerializeField] private PanCamera panCam = null;
		[SerializeField] private SpriteRenderer visualSprite = null;

		private Vector2 baseSize;

		private void Start() {
			baseSize = visualSprite.size;
		}

		private void Update() {
			visualSprite.size = baseSize * (panCam.Size / panCam.BaseSize);
		}
	}
}