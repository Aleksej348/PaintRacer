using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	public List<Rigidbody2D> wheels;
	[SerializeField] private float torqueSpeed,forceSpeed;
	private float moveInput;
	private DrawManager _dm;
	private DrawManager dm => _dm??=GameManager.gm.dm;
	private void Start()
	{		
		torqueSpeed=6000;		
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
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag=="Basket")
		{
			dm.PaintValue+=30;
			collision.gameObject.SetActive(false);
		}
	}
	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if(collision.transform.tag=="Basket")
	//	{
	//		dm.PaintValue+=30;
	//		collision.gameObject.SetActive(false);
	//	}
	//}

}
