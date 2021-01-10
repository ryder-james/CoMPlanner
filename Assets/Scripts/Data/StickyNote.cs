using System.Collections.Generic;

public class StickyNote {
	public readonly int ID;

	private string title;
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

	public int[] Connections => connections.ToArray();
	public StickyNoteType Type { get; set; }

	protected StickyNote(int id) {
		ID = id;

		title = "";
		connections = new List<int>();
		Type = StickyNoteType.Location;
	}
}
