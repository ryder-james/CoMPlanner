using CasePlanner.Data;
using System.Collections.Generic;

public abstract class Note {
	public delegate void ValueChanged<ValueType>(ValueType oldValue, ValueType newValue);

	public int ID { get; protected set; }

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
	public float X { get; set; }
	public float Y { get; set; }

	protected Note(int id) {
		ID = id;

		title = "";
		connections = new List<int>();
		Type = NoteType.Location;
	}

	public bool Connect(int otherID) {
		if (connections.Contains(otherID)) {
			return false;
		} else {
			connections.Add(otherID);
			return true;
		}
	}

	public void Disconnect(int otherID) {
		connections.Remove(otherID);
	}
}
