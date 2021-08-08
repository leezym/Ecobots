using UnityEngine;
using System.Collections;
using Globales;

public class AnguilaElectrica : MonoBehaviour {
	
	float Speed = 1.5f, LeftDir, RightDir;
	bool patrulla = true, Destruction = false, Seguir = false, Ice = false;
	Vector2 WalkDistance, Restriccion, res, R;
	Vector3 Move;
	BoxCollider2D Coll;
	public GameObject pj, IceCube, Explosion;
	public int Dam, Health = 50;
	Shooting Shoot;
	
	void Start () 
	{
		pj = GameObject.FindGameObjectWithTag("Player");
		Shoot = GameObject.FindWithTag ("Player").GetComponent<Shooting> ();
		Coll = GetComponent<BoxCollider2D> ();
		LeftDir = transform.position.x - 2.5f;
		RightDir = transform.position.x + 2.5f;
	}
	
	// Patrullaje
	void Update () 
	{
		Restriccion = transform.position -  pj.transform.position;
		if(patrulla == true)
		{
			WalkDistance.x = Speed * Time.deltaTime;
			if(transform.position.x >= RightDir)
			{
				transform.eulerAngles = new Vector2 (0, -180);
			} 
			if(transform.position.x <= LeftDir)
			{
				transform.eulerAngles = new Vector2 (0, 0);
			}
			
			transform.Translate (WalkDistance);
		}

		if(Destruction)
		{
			Destroy(gameObject,5);
		}

		if(Ice)
		{
			IceCube.gameObject.SetActive(true);
			Invoke("DisableIce",5);
		}

		if(Health <=0)
		{
			Explosion.SetActive(true);
			//gameObject.SetActive(false);
			Destroy (gameObject);
		}
	}

	void DisableIce()
	{
		Ice = false;
		IceCube.gameObject.SetActive (false);
	}

	void Electrocutar()
	{
		GameController.data.sliderHealth.value -= Dam;
		GameController.data.sliderHealthP.value -= Dam;
	}
		
	void OnTriggerEnter2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			if(Ice == false)
			{
				patrulla = false;
				transform.Translate(Restriccion);
				Seguir = true;
			}
		}
	}

//	void OnTriggerStay2D(Collider2D Tar)
//	{
//		if(Tar.gameObject.tag == "Fire")
//		{
//			Health -= Shoot.Damage;
//		}
//
//	}
	
//	void OnCollisionEnter2D(Collision2D Col)
//	{
//		if(Col.gameObject.tag == "Player")
//		{
//			Destruction = true;
//			InvokeRepeating("Electrocutar",0,1);
//			Coll.enabled =false;
//		}
//
//		if(Col.gameObject.tag == "Bullet")
//		{
//			Health -= Shoot.Damage;
//			Destroy(Col.gameObject);
//		}
//
//		if(Col.gameObject.tag == "Electric")
//		{
//			Health -= Shoot.Damage;
//			Destroy(Col.gameObject);
//		}
//
//		if(Col.gameObject.tag == "Ice")
//		{
//			Ice = true;
//			Health -= Shoot.Damage;
//		}
//	}

}
