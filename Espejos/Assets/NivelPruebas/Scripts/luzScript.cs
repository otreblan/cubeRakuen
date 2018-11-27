using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luzScript : MonoBehaviour {

	///<sumary>
	///El espejo desde el que rebota
	///</sumary>
	public int espejo;
	public float luzVel = 1f;
	[HideInInspector]
	public GameObject luz;
	[HideInInspector]
	public lampScript lamp;
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
		lamp.luces.AddLast(this.gameObject);
		filtro.useLayerMask = true;
		filtro.SetLayerMask(LayerMask.GetMask("Planos"));
	}
	
	// Update is called once per frame
	void Update () {
		gameManaScript.gameMana.setDistance(transform.position.y+(transform.rotation*transform.localScale/10f).y-lamp.transform.position.y);

		largoLuz.x += Time.deltaTime* luzVel;
		
		transform.localScale = largoLuz;//para obtener el largo en Unity Units hay que dividirlo entre 10

		hitCount = Physics2D.Raycast(transform.position,transform.rotation*Vector2.right,filtro,hits,largoLuz.x/10f);
		for(int ii = 0; ii < hitCount; ii++)
		{
			if(espejo == hits[ii].collider.GetInstanceID())
			{
				print("continue");
				continue;
			}
			switch(hits[ii].collider.tag)
			{
				case "Espe":
					//angle = Vector2.Angle(transform.rotation*Vector2.right, -hits[ii].normal);
					//print("espe");
					/* if(Quaternion.Angle(transform.rotation, hits[ii].transform.rotation) > 90f)
					{
						ricoVector = Vector2.Reflect(transform.rotation*Vector2.right, hits[ii].normal);
						ricochetAngle = Quaternion.Euler(0f,0f, Mathf.Atan2(ricoVector.y,ricoVector.x)*Mathf.Rad2Deg);
						Instantiate(luz,hits[ii].point, ricochetAngle).GetComponent<luzScript>().espejo = hits[ii].collider.GetInstanceID();
					}*/
					ricoVector = Vector2.Reflect(transform.rotation*Vector2.right, hits[ii].normal);
					//print(hits[ii].normal);
					//print(transform.rotation*Vector2.right);
					//print(Vector2.Reflect(transform.rotation*Vector2.right, hits[ii].normal));
					//print(Mathf.Atan2(ricoVector.y,ricoVector.x)*Mathf.Rad2Deg);
					ricochetAngle = Quaternion.Euler(0f,0f, Mathf.Atan2(ricoVector.y,ricoVector.x)*Mathf.Rad2Deg);
					//Instantiate(luz,hits[ii].point, ricochetAngle).GetComponent<luzScript>().espejo = hits[ii].collider.GetInstanceID();
					luzScript luzScr = Instantiate(luz,hits[ii].point, ricochetAngle).GetComponent<luzScript>();
					luzScr.espejo = hits[ii].collider.GetInstanceID();
					luzScr.luz = this.luz;
					luzScr.lamp = this.lamp;
					
					
					gameManaScript.gameMana.mueve(transform.position, hits[ii].point);
					break;
				case "Opac":
					print("opac");
					gameManaScript.gameMana.rreset();
					//Destroy(this.gameObject);
					break;
				default:
					print("default");
					break;
			}
			
			Destroy(this);
		}
	}
}
