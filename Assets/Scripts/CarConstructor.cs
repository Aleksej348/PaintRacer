
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarConstructor : MonoBehaviour
{
	
	[SerializeField] private Rigidbody2D wheelPrefab, pilotBody;	
	[SerializeField] private Transform marker;
	private List<Rigidbody2D> wheels = new();
	[HideInInspector] public Camera mainCam;

	private DrawManager _dm;
	private DrawManager dm => _dm??=GameManager.gm.dm;

	[SerializeField] private Button drawButton, wheelButton, applyButton;
	private Button _selectedButton;
	private Button selectedButton
	{
		get => _selectedButton;
		set
		{			
			if(value==null)
			{
				drawButton.interactable=true;
				wheelButton.interactable=true;
				applyButton.interactable=true;
				marker.gameObject.SetActive(false);
			}
			else
			{
				drawButton.interactable=false;
				wheelButton.interactable=false;
				applyButton.interactable=false;
				marker.gameObject.SetActive(true);
				marker.position=value.transform.position;
			}
			_selectedButton=value;
		}
	}	
	public void Init()
	{

	}
	#region#AddWheels
	public void SpawnWheel()
	{
		selectedButton=wheelButton;
		wheels.Add(Instantiate(wheelPrefab));
		StartCoroutine(MoveWheelTowardsMouse(wheels[wheels.Count-1].transform));
	}
	public IEnumerator MoveWheelTowardsMouse(Transform go)
	{
		while(Input.GetMouseButton(0)==false)
		{
			go.transform.position=(Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
			yield return new WaitForSecondsRealtime(0.01f);
		}
		selectedButton=null;
	}
	#endregion
	#region#DrawCar
	public void DrawCar()
	{
		selectedButton=drawButton;
		StartCoroutine(_DrawCar());
	}
	private IEnumerator _DrawCar()
	{
		yield return new WaitForEndOfFrame();
		dm.mode=DrawMode.building;
		dm.canDraw=true;
		yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
		dm.canDraw=false;
		selectedButton=null;
	}
	#endregion
	#region#Apply
	public void ApplyCreateCar()
	{
		if(wheels.Count<1)
			return;
		Line generalLine = GetLargestLine(dm.lines);
		Car car = generalLine.gameObject.AddComponent<Car>();
		Rigidbody2D carRb = car.gameObject.AddComponent<Rigidbody2D>();
		carRb.drag=0.1f;
		carRb.angularDrag=1;
		car.wheels=new List<Rigidbody2D>(wheels);	
		car.transform.name="Car";				
		foreach(var line in dm.lines)
		{
			line.transform.parent=car.transform;
		}
		foreach(var wh in wheels)
		{
			var wheelJoint = car.gameObject.AddComponent<WheelJoint2D>();
			wheelJoint.autoConfigureConnectedAnchor=false;
			wheelJoint.suspension=new JointSuspension2D { dampingRatio=0.1f,frequency=10,angle=90 };
			wheelJoint.anchor=wh.transform.position-car.transform.position;
			wheelJoint.connectedBody=wh;
			wh.gravityScale=1;
			wh.transform.parent=car.transform;
		}
		carRb.gravityScale=1;
		pilotBody.gravityScale=1;
		dm.mode=DrawMode.drawing;
		dm.canDraw=true;
		applyButton.gameObject.SetActive(false);
		wheelButton.gameObject.SetActive(false);
		drawButton.gameObject.SetActive(false);
		GameManager.gm.cinemachine.Follow=car.transform;
		GameManager.gm.cinemachine.LookAt=car.transform;
	}
	private Line GetLargestLine(List<Line> lines)
	{
		Line _line = lines[0];
		foreach(Line li in lines)
		{
			if(li._points.Count>_line._points.Count)
				_line=li;
		}
		dm.lines.Remove(_line);
		return _line;
	}
	#endregion

}
