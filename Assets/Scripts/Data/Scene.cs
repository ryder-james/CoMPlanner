using System.Collections.Generic;

public class Scene : StickyNote {
	private readonly List<Clue> clues;

	public Clue[] Clues => clues.ToArray();
	public string Description { get; set; }
	public string Notes { get; set; }

	public Scene(int id) : base(id) {
		clues = new List<Clue>();
		Description = "";
		Notes = "";
	}
}
