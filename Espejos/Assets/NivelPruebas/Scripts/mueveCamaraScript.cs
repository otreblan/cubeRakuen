using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mueveCamaraScript : MonoBehaviour {
	[Tooltip("El Transform al que seguir")]
	public Transform enfoque;
	[Tooltip("La velocidad a la que se sigue al Transform")]
	public float vel = 1f;

	static Vector3 originPoint;
	static mueveCamaraScript mueveCamara;
	Vector3 position;
	// Use this for initialization
	void Start () {
		if(!mueveCamara)
		{
			mueveCamara = this;
		}
		if(enfoque)
		{
			originPoint = enfoque.transform.position;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		//enfoque.position += Vector3.up*(Time.deltaTime*5f);
		position = Vector3.Lerp(transform.position,enfoque.position ,Time.deltaTime*vel);
		position.z = transform.position.z;
		//print(position);
		transform.position = position;
	}
	static public void rreset()
	{
		print("reset");
		mueveCamara.enfoque.position = mueveCamaraScript.originPoint;
	}
}
