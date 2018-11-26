using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giraScript : MonoBehaviour {

	public Color color;
	public Color color2;
	public GameObject girado;

	bool existGirado;

	Quaternion rotTemp;

	Camera camara;
	// Use this for initialization
	[Tooltip("la posición del touch")]
	Vector2 pos;

	
	void Start () {
		//yield return new WaitUntil(()=> Camera.);
		camara = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		existGirado = girado;
		if(existGirado)
		{
			girado.transform.rotation = Quaternion.Lerp(girado.transform.rotation, rotTemp, Time.deltaTime*5f);
		}
		if(Input.touchCount > 0)
		{
			pos = camara.ScreenToWorldPoint(Input.GetTouch(0).position);
			switch(Input.GetTouch(0).phase)
			{
				case TouchPhase.Began:
					//print(0);
					//RaycastHit hit;
					//Ray rayo = camara.ScreenPointToRay(camara.WorldToScreenPoint(Input.GetTouch(0).position));
					//RaycastHit2D hit2D = Physics2D.Raycast();
					//print(Input.GetTouch(0).position);
					RaycastHit2D hit2D = Physics2D.Raycast(pos,Vector2.zero);
					if(hit2D)
					{
						if(existGirado)
						{
							girado.GetComponent<SpriteRenderer>().color = color;
							
						}
						//print(1);
						
						girado = hit2D.transform.gameObject;
						rotTemp = girado.transform.rotation;
						hit2D.transform.GetComponent<SpriteRenderer>().color = color2;
						
						
					}
					goto case TouchPhase.Moved;
				case TouchPhase.Stationary:
				case TouchPhase.Moved:
					
					if(girado)
					{
						Vector2 distan = pos - (Vector2)girado.transform.position;
						rotTemp = Quaternion.Euler(0f,0f,Mathf.Atan2(distan.y, distan.x)*Mathf.Rad2Deg);
					}
					break;
			}
		}
	}
}
