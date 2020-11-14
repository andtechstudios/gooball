using ExamplePackage;
using UnityEngine;

public class Apple : Food {
	public Color Color {
		get => color;
		set => color = value;
	}

	[SerializeField]
	private Color color;
}
