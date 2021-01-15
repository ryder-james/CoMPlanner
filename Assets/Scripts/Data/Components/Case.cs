using CasePlanner.Data;
using System.Collections.Generic;
using System.Linq;

public class Case : Note, ISerializable<Case.SerializedCase> {
	private static int nextID = 0;

	private readonly List<Scene> scenes;

	public Scene[] Scenes => scenes.ToArray();

	public Case() : base(nextID++) {
		Title = "Case";

		scenes = new List<Scene>();
	}

	public void AddScene(Scene scene) {
		scenes.Add(scene);
	}

	public void RemoveScene(Scene scene) {
		foreach (int id in scene.Connections) {
			GetSceneByID(id).Disconnect(scene.ID);
		}

		scenes.Remove(scene);
	}

	public SerializedCase Serialized() {
		List<Edge> edges = new List<Edge>();
		foreach (Scene s in scenes) {
			foreach (int bSide in s.Connections) {
				Edge e = new Edge {
					a = s.ID,
					b = bSide
				};

				if (!edges.Contains(e)) {
					edges.Add(e);
				}
			}
		}

		SerializedCase serCase = new SerializedCase {
			id = ID,
			title = Title,
			x = X,
			y = Y,
			scenes = Scenes.Select(s => s.Serialized()).ToArray(),
			sceneEdges = edges.ToArray()
		};

		return serCase;
	}

	public void Deserialize(SerializedCase obj) {
		ID = obj.id;
		Title = obj.title;
		X = obj.x;
		Y = obj.y;

		scenes.Clear();
		foreach (var scene in obj.scenes) {
			Scene s = new Scene();
			s.Deserialize(scene);
			scenes.Add(s);
		}

		foreach (Edge e in obj.sceneEdges) {
			Scene a = GetSceneByID(e.a);
			Scene b = GetSceneByID(e.b);

			a.Connect(e.b);
			b.Connect(e.a);
		}
	}

	public Scene GetSceneByID(int id) {
		return scenes.Where(s => s.ID == id).FirstOrDefault();
	}

	[System.Serializable]
	public struct SerializedCase {
		public int id;
		public string title;
		public float x;
		public float y;
		public Scene.SerializedScene[] scenes;
		public Edge[] sceneEdges;
	}
}
