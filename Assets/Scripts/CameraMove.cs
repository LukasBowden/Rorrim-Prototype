using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour 
{
	private GameObject camera;
	private Vector3 pos;

	// Use this for initialization
	void Start ()
	{
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		pos = transform.position;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			Debug.Log("CameraTrigger");
			camera.transform.position = pos;
		}
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
