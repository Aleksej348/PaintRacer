using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
	[HideInInspector] public HingeJoint2D joint;
	private void Start()
	{
		joint=GetComponent<HingeJoint2D>();
	}
}
