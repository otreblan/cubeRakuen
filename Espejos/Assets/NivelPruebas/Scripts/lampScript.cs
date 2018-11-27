using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampScript : MonoBehaviour {

	[Tooltip("La luz para spawnear")]
	public GameObject luz;
	public LinkedList<GameObject> luces = new LinkedList<GameObject>();

	//Transform spawnPoint;
	// Use this for initialization
	void Start () {
		//luzScript.luz = luz;
		//spawnPoint = GetComponentInChildren<Transform>();
		lanzaLuz();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void lanzaLuz()
	{
		while(true)
		{
			if(luces.Last == null)
			{
				break;
			}
			Destroy(luces.Last.Value);
			luces.RemoveLast();
		}
		GameObject Luz = Instantiate(luz, transform.position, transform.rotation, transform);
		luzScript luzScr = Luz.GetComponent<luzScript>();
		luzScr.luz = this.luz;
		luzScr.lamp = this;
	}
}
