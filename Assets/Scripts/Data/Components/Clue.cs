using System.Collections.Generic;

public class Clue {
	public string Text { get; set; }
	public string Color { get; set; }

	public Clue(string text = "", string color = "#FDF1A4") {
		Text = text;
		Color = color;
	}

	public override bool Equals(object obj) {
		return obj is Clue clue &&
			   Text == clue.Text;
	}

	public override int GetHashCode() {
		return 1249999374 + EqualityComparer<string>.Default.GetHashCode(Text);
	}
}
