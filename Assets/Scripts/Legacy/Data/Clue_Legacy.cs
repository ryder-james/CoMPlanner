using CasePlanner.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CasePlanner.Data {
	public class Clue_Legacy : MonoBehaviour {
		[SerializeField]
		private TMP_InputField textField = null;

		private string text;

		public string Text { 
			get => text;
			set { 
				text = value;
				textField.text = value;
			}
		}

		public StickyNoteViewer_Legacy Viewer { get; set; }

		public void UpdateViewer() {
			if (Viewer.UpdateClue(text, textField.text)) {
				text = textField.text;
			}
		}

		public void Clear() {
			Text = "";
			UpdateViewer();
		}

		public override bool Equals(object obj) {
			return obj is Clue_Legacy clue &&
				   base.Equals(obj) &&
				   Text == clue.Text;
		}

		public override int GetHashCode() {
			var hashCode = 1665531892;
			hashCode = hashCode * -1521134295 + base.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
			return hashCode;
		}
	}
}