using UnityEngine;
using System.Collections;
using Globales;

public class RoboAve : dropItems {

	public Transform SpawnBullet;

	public float BulletSpeed;
	public float Health = 50;

	public Rigidbody2D Bullet;

	public GameObject Target;
	public GameObject Explosion;

	float Dis;
	float CountShoot;
	float TimeShoot;

	bool Drop = true;

	public bool Shooting; 

	BoxCollider2D Coll;

	bool Mecanicas = true;

	void Start()
	{
		Coll = GetComponent<BoxCollider2D> ();
		Target = GameObject.FindWithTag ("Player");
	}

	void Update () 
	{
		startDrop ();

		if(Target == null)
		{
			Target = GameObject.FindWithTag ("Player");
		}

		if(Mecanicas)
		{
			CalculoDistancia ();
			RotationEnemy();

			if(Shooting)
			{
				Disparo();
			}
		}

		if(Health <=0)
		{
			Mecanicas = false;

			if(Drop)
			{
				randomDrop ();
				Drop = false;
			}

			StartCoroutine (Muerte ());
		}
		Destroy(GameObject.Find("bave(Clone)"),3f);
	}

	//Efecto de Muerte
	IEnumerator Muerte()
	{
		Coll.isTrigger = true;
		Explosion.SetActive(true);
		Shooting = false;
		yield return new WaitForSeconds (2.5f);

		Destroy (gameObject);
	}

	void Disparo()
	{	
		if(CountShoot <=3.5f )
		{
			if(TimeShoot <= 0)
			{
				Rigidbody2D Shoting;

				if(Dis < 0)
				{
					Shoting = Instantiate(Bullet,SpawnBullet.transform.position,SpawnBullet.transform.rotation) as Rigidbody2D;
					Shoting.AddForce(new Vector2(-5,-7) * BulletSpeed);
				}

				if(Dis > 0)
				{
					Shoting = Instantiate(Bullet,SpawnBullet.transform.position,SpawnBullet.transform.rotation) as Rigidbody2D;
					Shoting.AddForce(new Vector2(5,-7) * BulletSpeed);
				}
				TimeShoot = 1;
			}
			CountShoot += Time.deltaTime;
			TimeShoot -= Time.deltaTime;
		}
		if(CountShoot > 3.5f)
		{
			CountShoot = 0;
		}
	}

	void CalculoDistancia()
	{
		Dis = (Target.transform.position.x - transform.position.x);
	}

	void RotationEnemy()
	{
		if(Dis > 0)
		{
			transform.eulerAngles = new Vector2 (0,180);
		}
		if(Dis < 0)
		{
			transform.eulerAngles = new Vector2 (0,0);
		}
	}

	void OnCollisionEnter2D(Collision2D Other)
	{
		if(Other.gameObject.tag == "Bullet")
		{
			Health -= GameController.DisparoBase; 
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Electric" )
		{
			Health -= GameController.ElectricShoot;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Energy")
		{
			Health -= GameController.EnergyBall;
			Destroy(Other.gameObject);
		}
	}
		
	public void OnTriggerStay2D(Collider2D Other)
	{
		if(Other.gameObject.tag == "Fire")
		{
			Health -= GameController.IceAndFire;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Ice")
		{
			Health -= GameController.IceAndFire;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Tornado")
		{
			Health -= GameController.Tornadito;
			Destroy(Other.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Shooting = true;
		}
	}

	void OnTriggerExit2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Shooting = false;
		}
	}
}
