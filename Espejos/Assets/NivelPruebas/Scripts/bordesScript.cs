using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class bordesScript : MonoBehaviour {

	Camera camara;
	EdgeCollider2D colli;
	Vector2[] edges;

	Transform camaraTransform;
	// Use this for initialization
	void Start () {
		camara = Camera.main;
		colli = GetComponent<EdgeCollider2D>();
		camaraTransform = camara.transform;
		edges = new Vector2[4]{
			camara.ViewportToWorldPoint(new Vector2(0f,1f)),
			camara.ViewportToWorldPoint(new Vector2(0f,0f)),
			camara.ViewportToWorldPoint(new Vector2(1f,0f)),
			camara.ViewportToWorldPoint(new Vector2(1f,1f))
		};
		for(int ii = 0; ii < edges.Length; ii++)
		{
			edges[ii] -= (Vector2)camaraTransform.position;
		}

		colli.points = edges;
		Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
