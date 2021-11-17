using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {
	public float targetPosX;
	private float moveSpeed = 2000f;
	float movePosition;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localPosition.x > targetPosX)
		{
			movePosition = Time.deltaTime * moveSpeed;
			transform.Translate(new Vector3(-1, 0, 0) * movePosition);
		}
	}
}
