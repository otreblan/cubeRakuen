using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class creaBordesScript : MonoBehaviour {

	EdgeCollider2D colli;
	// Use this for initialization
	void Start () {
		colli = GetComponent<EdgeCollider2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
