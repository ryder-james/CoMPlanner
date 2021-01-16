using System.Collections.Generic;

using JCommon.Data.Seralization;

namespace CasePlanner.Data.Components {
	public class Scene : Note, ISerializable<Scene.SerializedScene> {
		private static int nextID = 0;

		private readonly List<Clue> clues;

		public Clue[] Clues => clues.ToArray();
		public string Description { get; set; }
		public string Notes { get; set; }

		public Scene() : base(nextID++) {
			clues = new List<Clue>();
			Description = "";
			Notes = "";
		}

		public void UpdateClueText(ref Clue clue, string newText) {
			if (clues.Contains(clue)) {
				if (newText != "") {
					clue.Text = newText;
				} else {
					clues.Remove(clue);
				}
			} else {
				if (newText != "") {
					clue.Text = newText;
					clues.Add(clue);
				}
			}
		}

		public void UpdateClueColor(ref Clue clue, string newColor) {
			if (clues.Contains(clue)) {
				clue.Color = newColor;
			}
		}

		public SerializedScene Serialized() {
			List<string> clues = new List<string>();
			foreach (Clue clue in this.clues) {
				clues.Add(clue.Color);
				clues.Add(clue.Text);
			}

			SerializedScene serScene = new SerializedScene {
				id = ID,
				title = Title,
				notes = Notes,
				description = Description,
				x = X,
				y = Y,
				clues = clues.ToArray()
			};

			return serScene;
		}

		public void Deserialize(SerializedScene obj) {
			ID = obj.id;
			nextID = ID + 1;

			Title = obj.title;
			Notes = obj.notes;
			Description = obj.description;
			X = obj.x;
			Y = obj.y;

			string val;
			Clue clue = new Clue();
			for (int i = 0; i < obj.clues.Length; i++) {
				val = obj.clues[i];
				if (val.StartsWith("#")) {
					clue.Color = val;
				} else {
					clue.Text = val;
					clues.Add(clue);
					clue = new Clue();
				}
			}
		}

		[System.Serializable]
		public struct SerializedScene {
			public int id;
			public string title;
			public string notes;
			public string description;
			public float x;
			public float y;
			public string[] clues;
		}
	}
}