using UnityEngine;

namespace ExamplePackage {

	public class Food : MonoBehaviour {
		public float Tastiness {
			get => tastiness;
			set => tastiness = value;
		}

		[SerializeField]
		private float tastiness;
	}
}
