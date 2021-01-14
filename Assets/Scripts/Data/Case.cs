using System.Collections.Generic;

public class Case : Note {
	private static int nextID = 0;

	private readonly List<Scene> scenes;

	public Scene[] Scenes => scenes.ToArray();

	public Case() : base(nextID++) {
		scenes = new List<Scene>();
	}
}
