using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour {

	Vector3 screenPoint, offset, scanPos;

	public Transform boundLeft, boundRight;

	bool dragging = false;
	bool isInsideOverlay = false;

	[SerializeField]
	GameObject overlay;

	BoxCollider2D collider;

	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (dragging) {
			overlay.SetActive (true);
		} else {
			overlay.SetActive(false);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "test") {
			Debug.Log ("Entered collision");
		} else if (other.tag == "overlay") {
			Debug.Log ("Staying in top collision");
			isInsideOverlay = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "overlay") {
			isInsideOverlay = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Entered collision");
	}

	void OnMouseDown() {

		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

	}

	void OnMouseDrag() {

		GetComponent<BoxCollider2D> ().enabled = false;

		dragging = true;

		Vector3 cursorPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (cursorPoint) + offset;
		transform.position = new Vector3 (
			Mathf.Clamp (cursorPosition.x, boundLeft.position.x, boundRight.position.x),
			cursorPosition.y,
			cursorPosition.z);
	}

	void OnMouseUp() {
		dragging = false;
		if (isInsideOverlay) {
			Destroy (this.gameObject);
		}
	}
}
