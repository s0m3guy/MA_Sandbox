using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {

	Vector3 mousePos;

	CircleCollider2D collid;

	void Start() {
		collid = GetComponent<CircleCollider2D> ();
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("output")) {
			Debug.Log ("Entered output");
			collid.enabled = false;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("output")) {
			collid.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (GetComponent<CircleCollider2D> ().enabled);

		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		this.transform.position = mousePos;
	}
}
