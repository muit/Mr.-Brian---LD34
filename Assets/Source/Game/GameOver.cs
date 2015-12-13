using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	
	void Start () {
		Console console = FindObjectOfType<Console>();
		console.Write("GAME OVER", 75);
		console.Write("Hope you liked this small LudumDare34 Compo game.", 75);
		console.Write("Your feedback is highly appreciated.", 100);
		console.Write("@muitxer", 100);
	}
}
