using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManaScript : MonoBehaviour {

	[HideInInspector]
	static public gameManaScript gameMana;

	public lampScript lamp;
	public mueveCamaraScript mueveCamara;
	// Use this for initialization
	[Tooltip("Los espejos para spawnear")]
	public GameObject espejos;
	public Transform espejoSpawnPoint;
	GameObject ultimoEspejo;
	void Start () {
		if(!gameMana)
		{
			gameMana = this;
		}
		for (int ii = 0; ii < 5; ii++)
		{
			espejoSpawn();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void rreset()
	{
		mueveCamaraScript.rreset();
		lamp.lanzaLuz();
	}

	///<sumary>
	///Esta función mueve el enfoqueCamara a la mitad del eje Y entre 2 espejos
	///</sumary>
	///<param name= "vec1"> cosa</param>
	public void mueve(Vector2 vec1, Vector2 vec2)
	{
		Vector3 newPos = new Vector3(
			mueveCamara.transform.position.x,
			Mathf.Lerp(vec1.y, vec2.y, 0.5f),
			mueveCamara.transform.position.z
		);
		//print("newpos: "+ newPos.y);
		if(newPos.y > 0f)
		{
			mueveCamara.enfoque.position = newPos;
		}

	}
	/// <summary>
	/// Spawnea un espejo, necesita la posición del anterior espejo
	/// </summary>
	void espejoSpawn()
	{
		if(!ultimoEspejo)
		{
			ultimoEspejo = Instantiate(espejos,espejoSpawnPoint.position,Quaternion.Euler(0f,0f,180f+Random.Range(-10f,10f)));
		}
		else
		{
			Vector2 newPos = new Vector2(-ultimoEspejo.transform.position.x,ultimoEspejo.transform.position.y + Random.Range(2f,6f));
			Quaternion newRot = Quaternion.Euler(0f,0f,(ultimoEspejo.transform.position.x < 0 ? 180f : 0f)+Random.Range(-10f, 10f));
			ultimoEspejo = Instantiate(espejos, newPos, newRot);
		}
	}
}
