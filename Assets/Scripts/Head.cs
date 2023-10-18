using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
	[HideInInspector] public HingeJoint2D joint;
	[HideInInspector] public Rigidbody2D rb;
	private void Start()
	{
		joint=GetComponent<HingeJoint2D>();
		rb=GetComponent<Rigidbody2D>();
	}
}
