using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace JCommon.UI {
	[ExecuteInEditMode]
	[RequireComponent(typeof(Slider))]
	public class SliderValueDisplay : MonoBehaviour {
		[SerializeField] private float valueMultiplier = 1;
		[SerializeField] private TMP_Text valueField = null;

		private Slider slider;

		private void Start() {
			slider = GetComponent<Slider>();
			slider.onValueChanged.AddListener(UpdateText);
			UpdateText(slider.value);
		}

		private void UpdateText(float newValue) {
			valueField.text = ((int) (newValue * valueMultiplier)).ToString();
		}
	}
}