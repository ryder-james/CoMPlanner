using System.Collections.Generic;

public class Series {
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
}
