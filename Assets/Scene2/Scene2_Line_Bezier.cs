using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2_Line_Bezier : MonoBehaviour {

	public GameObject sourceObject, targetObject;

	public List<GameObject> controlPoints = new List<GameObject>();
	public Color color = Color.white;
	public float width = 0.2f;
	public int numberOfPoints = 20;
	LineRenderer lineRenderer;

	GameObject tangent1, tangent2, mouseFollower;

	void Start() {
		
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = true;
		lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));

		tangent1 = Instantiate (Resources.Load ("Tangent")) as GameObject;
		tangent2 = Instantiate (Resources.Load ("Tangent")) as GameObject;

		tangent1.transform.parent = this.transform;;
		tangent2.transform.parent = this.transform;

		mouseFollower = GameObject.Find ("mouseFollower");

		controlPoints.Add (sourceObject);
		controlPoints.Add (sourceObject);
		controlPoints.Add (tangent1);
		controlPoints.Add (tangent2);
		controlPoints.Add (mouseFollower);
		controlPoints.Add (mouseFollower);

		tangent1.transform.position = sourceObject.transform.position;
		tangent1.transform.position = new Vector3 (
			sourceObject.transform.position.x+2,
			sourceObject.transform.position.y,
			sourceObject.transform.position.z);
	}

	void Update() {

		mouseFollower.transform.position = new Vector3 (
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).x,
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).y,
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).z);

		tangent2.transform.position = new Vector3 (
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).x - 2,
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).y,
			(Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10).z);

		if (null == lineRenderer || controlPoints == null || controlPoints.Count < 3)
		{
			return; // not enough points specified
		}
		// update line renderer
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;

		if(numberOfPoints < 2)
		{
			numberOfPoints = 2;
		} 
		lineRenderer.positionCount = numberOfPoints * (controlPoints.Count - 2);

		Vector3 p0, p1 ,p2;
		for(int j = 0; j < controlPoints.Count - 2; j++)
		{
			// check control points
			if (controlPoints[j] == null || controlPoints[j + 1] == null 
				||	controlPoints[j + 2] == null)
			{
				return;  
			}
			// determine control points of segment
			p0 = 0.5f * (controlPoints[j].transform.position 
				+ controlPoints[j + 1].transform.position);
			p1 = controlPoints[j + 1].transform.position;
			p2 = 0.5f * (controlPoints[j + 1].transform.position 
				+ controlPoints[j + 2].transform.position);

			// set points of quadratic Bezier curve
			Vector3 position;
			float t;
			float pointStep = 1.0f / numberOfPoints;
			if (j == controlPoints.Count - 3)
			{
				pointStep = 1.0f / (numberOfPoints - 1.0f);
				// last point of last segment should reach p2
			}  
			for(int i = 0; i < numberOfPoints; i++) 
			{
				t = i * pointStep;
				position = (1.0f - t) * (1.0f - t) * p0 
					+ 2.0f * (1.0f - t) * t * p1 + t * t * p2;
				lineRenderer.SetPosition(i + j * numberOfPoints, position);
			}
		}
	}
}
