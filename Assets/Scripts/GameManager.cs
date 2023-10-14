using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public WheelJoint2D wheel;
	public DrawManager draw;
	private Camera mainCam;
	private List<WheelJoint2D> wheels=new();
	private void Start()
	{
		mainCam=Camera.main;
	}
	public void SpawnWheel()
	{
		wheels.Add(Instantiate(wheel));
		StartCoroutine(MoveWheelTowardsMouse(wheels[wheels.Count-1]));
	}
	public IEnumerator MoveWheelTowardsMouse(WheelJoint2D go)
	{		
		while(Input.GetMouseButton(0)==false)
		{
			go.transform.position=(Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);			
			yield return new WaitForSecondsRealtime(0.01f);
		}
		//go.connectedAnchor=go.transform.position;
	}

	public void Test()
	{
		if(wheels.Count<1)
			return;
		Rigidbody2D generalLine = GetLargestLine(draw.lines).GetComponent<Rigidbody2D>();
		foreach(var wh in wheels)
		{
			wh.connectedAnchor=wh.transform.position-generalLine.transform.position;
			wh.useMotor=true;
			wh.connectedBody=generalLine;
			wh.GetComponent<Rigidbody2D>().gravityScale=1;
		}
		Debug.Log(generalLine.centerOfMass);
		//Debug.Log(generalLine.GetComponent<Renderer>().bounds.center);
		generalLine.gravityScale=1;

	}
	private Line GetLargestLine(List<Line> lines)
	{
		Line _line=lines[0];
		foreach(Line li in lines)
		{
			if(li._collider.pointCount>_line._collider.pointCount)
				_line=li;
		}
		return _line;
	}
}
