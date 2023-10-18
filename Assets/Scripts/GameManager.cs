using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;

	[SerializeField] private float scrollSpeed,lensSizeMax;
	public CinemachineVirtualCamera cinemachine;
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
	private void Update()
	{
		float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
		if((scroll<0&&cinemachine.m_Lens.OrthographicSize<lensSizeMax)||(scroll>0))
			cinemachine.m_Lens.OrthographicSize-=scroll*scrollSpeed*Time.deltaTime;
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
