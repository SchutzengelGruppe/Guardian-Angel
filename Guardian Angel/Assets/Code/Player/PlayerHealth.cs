using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float Health;

	void OnGUI(){

	}

	void OnCollisionEnter2D(Collision2D collision){

		if (collision.gameObject.tag == "Enemy") {
			Health = Health - 1;
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
