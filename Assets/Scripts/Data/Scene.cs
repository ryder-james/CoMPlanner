using System.Collections.Generic;

public class Scene : Note {
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
}
