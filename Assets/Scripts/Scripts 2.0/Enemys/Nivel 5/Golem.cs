using UnityEngine;
using System.Collections;
using Globales;

public class Golem : dropItems {

	public float BulletSpeed;
    public float Health = 50;

	public GameObject SpawnBullet,Explosion, Target;

	public Rigidbody2D  Bullet;

	float Dis;
    float CountShoot;
    float TimeShoot;

	bool Detected;
	bool Drop = true;

    public Animator Anim;

    BoxCollider2D Coll;

	bool Mecanicas = true;

	void Start () 
	{
        Target = GameObject.FindGameObjectWithTag("Player");
        Anim = GetComponent<Animator>();
        Coll = GetComponent<BoxCollider2D>();
	}
	
	void Update () 
	{
		startDrop ();

       if(Anim == null)
       {
           Anim = GetComponent<Animator>();
       }

        if(Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("Player");
        }

		if(Mecanicas)
		{
			Anim.SetBool("Ataque",false);

			CalculoDistancia ();

			if(Detected)
			{
				Disparo();
				RotationEnemy();
				Anim.SetBool("Ataque", true);
			}
		}
       
        if(Health < 1)
        {
			Mecanicas = false;

			if(Drop)
			{
				randomDrop ();
				Drop = false;
			}
			StartCoroutine(Muerte());
        }

        Destroy(GameObject.Find("BulletYeti(Clone)"), 3f);
	}

    //Efecto de Muerte
    IEnumerator Muerte()
    {
        Coll.isTrigger = true;
        Detected = false;
        Explosion.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);
    }

	void Disparo()
	{
		if(TimeShoot < 0)
		{
			Rigidbody2D Shoting;
			Shoting = Instantiate(Bullet,SpawnBullet.transform.position,SpawnBullet.transform.rotation) as Rigidbody2D;
            Shoting.velocity = transform.right * BulletSpeed;
			TimeShoot = 1;
		}
		TimeShoot -= Time.deltaTime;
	}

	void CalculoDistancia()
	{
		Dis = (Target.transform.position.x - transform.position.x);
	}

	void RotationEnemy()
	{
		if(Dis > 0)
		{
			transform.eulerAngles = new Vector2 (0,0);
		}
		if(Dis < 0)
		{
			transform.eulerAngles = new Vector2 (0,180);
		}
	}

    //Collisiones
    void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.tag == "Bullet")
        {
            Health -= GameController.DisparoBase;
            Destroy(Other.gameObject);
        }

        if (Other.gameObject.tag == "Electric")
        {
            Health -= GameController.ElectricShoot;
            Destroy(Other.gameObject);
        }

        if (Other.gameObject.tag == "Energy")
        {
            Health -= GameController.EnergyBall;
            Destroy(Other.gameObject);
        }
    }


    public void OnTriggerStay2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "Fire")
        {
            Health -= GameController.IceAndFire;
        }

        if (Other.gameObject.tag == "Ice")
        {
            Health -= GameController.IceAndFire;
        }

        if (Other.gameObject.tag == "Tornado")
        {
            Health -= GameController.Tornadito;
            Destroy(Other.gameObject);
        }
    }

	
	void OnTriggerEnter2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Detected = true;	
		}
	}
	
	void OnTriggerExit2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Detected = false;
		}
	}
}
