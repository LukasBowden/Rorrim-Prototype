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
	private Color lerpFrom;

	void Start ()
	{
		activatorRenderer = gameObject.GetComponent<SpriteRenderer> ();
		lockedColor = activatorRenderer.color;
	}	

	void Update ()
	{
		curColor = activatorRenderer.color;	

		if(adding)
		{
			curTimeDecharging = 0;
			curTimeCharging += Time.deltaTime * 0.5f;
		}
		if(!adding)
		{			
			curTimeCharging = 0;
			curTimeDecharging += Time.deltaTime * 0.2f;
			activatorRenderer.color = Color.Lerp (curColor, lockedColor, curTimeDecharging);
		}
		adding = false;
	}

	public void AddColor(Color beamColor)
	{
		if(!adding)
		{
			lerpFrom = activatorRenderer.color;
		}
		adding = true;
		if(Input.GetAxisRaw("Mouse ScrollWheel") != 0)
		{
			curTimeCharging = 0;
			curColor = activatorRenderer.color;	
		}
		activatorRenderer.color = Color.Lerp(lerpFrom, beamColor, curTimeCharging);	
		if(lerpFrom == beamColor)
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
