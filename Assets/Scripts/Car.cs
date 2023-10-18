using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	public List<Rigidbody2D> wheels;
	[SerializeField] private float torqueSpeed,forceSpeed;
	private float moveInput;
	//private Rigidbody2D carRb;
	private void Start()
	{
		//carRb=GetComponent<Rigidbody2D>();
		torqueSpeed=5000;
		//forceSpeed=1000;
	}
	private void Update()
	{
		moveInput=Input.GetAxisRaw("Horizontal");		
	}
	private void FixedUpdate()
	{
		if(moveInput!=0)
		{
			for(int i = 0;i<wheels.Count;i++)
			{
				Rigidbody2D rb = wheels[i];
				if(Mathf.Abs(rb.angularVelocity)<1200) rb.AddTorque(-moveInput*torqueSpeed*Time.fixedDeltaTime);				
				if(rb.velocity.normalized.x*moveInput<0) 
					rb.AddForce(-rb.velocity,ForceMode2D.Impulse);
			}
			//carRb.AddForce(new Vector2(-moveInput*forceSpeed*Time.fixedDeltaTime,0));
		}
	}
	private void BreakForce()
	{
		
	}
}
