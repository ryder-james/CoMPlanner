using UnityEngine;
using UnityEngine.UI;

namespace JCommon.UI {
	[RequireComponent(typeof(Slider))]
	public class PrefsSlider : MonoBehaviour {
		[SerializeField] private string preferenceName = "";
		[SerializeField, Range(0, 1)] private float defaultValue = 0.5f;

		private Slider slider;

		private void Awake() {
			slider = GetComponent<Slider>();
			float value = PlayerPrefs.GetFloat(preferenceName, defaultValue);
			slider.value = value * slider.maxValue;
			slider.onValueChanged.AddListener(UpdateValue);
		}

		private void UpdateValue(float _) {
			PlayerPrefs.SetFloat(preferenceName, slider.normalizedValue);
		}

		private void OnDestroy() {
			slider.onValueChanged.RemoveListener(UpdateValue);
		}
	}
}