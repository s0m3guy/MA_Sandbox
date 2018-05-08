using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopScript : MonoBehaviour {

	GameObject clone;
	Vector3 screenPoint, offset, scanPos;

	bool bla;

	BoxCollider2D collider;

	// Use this for initialization
	void Start () {

		collider = GetComponent<BoxCollider2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		clone = Instantiate(Resources.Load("Top 1")) as GameObject;
		clone.GetComponent<TopScript> ().bla = true;

		collider.enabled = false;

		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag() {

		Vector3 cursorPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (cursorPoint) + offset;
		clone.transform.position = cursorPosition;
	}
}
