﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

	public GameObject sourceObject, targetObject;
	public bool isSnapped = false;


	void Update() {
		Debug.Log (targetObject);
	}
}
