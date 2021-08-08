using UnityEngine;
using System.Collections;
using Globales;

public class Shooting : MonoBehaviour {

	// Variables Publicas
	public float BulletSpeed;
	public GameObject SpawnBullet,Fire,Ice,ChargeParticle;
	public Rigidbody2D [] Bullet;
	public Rigidbody2D Tornado,ElectricShoot;
	[HideInInspector]
	public bool Disparo = true,Charge;
	public bool Shoot;
	// Variables Privadas
	private Vector2 Move;
	private float TimeCharge = 0.0f, TimeShoot = 0.0f;
	[HideInInspector]
	public int LVL = 0, bala = 1;
	private int Special =1;
	Rigidbody2D Shoting;

	void Update () 
	{
    	if(GameController.ActiveInputs)
		{
			if(Disparo)
			{
				SliderArmy();
				TimeShoot += Time.deltaTime;
				Destroy(GameObject.Find("Shoot Lvl 1(Clone)"),3f);
				Destroy(GameObject.Find("Shoot Lvl 2(Clone)"),3f);
				Destroy(GameObject.Find("Shoot Lvl 3(Clone)"),3f);
				Destroy(GameObject.Find("Shoot Special(Clone)"),3f);
			}
			//Deshabilita Disparo, Habilidad de espectro
			if(!Disparo)
			{
				Invoke("Change",10);
			}
		}
	
	}

	void Change()
	{
		Disparo = true;
	} 
		
	// Disparo arma, barra de municion
	void SliderArmy(){	


		if (Input.GetKey(KeyCode.X) && GameController.data.sliderA [GameController.armas].value > 0 && GameController.data.Pause_canvas.enabled == false) 
		{
			Shoot = true;

			if(GameController.armas == 0)
			{
				TimeCharge += Time.deltaTime;

				if (TimeCharge > 1)
				{
					ChargeParticle.SetActive (true);
				}
			}

			if (GameController.armas == 1){
				Fire.gameObject.SetActive (true);
				GameController.data.sliderA [1].value -= bala * Time.deltaTime ;
				GameController.data.sliderPA [1].value -= bala * Time.deltaTime ;
			}
			if (GameController.armas == 5){
				Ice.gameObject.SetActive (true);
				GameController.data.sliderA [5].value -= bala * Time.deltaTime;
				GameController.data.sliderPA [5].value -= bala * Time.deltaTime;

			}
		}
		else 
		{
			Shoot = false;				
			Fire.SetActive(false);
			Ice.SetActive(false);
		}

		if (Input.GetKeyUp (KeyCode.X) && GameController.data.sliderA [GameController.armas].value > 0 && GameController.data.Pause_canvas.enabled == false) {

			Shoot = true;

			if (GameController.armas == 0) {

				if (TimeCharge > 0f && TimeCharge < 1.5f) {
					LVL = 0;
					TimeCharge = 0f;
					GameController.DisparoBase = 5;
					ChargeParticle.SetActive (false);
				}
				if (TimeCharge > 1.5f && TimeCharge < 2.5f) {
					LVL = 1;
					TimeCharge = 0f;
					GameController.DisparoBase = 10;
					ChargeParticle.SetActive (false);
				}
				if (TimeCharge > 2.5f) {
					LVL = 2;
					TimeCharge = 0f;
					GameController.DisparoBase = 15;
					ChargeParticle.SetActive (false);
				}
				if (TimeShoot >= 0.2f) {
					Shoting = Instantiate (Bullet [LVL], SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
					Shoting.velocity = transform.right * BulletSpeed;
					TimeShoot = 0;
				}
			}

			if (Input.GetKeyUp (KeyCode.C) && Special == 1) {
				Rigidbody2D Shoting;
				Shoting = Instantiate (Bullet [3], SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
				Shoting.velocity = transform.right * BulletSpeed;
				Special = 0;
			}

			if (GameController.armas == 2) {
				GameController.data.sliderA [2].value -= bala;
				GameController.data.sliderPA [2].value -= bala;
				Rigidbody2D Shoting;
				Shoting = Instantiate (Tornado, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
				Shoting.velocity = transform.right * BulletSpeed;
			}

			if (GameController.armas == 3) {
				GameController.data.sliderA [3].value -= bala;
				GameController.data.sliderPA [3].value -= bala;
			}

			if (GameController.armas == 4) {
				GameController.data.sliderA [4].value -= bala;
				GameController.data.sliderPA [4].value -= bala;
				Rigidbody2D Shoting;
				Shoting = Instantiate (ElectricShoot, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
				Shoting.velocity = transform.right * BulletSpeed;
			}
		} else 
		{
			Shoot = false;
		}
	}

    void OnColliderEnter2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "EmbestidaCandileja")
        {
            Destroy(gameObject);
        }
    }
}
