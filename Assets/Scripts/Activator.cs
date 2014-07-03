using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour 
{
	private SpriteRenderer activatorRenderer;

	private bool locked = false;
	private bool adding = false;

	private float curTimeCharging = 0;
	private float curTimeDecharging = 0;

	private Color curColor;
	private Color lockedColor;
	// Use this for initialization
	void Start ()
	{
		activatorRenderer = gameObject.GetComponent<SpriteRenderer> ();
		activatorRenderer.color = Color.blue;
		lockedColor = activatorRenderer.color;
	}
	
	// Update is called once per frame
	void Update ()
	{
		curColor = activatorRenderer.color;	
		if (!locked && !adding)
		{
		}		
		if(adding)
		{
			curTimeDecharging = 0;
			curTimeCharging += Time.deltaTime * 0.05f;
		}
		if(curTimeCharging > 0 && !adding)
		{
			curTimeCharging -= Time.deltaTime * 0.5f;
		}
		if(!adding)
		{			
			curTimeDecharging += Time.deltaTime * 0.5f;
			activatorRenderer.color = Color.Lerp (curColor, lockedColor, curTimeDecharging);
		}
		adding = false;
	}

	public void AddColor(Color beamColor)
	{
		adding = true;
	//	curColor = activatorRenderer.color;	
		if(Input.GetAxisRaw("Mouse ScrollWheel") != 0)
		{
			curTimeCharging = 0;
			curColor = activatorRenderer.color;	
		}
		activatorRenderer.color = Color.Lerp(curColor, beamColor, curTimeCharging);	
		if(curColor == beamColor)
		{
			Debug.Log("lock");
			locked = true;
			lockedColor = curColor;
			curTimeCharging = 0;
		}
		else
		{
			locked = false;
		}
	}
}
