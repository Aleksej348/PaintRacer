using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
	[SerializeField] protected LineRenderer _renderer;	

	public readonly List<Vector2> _points = new List<Vector2>();
	
	public virtual void SetPosition(Vector2 pos){}

	public bool CanAppend(Vector2 pos)
	{
		if(_renderer.positionCount==0)
			return true;

		return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount-1),pos)>DrawManager.RESOLUTION;
	}
}