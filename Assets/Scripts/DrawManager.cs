using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	private float currPaintValue;
	[SerializeField] private float maxPaintValue;
	[SerializeField] private Image paintBar;
	public float PaintValue
	{
		get => currPaintValue;
		set
		{
			if(mode!=DrawMode.drawing)
				return;
			currPaintValue=value<0 ? 0 : value>maxPaintValue ? maxPaintValue : value;
			paintBar.fillAmount=currPaintValue/maxPaintValue;
		}
	}
	public void Init()
	{
		currPaintValue=maxPaintValue;

	}


	private void Update()
	{
		if(canDraw&&PaintValue>0)
		{
			Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

			if(Input.GetMouseButtonDown(0))
			{
				currLine=Instantiate(currPrefab,mousePos,Quaternion.identity);				
			}

			if(Input.GetMouseButton(0))
			{
				currLine.SetPosition(mousePos);
			}

			if(Input.GetMouseButtonUp(0))
			{
				lines.Add(currLine);				
			}
		}
	}
}
