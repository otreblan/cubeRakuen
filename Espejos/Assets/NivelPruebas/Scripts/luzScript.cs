using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luzScript : MonoBehaviour {

	///<sumary>
	///El espejo desde el que rebota
	///</sumary>
	public int espejo;
	public float luzVel = 1f;
	static public GameObject luz;
	//public GameObject Luz;
	Vector3 largoLuz = Vector3.up+Vector3.forward;
	//SpriteRenderer sprite;
	int hitCount = 0;
	ContactFilter2D filtro;
	///<sumary>
	///El ángulo absouto entre la luz y la normal
	///</sumary>
	//float angle;
	///<sumary>
	///El vector del rebote, de aquí se saca el ángulo
	///</sumary>
	Vector2 ricoVector;
	///<sumary>
	///El ángulo del nuevo rayo de luz
	///</sumary>
	Quaternion ricochetAngle;
	RaycastHit2D[] hits = new RaycastHit2D[5];
	// Use this for initialization
	void Start () {
		filtro.useLayerMask = true;
		filtro.SetLayerMask(LayerMask.GetMask("Planos"));
	}
	
	// Update is called once per frame
	void Update () {
		largoLuz.x += Time.deltaTime* luzVel;
		
		transform.localScale = largoLuz;//para obtener el largo en Unity Units hay que dividirlo entre 10

		hitCount = Physics2D.Raycast(transform.position,transform.rotation*Vector2.right,filtro,hits,largoLuz.x/10f);
		for(int ii = 0; ii < hitCount; ii++)
		{
			if(espejo == hits[ii].collider.GetInstanceID())
			{
				continue;
			}
			switch(hits[ii].transform.tag)
			{
				case "Espe":
					//angle = Vector2.Angle(transform.rotation*Vector2.right, -hits[ii].normal);
					if(Quaternion.Angle(transform.rotation, hits[ii].transform.rotation) > 90f)
					{
						ricoVector = Vector2.Reflect(transform.rotation*Vector2.right, hits[ii].normal);
						ricochetAngle = Quaternion.Euler(0f,0f, Mathf.Atan2(ricoVector.y,ricoVector.x)*Mathf.Rad2Deg);
						Instantiate(luz,hits[ii].point, ricochetAngle).GetComponent<luzScript>().espejo = hits[ii].collider.GetInstanceID();
					}
					break;
				case "Opac":

					break;
			}
			
			Destroy(this);
		}
	}
}
