using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAlt : MonoBehaviour {

	public GameObject connectedLine;
	public bool isConnected = false;

	void Start() {

//		Manager.dict.Add (this, false); // Add entry
//
//		foreach (KeyValuePair<CircleAlt, bool> pair in Manager.dict)	// Iterate through dictionary
//		{
////			Debug.Log(pair.Key + " " + pair.Value);
//		}
//
	}

	void Update() {

//		Debug.Log (Manager.currentlyDrawnLine);

		if (connectedLine) {
			GetComponent<SpriteRenderer> ().color = Color.green;
		} else {
			GetComponent<SpriteRenderer> ().color = Color.white;
		}

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;

		if (GetComponent<CircleCollider2D> ().bounds.Contains (mousePos)) {
			Manager.isMouseInsideCircle = true;
		} else if (!GetComponent<CircleCollider2D> ().bounds.Contains (mousePos)) {
			Manager.isMouseInsideCircle = false;
		}
			
		// If mouse inside circle and a line being drawn
		if (Manager.isMouseInsideCircle && Manager.currentlyDrawnLine) {
			Manager.currentlyDrawnLine.GetComponent<LineRenderer> ().SetPosition (1, this.transform.position);

			connectedLine = Manager.currentlyDrawnLine;
			connectedLine.GetComponent<Line> ().destinObject = this.gameObject;

			// If mouse not inside circle and a line being drawn
		} else if (!Manager.isMouseInsideCircle && Manager.currentlyDrawnLine) {
			if (connectedLine && !connectedLine.GetComponent<Line> ().isSnapped) {
				if (connectedLine) {
					connectedLine.GetComponent<Line> ().destinObject = null;
				}
				connectedLine = null;
			}

		}
	}
}
