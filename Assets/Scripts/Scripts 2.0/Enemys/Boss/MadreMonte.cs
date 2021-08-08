using UnityEngine;
using System.Collections;
using Globales;

public class MadreMonte : MonoBehaviour {

	public float BulletSpeed;
	public Rigidbody2D SuperBullet;
	public GameObject Pilares;
	public GameObject Bomba;
	public GameObject Explosion;
	public GameObject[] Posiciones;
	public Transform[] SpawnFloor;
	public Transform SpawnShoot;
	float Dis = 0;
	Vector3 move;
    int Control = 0;
	GameObject Player, Enemigo, pilar, bomba;
	public Animator Ani;

    public Army_Menu arm;

	void Start() 
	{
        arm = GameObject.Find("ScriptCanvas").GetComponent<Army_Menu>();
		Ani = GetComponent<Animator> ();

		if (GameController.muerto3) {
			Destroy(gameObject);
		}
			
		Player = GameObject.FindWithTag ("Player");
	}

	void Update()
	{
        if (Player == null) {
            Player = GameObject.FindWithTag("Player");           
        }

		if (GameController.data.sliderHealthBoss.value == GameController.data.sliderHealthBoss.minValue)
        {
            StartCoroutine(Muerte());
        }

        if (GameController.Mecanicas)
        {
            if (Control == 0)
            {
                StartCoroutine(Boss());
                Control++;
            }
        }
        Destroy(GameObject.Find("ShootEnergy(Clone)"), 2f);
	}

	IEnumerator Boss() 
	{
		while (GameController.Mecanicas)
		{
			//Posicionamiento Punto1
			while (transform.position.x != Posiciones[0].transform.position.x)
			{
				transform.position = new Vector3(Posiciones[0].transform.position.x, Posiciones[0].transform.position.y, Posiciones[0].transform.position.z) ;
			}

			yield return new WaitForSeconds(.3f);

			CalculoDistancia();

			yield return new WaitForSeconds(.3f);

			RotationEnemy();

			yield return new WaitForSeconds(1f);

			Ani.SetBool ("Zarzas",true);

			//Spawn Piso
			for (int i=0;i<SpawnFloor.Length;i+=2)
			{
				pilar = Instantiate(Pilares, SpawnFloor[i].position, Quaternion.identity) as GameObject;
				pilar.transform.Translate(0,3,0);
				Destroy(pilar.gameObject, 3f);
			}

			yield return new WaitForSeconds(4f);
			
			for (int i=1;i<SpawnFloor.Length;i+=2)
			{
				pilar = Instantiate(Pilares, SpawnFloor[i].position, Quaternion.identity) as GameObject;
				pilar.transform.Translate(0,3,0);
				Destroy(pilar.gameObject, 3f);
			}

			yield return new WaitForSeconds(4);

			Ani.SetBool ("Zarzas",false);
			Ani.SetBool ("EnergyBall", true);

			int NumShoot =0;

			//Contador de Disparos
			while (NumShoot < 1)
			{
				Rigidbody2D Shoting;
				Shoting = (Rigidbody2D)Instantiate(SuperBullet, SpawnShoot.position, Quaternion.identity);
				Shoting.velocity = transform.right * BulletSpeed;
				NumShoot++;
			}

			//Reiniciar Contador
			if (NumShoot != 0)
			{
				NumShoot = 0;
			}

			Ani.SetBool ("EnergyBall", false);

			yield return new WaitForSeconds(4);

			//Posicionamiento Punto2
			while (transform.position.x != Posiciones[1].transform.position.x)
			{
				transform.position = new Vector3(Posiciones[1].transform.position.x, Posiciones[1].transform.position.y, Posiciones[1].transform.position.z); 
			}

			yield return new WaitForSeconds(.3f);

			CalculoDistancia();

			yield return new WaitForSeconds(.3f);

			RotationEnemy();

			yield return new WaitForSeconds(1f);

			Ani.SetBool ("Zarzas",true);

			//Spawn Piso
			for (int i=1;i<SpawnFloor.Length;i+=2)
			{
				pilar = Instantiate(Pilares, SpawnFloor[i].position, Quaternion.identity) as GameObject;
				pilar.transform.Translate(0,3,0);
				Destroy(pilar.gameObject, 3f);
			}

			yield return new WaitForSeconds(3.5f);

			for (int i=0;i<SpawnFloor.Length;i+=2)
			{
				pilar = Instantiate(Pilares, SpawnFloor[i].position, Quaternion.identity) as GameObject;
				pilar.transform.Translate(0,3,0);
				Destroy(pilar.gameObject, 3f);
			}

			yield return new WaitForSeconds(4);

			Ani.SetBool ("Zarzas",false);
			Ani.SetBool ("Explosion",true);

			//Explosion
			for(int i=0; i<5; i++ )
			{	
				bomba = Instantiate(Bomba, SpawnFloor[Random.Range(0,8)].position, Quaternion.identity) as GameObject;
				Destroy(bomba.gameObject, 3f);
			}

			yield return new WaitForSeconds(1);
			bomba.GetComponent<Collider2D>().enabled = true;

			Ani.SetBool ("Explosion",false);

			yield return new WaitForSeconds(2);
		}
	}

	public void CalculoDistancia()
	{
		Dis = (Player.transform.position.x - transform.position.x);
	}

	public void RotationEnemy()
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

	void OnCollisionEnter2D(Collision2D Other)
	{
		if(Other.gameObject.tag == "Bullet")
		{
			GameController.data.sliderHealthBoss.value -= GameController.DisparoBase; 
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Electric" )
		{
			GameController.data.sliderHealthBoss.value -= GameController.ElectricShoot;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Energy")
		{
			GameController.data.sliderHealthBoss.value -= GameController.EnergyBall;
			Destroy(Other.gameObject);
		}
	}


	public void OnTriggerStay2D(Collider2D Other)
	{
		if(Other.gameObject.tag == "Fire")
		{
			GameController.data.sliderHealthBoss.value -= GameController.IceAndFire;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Ice")
		{
			GameController.data.sliderHealthBoss.value -= GameController.IceAndFire;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Tornado")
		{
			GameController.data.sliderHealthBoss.value -= GameController.Tornadito;
			Destroy(Other.gameObject);
		}
	}

	IEnumerator Muerte()
	{
		GameController.Mecanicas = false;
		GameController.ActiveInputs = false;
		GameController.isActive = true;
		GameController.isActiveBossDead = true;
		Explosion.SetActive(true);        

		yield return new WaitForSeconds(5f);

		Destroy(gameObject);
		Application.LoadLevel("Menu");
		GameController.muerto3 = true;
		GameController.lvl = 0;
		GameController.isActive = false;
		GameController.isActiveBoss = false;
		GameController.isActiveBossDead = false;
		GameController.data.gameArma.enabled = false;
		GameController.data.gameLife.enabled = false;
		GameController.data.HUD_canvas.enabled = false;
		GameController.data.Enemy_canvas.enabled = true;
		arm.Resetear();
		arm.PlaySound();
	}
}