using System.Collections.Generic;

public class Case {
	public readonly int ID;

	private string title;
	private readonly List<Scene> scenes;
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

	public Scene[] Scenes => scenes.ToArray();
	public int[] Connections => connections.ToArray();

	public Case(int id) {
		ID = id;

		title = "";
		scenes = new List<Scene>();
		connections = new List<int>();
	}
}
