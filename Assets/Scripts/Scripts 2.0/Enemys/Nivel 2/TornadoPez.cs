using UnityEngine;
using System.Collections;

public class TornadoPez : MonoBehaviour {
	
	Vector3 Move;
	bool Empuje = false;
	GameObject Player,PezGlobo;
	PezGloboEnemy Pez;

	void Start()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		PezGlobo = GameObject.FindGameObjectWithTag ("PezGlobo");
		Pez = PezGlobo.gameObject.GetComponent<PezGloboEnemy>();
	}

	void Update()
	{
		if(Empuje == true)
		{
			Movimiento();
		}
	}

	void Movimiento()
	{
		Move = new Vector3(-15,0,0) * Time.deltaTime;
		Player.transform.Translate(Move);
		//		if(Pez.Dis < 0)
//		{
//			Move = new Vector3(-7,0,0) * Time.deltaTime;
//			Player.transform.Translate(Move);
//		}
//
//		if(Pez.Dis > 0)
//		{
//			Move = new Vector3(-7,0,0) * Time.deltaTime;
//			Player.transform.Translate(Move);
//		}
	}

	void Desactivar()
	{
		Empuje = false;
	}

	void OnTriggerEnter2D(Collider2D Tar)
	{
		if(Tar.tag == "Player")
		{
			Empuje = true;
		}
	}

	void OnTriggerExit2D(Collider2D Tar)
	{
		if(Tar.tag == "Player")
		{
			Invoke("Desactivar",2f);
		}
	}
}
