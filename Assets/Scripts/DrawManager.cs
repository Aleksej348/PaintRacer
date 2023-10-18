using System.Collections.Generic;
using UnityEngine;

public enum DrawMode { building,drawing}
public class DrawManager : MonoBehaviour
{
	public bool canDraw;
	public List<Line> lines = new();
	public const float RESOLUTION = .2f;

	[SerializeField] private Line drawLinePrefab, carLinePrefab;
	[HideInInspector] public Camera mainCam;
	private Line currLine,currPrefab;
	private DrawMode _mode;
	public DrawMode mode
	{
		get => _mode;
		set
		{
			_mode=value;
			currPrefab=value==0 ? carLinePrefab : drawLinePrefab;
		}
	}
	public void Init()
	{
		
	}


	private void Update()
	{
		if(canDraw)
		{
			Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

			if(Input.GetMouseButtonDown(0))
			{
				currLine=Instantiate(currPrefab,mousePos,Quaternion.identity);				
			}

			if(Input.GetMouseButton(0))
				currLine.SetPosition(mousePos);

			if(Input.GetMouseButtonUp(0))
			{
				lines.Add(currLine);
			}
		}
	}
}
