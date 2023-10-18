using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeLine : Line
{
	public EdgeCollider2D _collider;
	private DrawManager _dm;
	private DrawManager dm => _dm??=GameManager.gm.dm;

	public override void SetPosition(Vector2 pos)
	{
		pos-=(Vector2)transform.position;
		if(!CanAppend(pos))
			return;

		_points.Add(pos);		
		_renderer.positionCount++;
		_renderer.SetPosition(_renderer.positionCount-1,pos);
		_collider.SetPoints(_points);
	    dm.PaintValue-=1;
	}
}