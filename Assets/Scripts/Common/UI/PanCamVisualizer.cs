using Common.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
