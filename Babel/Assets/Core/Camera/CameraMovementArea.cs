﻿using UnityEngine;
using System.Collections;

public class CameraMovementArea : MonoBehaviour {

	//private Transform cameraParent;
	[SerializeField]
	public Vector3 lastPos = Vector3.zero;
	public bool isInsideArea = true;

	// trqbva da vzema last positiona ot stay i kogato exit-na da varna parent position-a na last position
	// Use this for initialization
	void Start () {
		//cameraParent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other) {
		//Debug.Log ("Inside");
		isInsideArea = true;
		lastPos = transform.position;
	}

	void OnTriggerExit(Collider other) {

		isInsideArea = false;
	}
}
