using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Globales;

public class PlayerStatus : MonoBehaviour {

	BoxCollider2D Control;
	Shooting Shoot;
	int amountS = 10, amountL = 25; // Valores Armas
	Instantiate Ins;
	bool Golpe;
	public GameObject Damege;

	//ParticleSystem Particulas;

	void Start()
	{
		Control = GetComponent<BoxCollider2D> ();
		Shoot = GetComponent<Shooting>();
		Ins = GameObject.Find ("Instanciate").GetComponent<Instantiate> ();
		GameController.data.sliderHealth.value = GameController.SaludMax;
		GameController.data.sliderHealthP.value = GameController.SaludMax;
	}

	void Update(){

		if(Control.isTrigger == true)
		{
			Invoke("ChangeCollider",3);
		}

		if(Golpe)
		{
			Damege.SetActive (true);	
			Invoke ("ChangeDamage", 0.5f);
		}

		if(!Golpe)
		{
			Damege.SetActive (false);
		}
	}

	public void MuertePlayer(){
		if (GameController.data.sliderHealth.value == GameController.data.sliderHealth.minValue) {
            Application.LoadLevel(Application.loadedLevel);
			GameController.data.vidasA [GameController.vidas].SetActive (false);
			GameController.data.vidasPA [GameController.vidas].SetActive (false);
			GameController.data.sliderHealth.value = GameController.SaludMax;
			GameController.data.sliderHealthP.value = GameController.SaludMax;
			GameController.vidas ++;
            GameController.Mecanicas = false;
            GameController.ActivarDialogo = false;
		}
	}

	void OnCollisionEnter2D(Collision2D Tar)
	{
		if(Tar.gameObject.layer == LayerMask.NameToLayer("Enemys"))
		{
            Control.isTrigger = true;
			GameController.data.sliderHealth.value -=GameController.DañoCollision;
			GameController.data.sliderHealthP.value -= GameController.DañoCollision;
			MuertePlayer();
		}

		// Colisiones de Enemigos	
		if(Tar.gameObject.tag == "BulletEnemy")
		{
			GameController.data.sliderHealth.value -= GameController.BulletEnemys;
			GameController.data.sliderHealthP.value -= GameController.BulletEnemys;
			Destroy(Tar.gameObject);
			MuertePlayer();
			Golpe = true;
		}

		//Colisiones de LavaBots
		if(Tar.gameObject.tag == "Kamikaze")
		{
			GameController.data.sliderHealth.value -= GameController.CollisionKamikaze;
			GameController.data.sliderHealthP.value -= GameController.CollisionKamikaze;
			MuertePlayer();
		}

		//Colisiones Amadillo
		if(Tar.gameObject.tag == "Armadillo")
		{
			GameController.data.sliderHealth.value -= GameController.CollisionArmadillo;
			GameController.data.sliderHealthP.value -= GameController.CollisionArmadillo;
			MuertePlayer();
		}

		//Colision Espectro Energia
		if(Tar.gameObject.tag == "Espectro")
		{
			Shoot.Disparo = false;
		}
		
		//Collisiones Candileja
        if(Tar.gameObject.tag == "Meteoros")
        {
			GameController.data.sliderHealth.value -= GameController.Fuego;
			GameController.data.sliderHealthP.value -= GameController.Fuego;
            Destroy(Tar.gameObject);
			MuertePlayer();
        }

		if(Tar.gameObject.tag == "EmbestidaCandileja")
		{
			GameController.data.sliderHealth.value -= GameController.EmbesCandi;
			GameController.data.sliderHealthP.value -= GameController.EmbesCandi;
			MuertePlayer();
		}

		//Collisiones Madre Agua
		if(Tar.gameObject.tag == "Burbujas")
		{
			GameController.data.sliderHealth.value -= GameController.Burbujas;
			GameController.data.sliderHealthP.value -= GameController.Burbujas;
			Destroy(Tar.gameObject);
			MuertePlayer();
		}

		if(Tar.gameObject.tag == "Burbujota")
		{
			GameController.data.sliderHealth.value -= GameController.Burbujota;
			GameController.data.sliderHealthP.value -= GameController.Burbujota;
			Destroy(Tar.gameObject);
			MuertePlayer();
		}

	// Colision Vidas, Recargas, Enemigos y Limitadores
		// Salud Aumentar (Vidas)
		if (Tar.gameObject.tag =="healthL"){
			GameController.data.sliderHealth.value += amountL;
			GameController.data.sliderHealthP.value += amountL;
			Destroy (Tar.gameObject);
			if (GameController.data.sliderHealth.value == GameController.SaludMax) {
				Destroy (Tar.gameObject);
			}
		}else
			if (Tar.gameObject.tag == "healthS"){
				GameController.data.sliderHealth.value += amountS;
				GameController.data.sliderHealthP.value += amountS;
				Destroy (Tar.gameObject);
				if (GameController.data.sliderHealth.value == GameController.SaludMax) {
					Destroy (Tar.gameObject);
				}
			}
	
		// Recargar	
		for (int i = 0;i < 6;i++){
			if(GameController.armas == i){
				if (Tar.collider.gameObject.tag == "municionL"){
					GameController.data.sliderA [i].value += amountL;
					GameController.data.sliderPA [i].value += amountL;
					Destroy (Tar.gameObject);
				}else
					if (Tar.collider.gameObject.tag == "municionS"){ 
						GameController.data.sliderA [i].value += amountS;
						GameController.data.sliderPA [i].value += amountS;
						Destroy (Tar.gameObject);
					}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D Tar)
	{	
		// Colision checkpoints
		for (GameController.cp = 0; GameController.cp < GameController.data.checkpoint.Length; GameController.cp++) {
			if (Tar.gameObject == GameController.data.checkpoint [GameController.cp]) {
				GameController.posX = GameController.data.checkpoint[GameController.cp].transform.position.x;
				GameController.posY = GameController.data.checkpoint[GameController.cp].transform.position.y;
				GameController.posZ = GameController.data.checkpoint[GameController.cp].transform.position.z;
				GameController.actCP = true;
				GameController.data.checkpoint [GameController.cp].GetComponent<BoxCollider2D> ().enabled = false;
			}
		}
	}

	void ChangeCollider()
	{
		Control.isTrigger = false;
	}

	void ChangeDamage()
	{
		Golpe = false;
	}
}
