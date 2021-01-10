using System.Collections.Generic;

public class Case : StickyNote {
	private readonly List<Scene> scenes;

	public Scene[] Scenes => scenes.ToArray();

	public Case(int id) : base(id) {
		scenes = new List<Scene>();
	}
}
