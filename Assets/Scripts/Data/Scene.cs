using System.Collections.Generic;

public class Scene {
	public readonly int ID;

	private string title;
	private readonly List<Clue> clues;
	private readonly List<int> connections;

	public string Title {
		get => title;
		set { 
			title = value.Trim();
			if (title.Length > 32) {
				title = title.Substring(0, 32);
			}
		}
	}

	public Clue[] Clues => clues.ToArray();
	public int[] Connections => connections.ToArray();
	public string Description { get; set; }
	public string Notes { get; set; }
	public SceneType Type { get; set; }

	public Scene(int id) {
		ID = id;

		title = "";
		clues = new List<Clue>();
		connections = new List<int>();
		Description = "";
		Notes = "";
		Type = SceneType.Location;
	}
}
