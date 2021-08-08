using UnityEngine;
using System.Collections;
using Globales;

public class Gorilla : MonoBehaviour {

	public Transform Target;

	public float BulletSpeed; 
    public float Health = 50;

	public GameObject SpawnBullet;
    public GameObject Explosion;

	public Rigidbody2D  Bullet;

	float Dis,CountShoot,TimeShoot;

	public bool Detected;

    Animator Ani;

	// Use this for initialization
	void Start () 
	{
		Target = GameObject.FindWithTag ("Player").transform;
        Ani = GetComponent<Animator>();
	}

	void Update () 
	{
		CalculoDistancia ();

        Ani.SetBool("Ataque",false);

        if(Detected)
        {
            Ani.SetBool("Ataque",true);
            Disparo();
        }

		if(Health < 1)
		{
			Explosion.SetActive(true);
			//gameObject.SetActive(false);
			Destroy (gameObject);
		}
		Destroy(GameObject.Find("BulletGorilla(Clone)"),3f);
	}

    //Mecanica de disparo
    void Disparo()
    {
        if (CountShoot <= 3.5f)
        {
            if (TimeShoot <= 0)
            {
                Rigidbody2D Shoting;
                Shoting = Instantiate(Bullet, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
                Shoting.velocity = transform.right * BulletSpeed;
                TimeShoot = 1;
            }
            CountShoot += Time.deltaTime;
            TimeShoot -= Time.deltaTime;
        }
        if (CountShoot > 3.5f)
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
            Destroy(Other.gameObject);
        }

        if (Other.gameObject.tag == "Ice")
        {
            Health -= GameController.IceAndFire;
            Destroy(Other.gameObject);
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
