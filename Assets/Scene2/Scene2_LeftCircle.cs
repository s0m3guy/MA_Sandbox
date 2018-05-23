using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2_LeftCircle : MonoBehaviour {

	GameObject line;
	Vector2[] tempEdges;
	Collider2D overlappedCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		// instantiate Line after clicking circle
		line = Instantiate (Resources.Load ("Line2")) as GameObject;
//		Scene2_Manager.currentlyDrawnLine = line.gameObject;
	}

	void OnMouseDrag() {

		Vector2 screenPos = new Vector2 ();
		Camera.main.ScreenToWorldPoint (screenPos);

		line.GetComponent<LineRenderer> ().SetPosition (0,
			new Vector3 (transform.position.x + (GetComponent<SpriteRenderer> ().bounds.size.x) / 2,
				transform.position.y,
				transform.position.z));
		line.GetComponent<LineRenderer> ().SetPosition (1, Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10);

		tempEdges = line.GetComponent<EdgeCollider2D> ().points;
		tempEdges [0] = new Vector2 (
			transform.position.x + (GetComponent<SpriteRenderer> ().bounds.size.x) / 2 - 0.7f,
			transform.position.y - 0.217f);
		tempEdges [1] = new Vector2 (
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).x - 0.7f,
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).y - 0.217f);

		line.GetComponent<EdgeCollider2D> ().points = tempEdges;

		overlappedCollider = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition));

		if (overlappedCollider && overlappedCollider.CompareTag ("inputPin")) {
//			Debug.Log ("hit input pin");
			line.GetComponent<LineRenderer> ().SetPosition (1, overlappedCollider.transform.position);
		}
	}

	void OnMouseUp() {

		if (overlappedCollider && overlappedCollider.CompareTag ("inputPin")) {
			if (overlappedCollider.GetComponent<Scene2_RightCircle> ().connectedLine) {
				// Line is already connected
				Destroy(overlappedCollider.GetComponent<Scene2_RightCircle>().connectedLine.gameObject);
				overlappedCollider.GetComponent<Scene2_RightCircle>().connectedLine = line;
				line.GetComponent<Scene2_Line> ().targetObject = overlappedCollider.gameObject;
				line.GetComponent<Scene2_Line> ().sourceObject = this.gameObject;
			} else {
				// No line connected
				overlappedCollider.GetComponent<Scene2_RightCircle> ().connectedLine = line;
				line.GetComponent<Scene2_Line> ().targetObject = overlappedCollider.gameObject;
				line.GetComponent<Scene2_Line> ().sourceObject = this.gameObject;
			}
		} else if (!overlappedCollider) {
			Destroy (line);
			Debug.Log ("Destroyed " + line);
		}
	}
}
