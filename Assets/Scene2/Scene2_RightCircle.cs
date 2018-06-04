using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2_RightCircle : MonoBehaviour {

	public GameObject connectedLine;
	Collider2D collisionObject;
	Transform transformSourceObject;

	GameObject tempLine;

	void Update() {

		if (connectedLine) {
			GetComponent<SpriteRenderer> ().color = Color.green;
		} else {
			GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}

	void OnMouseDrag() {

		if (connectedLine) {

			tempLine = connectedLine;

			Vector2 screenPos = new Vector2 ();
			Camera.main.ScreenToWorldPoint (screenPos);

			connectedLine.GetComponent<LineRenderer> ().SetPosition (0,
				new Vector3 (connectedLine.GetComponent<Scene2_Line> ().sourceObject.transform.position.x + (GetComponent<SpriteRenderer> ().bounds.size.x) / 2,
					connectedLine.GetComponent<Scene2_Line> ().sourceObject.transform.position.y,
					connectedLine.GetComponent<Scene2_Line> ().sourceObject.transform.position.z));
			connectedLine.GetComponent<LineRenderer> ().SetPosition (1, Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10);

			collisionObject = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition));

			if (collisionObject && collisionObject.CompareTag ("inputPin")) {
				connectedLine.GetComponent<LineRenderer> ().SetPosition (1, collisionObject.transform.position);
			}
		}
	}

	void OnMouseUp() {

		if (collisionObject && collisionObject.CompareTag ("inputPin")) {
			if (collisionObject.GetComponent<Scene2_RightCircle> ().connectedLine) {
				// Line is already connected
				Destroy(connectedLine.gameObject);
				collisionObject.GetComponent<Scene2_RightCircle> ().connectedLine = connectedLine;
				connectedLine.GetComponent<Scene2_Line> ().targetObject = collisionObject.gameObject;
				this.connectedLine = null;

			} else {
				// No line connected
				collisionObject.GetComponent<Scene2_RightCircle> ().connectedLine = connectedLine;
				connectedLine.GetComponent<Scene2_Line> ().targetObject = collisionObject.gameObject;
				this.connectedLine = null;
			}
		} else if (!collisionObject) {
			Destroy (connectedLine);
			connectedLine = null;
			Debug.Log ("Destroyed " + connectedLine);
		}
	}
}
