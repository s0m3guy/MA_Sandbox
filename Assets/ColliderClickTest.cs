using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderClickTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		GetComponent<CircleCollider2D> ().enabled = false;
	}
}
