[System.Serializable]
public class Edge {
	public int a, b;

	public static bool operator ==(Edge a, Edge b) {
		return !(a is null) ? a.Equals(b) : b is null;
	}

	public static bool operator !=(Edge a, Edge b) {
		return !(a is null) ? !a.Equals(b) : !(b is null);
	}

	public override bool Equals(object obj) {
		if (obj is Edge) {
			return (a == (obj as Edge).a && b == (obj as Edge).b)
				|| (a == (obj as Edge).b && b == (obj as Edge).a);
		} else {
			return false;
		}
	}

	public override int GetHashCode() {
		var hashCode = 2118541809;
		hashCode = hashCode * -1521134295 + a.GetHashCode();
		hashCode = hashCode * -1521134295 + b.GetHashCode();
		return hashCode;
	}
}