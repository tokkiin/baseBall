using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
	//public GameObject cameras;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		GameObject ball = GameObject.FindGameObjectWithTag ("hitball");
		Camera camera = GetComponent<Camera> ();
		if (ball == null) {

			camera.transform.position = new Vector3 (-2.5f, 2.6f, -2.5f);
		} else {
			float speed = ball.transform.GetComponent<Rigidbody> ().velocity.magnitude / 2;
			camera.transform.position = new Vector3 (ball.transform.position.x - 4 - speed, ball.transform.position.y + 2.6f, ball.transform.position.z - 4 - speed);;
		}
	}
}