using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2_LeftCircle : MonoBehaviour {

	GameObject line;
	Collider2D overlappedCollider;

	Vector3 clampVector, unclampedVector;

	BoxCollider2D upperBound, lowerBound;

	GameObject tangent1, tangent2;

	// Use this for initialization
	void Start () {
		upperBound = GameObject.Find ("Upperbound").GetComponent<BoxCollider2D>();
		lowerBound = GameObject.Find ("Lowerbound").GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		// instantiate Line after clicking circle
		line = Instantiate (Resources.Load ("Line2")) as GameObject;
		line.GetComponent<Scene2_Line_Bezier> ().sourceObject = this.gameObject;
	}

	void OnMouseDrag() {

		Vector2 screenPos = new Vector2 ();
		Camera.main.ScreenToWorldPoint (screenPos);

		unclampedVector = Camera.main.ScreenToWorldPoint (Input.mousePosition)
			+ Vector3.forward * 10
			;

		clampVector = new Vector3 ((Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).x,
			Mathf.Clamp ((Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).y,
				lowerBound.bounds.max.y,
				upperBound.bounds.min.y),
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).z);


		line.GetComponent<LineRenderer> ().SetPosition (0,
			new Vector3 (transform.position.x + (GetComponent<SpriteRenderer> ().bounds.size.x) / 2,
				transform.position.y,
				transform.position.z));
		line.GetComponent<LineRenderer>().SetPosition(1, unclampedVector);

		overlappedCollider = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		Debug.Log (overlappedCollider);

		line.GetComponent<Scene2_Line_Bezier>().tangent2.transform.position = new Vector3 (
//			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).x - 2,
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).x - GetComponent<CircleCollider2D>().bounds.size.x/2,
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).y,
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).z);

		if (overlappedCollider && overlappedCollider.CompareTag ("inputPin")) {
//			line.GetComponent<LineRenderer> ().SetPosition (1, overlappedCollider.transform.position);
			line.GetComponent<Scene2_Line_Bezier> ().controlPoints [4] = overlappedCollider.gameObject;
			line.GetComponent<Scene2_Line_Bezier> ().controlPoints [5] = overlappedCollider.gameObject;
		} else if (!overlappedCollider) {
			line.GetComponent<Scene2_Line_Bezier> ().controlPoints [4] = line.GetComponent<Scene2_Line_Bezier> ().mouseFollower;
			line.GetComponent<Scene2_Line_Bezier> ().controlPoints [5] = line.GetComponent<Scene2_Line_Bezier> ().mouseFollower;
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
