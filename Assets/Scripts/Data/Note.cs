using System.Collections.Generic;

public class Note {
	public delegate void ValueChanged<T>(T oldValue, T newValue);

	public readonly int ID;

	private string title;
	private readonly List<int> connections;

	public string Title {
		get => title;
		set {
			OnTitleChanged?.Invoke(title, value);
			title = value;
			if (title.Length > 32) {
				title = title.Substring(0, 32);
			}
		}
	}

	public int[] Connections => connections.ToArray();
	public NoteType Type { get; set; }
	public ValueChanged<string> OnTitleChanged { get; set; }

	protected Note(int id) {
		ID = id;

		title = "";
		connections = new List<int>();
		Type = NoteType.Location;
	}
}
