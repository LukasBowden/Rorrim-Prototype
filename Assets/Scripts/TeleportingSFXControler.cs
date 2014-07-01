using UnityEngine;
using System.Collections;

public class TeleportingSFXControler : MonoBehaviour {

	public AudioClip button;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton(0))
		{
			if (Input.GetMouseButtonDown(1)) 
			{
			audio.PlayOneShot(button);
			}
		}
	}
}
