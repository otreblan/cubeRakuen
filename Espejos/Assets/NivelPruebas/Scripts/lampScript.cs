using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampScript : MonoBehaviour {

	public GameObject luz;

	//Transform spawnPoint;
	// Use this for initialization
	void Start () {
		luzScript.luz = luz;
		//spawnPoint = GetComponentInChildren<Transform>();
		Instantiate(luz, transform.position, transform.rotation, transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
