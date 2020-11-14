using UnityEngine;

namespace ExampleProject {

	public class Script : MonoBehaviour {
		public string Health {
			get => health;
			set => health = value;
		}

		[SerializeField]
		private string health;
	}
}
