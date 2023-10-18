using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;
	[HideInInspector] public DrawManager dm;
	[HideInInspector] public CarConstructor constructor;
	private Camera mainCam;
	
	
	private Transform _selectedButton;
	public Transform selectedButton
	{
		get => _selectedButton;
		set
		{
			_selectedButton=value;
		}
	}
	private void Awake()
	{
		Application.targetFrameRate=40;
		if(gm==null)
			gm=this;
	}
	private void Start()
	{
		Init();
			
	}
	private void Init()
	{
		dm=transform.GetChild(0).GetComponent<DrawManager>();
		constructor=transform.GetChild(1).GetComponent<CarConstructor>();
		mainCam=Camera.main;
		dm.mainCam=mainCam;
		constructor.mainCam=mainCam;
	}
	
    public void ReloadScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
	public void Quit()
	{
		Application.Quit();
	}
}
