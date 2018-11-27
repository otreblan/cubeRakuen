using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManaScript : MonoBehaviour {

	[HideInInspector]
	static public gameManaScript gameMana;

	public lampScript lamp;
	public mueveCamaraScript mueveCamara;
	// Use this for initialization
	[Tooltip("Los espejos para spawnear")]
	public GameObject espejoPreFab;
	public Transform espejoSpawnPoint;
	public Text distancia;
	GameObject ultimoEspejo;

	LinkedList<GameObject> espejos;

	GameObject camara;
	Camera camaraCamara;
	void Start () {
		espejos = new LinkedList<GameObject>();
		camara = GameObject.FindGameObjectWithTag("MainCamera");
		camaraCamara = camara.GetComponent<Camera>();
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

		while(true)
		{
			Destroy(espejos.Last.Value);
			espejos.RemoveLast();
			if(espejos.First == null)
			{
				break;
			}
		}
		for (int ii = 0; ii < 5; ii++)
		{
			espejoSpawn();
		}
		StartCoroutine(lanzaLuzCorru());
	}
	IEnumerator lanzaLuzCorru()
	{
		yield return new WaitUntil(()=> camara.transform.position.y < 3f);
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
		//newPos -= (camaraCamara.ViewportToWorldPoint(new Vector2(0.5f,0.25f)- (Vector2)camara.transform.position));
		//print("newpos: "+ newPos.y);
		if(newPos.y > 0f)
		{
			mueveCamara.enfoque.position = newPos;
		}
		espejoSpawn();

	}
	/// <summary>
	/// Spawnea un espejo, necesita la posición del anterior espejo
	/// </summary>
	void espejoSpawn()
	{
		if(espejos.Last == null)
		{
			espejos.AddFirst(Instantiate(espejoPreFab,espejoSpawnPoint.position,Quaternion.Euler(0f,0f,180f+Random.Range(-10f,10f))));
		}
		else
		{
			ultimoEspejo = espejos.First.Value;
			Vector2 newPos = new Vector2(-ultimoEspejo.transform.position.x,ultimoEspejo.transform.position.y + Random.Range(1f,3f));
			Quaternion newRot = Quaternion.Euler(0f,0f,(ultimoEspejo.transform.position.x < 0 ? 180f : 0f)+Random.Range(-10f, 10f));
			espejos.AddFirst(Instantiate(espejoPreFab, newPos, newRot));
		}
		print(espejos.Count);
		if(espejos.Count > 7)
		{
			Destroy(espejos.Last.Value);
			espejos.RemoveLast();
		}
	}
	public void setDistance(float dis)
	{
		distancia.text = "Dis: "+ dis.ToString("0.00");
	}
}
