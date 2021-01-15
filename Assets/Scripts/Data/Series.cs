using CasePlanner.Data;
using System.Collections.Generic;
using System.Linq;

public class Series : ISerializable<Series.SerializedSeries> {
	private string title;
	private readonly List<Case> cases;

	public string Title {
		get => title;
		set {
			title = value.Trim();
			if (title.Length > 32) {
				title = title.Substring(0, 32);
			}
		}
	}

	public Case[] Cases => cases.ToArray();

	public Series() {
		title = "";
		cases = new List<Case>();
	}

	public SerializedSeries Serialized() {
		List<Edge> edges = new List<Edge>();
		foreach (Case c in Cases) {
			foreach (int bSide in c.Connections) {
				Edge e = new Edge {
					a = c.ID,
					b = bSide
				};

				if (!edges.Contains(e)) {
					edges.Add(e);
				}
			}
		}

		SerializedSeries serSeries = new SerializedSeries {
			title = Title,
			cases = Cases.Select(c => c.Serialized()).ToArray(),
			caseEdges = edges.ToArray()
		};

		return serSeries;
	}

	public void Deserialize(SerializedSeries obj) {
		Title = obj.title;

		foreach(var sCase in obj.cases) {
			Case c = new Case();
			c.Deserialize(sCase);
			cases.Add(c);
		}

		foreach (Edge e in obj.caseEdges) {
			Case a = GetCaseByID(e.a);
			Case b = GetCaseByID(e.b);

			a.Connect(e.b);
			b.Connect(e.a);
		}
	}

	public Case GetCaseByID(int id) {
		return cases.Where(c => c.ID == id).FirstOrDefault();
	}

	[System.Serializable]
	public struct SerializedSeries {
		public string title;
		public Case.SerializedCase[] cases;
		public Edge[] caseEdges;
	}
}
