using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Globales;

public class Army_Menu : MonoBehaviour {

	//public GameObject Ice,Fire;
	[SerializeField]
	public MusicEnabled musicE;
	[SerializeField]
	public GameObject player;

	void Awake(){
		musicE = GameObject.Find ("MusicEnabled").GetComponent<MusicEnabled> ();
	}

	void Start () {

		//Sonido
		PlaySound();

		//Agregar valor a las municiones y sangre
		GameController.data.sliderA [0].maxValue = GameController.MuniMax;
		GameController.data.sliderPA [0].maxValue = GameController.MuniMax;

        //Desactivar items
		for (int i = 1; i < GameController.data.itemsA.Length; i++)
        {
            GameController.data.itemsA[i].SetActive(false);
        }
	}
	
	void Update () {
		PauseGame ();
		habilitarArma ();

		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		// Cuando nos quedamos sin vidas
		if (GameController.vidas == 3) {
			StartCoroutine (ResetGame ());
			GameController.vidas = 0;
		}

	}

	IEnumerator ResetGame()
	{
		GameController.data.gameOver_canvas.enabled = true;
		GameController.data.gameLife.enabled = false;
		GameController.data.HUD_canvas.enabled = false;
		GameController.data.gameArma.enabled = false;
		GameController.ActiveInputs = false;
		GameController.Mecanicas = false;
		Resetear ();

		yield return new WaitForSeconds (1.5f);

		GameController.data.gameOver_canvas.enabled = false;
		GameController.data.gameLife.enabled = true;
		GameController.data.HUD_canvas.enabled = true;
		GameController.data.gameArma.enabled = true;
		GameController.ActiveInputs = true;
		Application.LoadLevel (Application.loadedLevel);
	}

//	void LateUpdate(){
//		//Buscar los poderes Joe en el lvl
//		if(GameController.lvl == 1 || GameController.lvl == 2 || GameController.lvl == 3 || GameController.lvl == 4 || GameController.lvl == 5)
//		{
//			if(Fire == null)
//			{
//				Fire = GameObject.Find ("Lanzallamas");
//			}
//		}
//	}

	//Sonido Principal
	public void PlaySound (){
		GameController.data.soundLvl= Resources.Load("ecobots theme") as AudioClip;
		GameController.data.audioSourceTheme.clip = GameController.data.soundLvl;
		GameController.data.audioSourceTheme.volume = GameController.data.volumenTheme;
		musicE.enabledAudioPrincipal ();
	}

	//Activar menu pausa
	void PauseGame(){
		if (!GameController.isActive) {
			if (GameController.lvl == 1 || GameController.lvl == 2 || GameController.lvl == 3 || GameController.lvl == 4 || GameController.lvl == 5) { // Solo aparezca en los nivelesd de juego
				if (Input.GetKeyDown (KeyCode.P)) {
					GameController.data.Pause_canvas.enabled = true;
					GameController.data.gameLife.enabled = false;
					GameController.data.HUD_canvas.enabled = false;
					GameController.data.gameArma.enabled = false;
					GameController.data.gameMusicaP.enabled = true;
					GameController.data.hud_p.enabled = true;
					GameController.data.gameArmaP.enabled = true;
					GameController.data.gameLifeP.enabled = true;
					Time.timeScale = 0;

					GameController.data.itemsPA [0].SetActive (true);

					for (int i = 1; i < GameController.data.itemsPA.Length; i++) {
						GameController.data.itemsPA [i].SetActive (false);
					}

					// Mostras las armas obtenidas
					if (GameController.muerto1 == true)
						GameController.data.itemsPA [5].SetActive (true);
					
					if (GameController.muerto2 == true)
						GameController.data.itemsPA [2].SetActive (true);
					
					if (GameController.muerto3 == true)
						GameController.data.itemsPA [3].SetActive (true);
					
					if (GameController.muerto4 == true)
						GameController.data.itemsPA [4].SetActive (true);
					
					if (GameController.muerto5 == true)
						GameController.data.itemsPA [5].SetActive (true);

					// Check Musica
					if (GameController.data.checkMusica.isOn) {
						GameController.data.checkMusicaP.isOn = true;
					}

					if (!GameController.data.checkMusica.isOn) {
						GameController.data.checkMusicaP.isOn = false;	
					}

					// Check Dialogos
					if (GameController.data.checkDialogo.isOn) {
						GameController.data.checkDialogoP.isOn = true;
					}

					if (!GameController.data.checkDialogo.isOn) {
						GameController.data.checkDialogoP.isOn = false;	
					}
				}
			}
		}
	}

	// Ir al Menu pricipal
	public void BackGame() {
		Time.timeScale = 1;		
		GameController.data.Main_canvas.enabled = true;
        CargarButtonPausa();
		GameController.lvl = 0;
	
	//RESETEAR	
		Resetear ();

	//SONIDO PPAL
		PlaySound();
		Application.LoadLevel ("Menu");
	}

	// Ir al menu de niveles
	public void LevelGame() {
		Time.timeScale = 1;		
		GameController.data.Enemy_canvas.enabled = true;
        CargarButtonPausa();
		GameController.lvl = 0;
	
	//RESETEAR
		Resetear ();
	
	//SONIDO PPAL
		PlaySound();
		Application.LoadLevel ("Menu");
	}

	// Quitar pausa
	public void ResumeGame(){
		GameController.data.Pause_canvas.enabled = false;
		GameController.data.gameLife.enabled = true;
		GameController.data.HUD_canvas.enabled = true;
		GameController.data.gameArma.enabled = true;
		GameController.data.gameMusicaP.enabled = false;
		GameController.data.hud_p.enabled = false;
		GameController.data.gameArmaP.enabled = false;
		GameController.data.gameLifeP.enabled = false;
		Time.timeScale = 1;
	}

	public void Resetear(){        
		// Jefe
		GameController.data.sliderHealthBoss.value = GameController.data.sliderHealthBoss.maxValue;
		GameController.data.iconoJefe.SetActive (false);

		//Armas
		GameController.armas = 0;
		for (int i = 1; i < GameController.data.sliderA.Length; i ++)
		{ 
			GameController.data.sliderA [i].value = GameController.MuniMax;
			GameController.data.sliderPA [i].value = GameController.MuniMax;
		}

        GameController.data.itemsA[0].SetActive(true);

		for (int i = 1; i < GameController.data.itemsA.Length; i++)
        {
            GameController.data.itemsA[i].SetActive(false);
        }

		//Vidas
		GameController.vidas = 0;
		for (int i = 0; i < GameController.data.vidasA.Length; i ++)
		{ 
			GameController.data.vidasA [i].SetActive (true);
			GameController.data.vidasPA [i].SetActive (true);
		}
		GameController.data.sliderHealth.value = GameController.SaludMax;
		GameController.data.sliderHealthP.value = GameController.SaludMax;
		
		//Checkpoint
		GameController.cp = 0;
		GameController.actCP = false;
	}

    void CargarButtonPausa()
    {
        GameController.data.Pause_canvas.enabled = false;
        GameController.data.gameMusicaP.enabled = false;
        GameController.data.hud_p.enabled = false;
        GameController.data.gameArmaP.enabled = false;
        GameController.data.gameLifeP.enabled = false;
        GameController.data.gameOver_canvas.enabled = false;

    }

	// Cambiar y Habilitar armas
	void habilitarArma(){
		//Desplazarme hacia la derecha
		if (GameController.armas < 6) {
			if (Input.GetKeyDown (KeyCode.S)) {
				GameController.armas++;
				if (GameController.armas == 1){
					if (GameController.muerto1 == true) {						
						for (int e=0; e < 6;e++){
							GameController.data.itemsA[e].SetActive(false);
							if (e == 1){
								GameController.data.itemsA[e].SetActive(true);
							}
						}
					}else GameController.armas++;
				}

				if(GameController.armas == 2){ 
					if (GameController.muerto2 == true) {						
						for (int e=0; e < 6;e++){
							GameController.data.itemsA[e].SetActive(false);
							if (e == 2){
								GameController.data.itemsA[e].SetActive(true);
							}
						}
					}else GameController.armas++; 
				}

				if (GameController.armas == 3){
					if (GameController.muerto3 == true) {						
						for (int e=0; e < 6;e++){
							GameController.data.itemsA[e].SetActive(false);
							if (e == 3){
								GameController.data.itemsA[e].SetActive(true);
							}
						}
					}else GameController.armas++;
				}

				if (GameController.armas == 4){
					if (GameController.muerto4 == true) {					
						for (int e=0; e < 6;e++){
							GameController.data.itemsA[e].SetActive(false);
							if (e == 4){
								GameController.data.itemsA[e].SetActive(true);
							}
						}
					}else GameController.armas++;
				}

				if (GameController.armas == 5){
					if (GameController.muerto5 == true) {						
						for (int e=0; e < 6;e++){
							GameController.data.itemsA[e].SetActive(false);
							if (e == 5){
								GameController.data.itemsA[e].SetActive(true);
							}
						}
					}else GameController.armas++;
				}

				if (GameController.armas > 5){	
					GameController.armas = 0;
					for (int e=0; e < 6;e++){
						GameController.data.itemsA[e].SetActive(false);
						if (e == 0){
							GameController.data.itemsA[e].SetActive(true);					
						}
					}
				}
			}
		}
		//Desplazarme hacia la izquierda
		if (GameController.armas > -1) {
			if (Input.GetKeyDown (KeyCode.A)) {
				GameController.armas--;

				if (GameController.armas < 0){	
					GameController.armas = 5;
					if (GameController.muerto5 == true) {						
						for (int e=0; e < 6;e++){
							GameController.data.itemsA[e].SetActive(false);
							if (e == 5){
								GameController.data.itemsA[e].SetActive(true);
							}
						}
					}else GameController.armas--;
				}
			
				if (GameController.armas == 4){
					if (GameController.muerto4 == true) {					
						for (int e=0; e < 6;e++){
							GameController.data.itemsA[e].SetActive(false);
							if (e == 4){
								GameController.data.itemsA[e].SetActive(true);
							}
						}
					}else GameController.armas--;
				}

				if (GameController.armas == 3){
					if (GameController.muerto3 == true) {						
						for (int e=0; e < 6;e++){
							GameController.data.itemsA[e].SetActive(false);
							if (e == 3){
								GameController.data.itemsA[e].SetActive(true);
							}
						}
					}else GameController.armas--;
				}

				if(GameController.armas == 2){ 
					if (GameController.muerto2 == true) {						
						for (int e=0; e < 6;e++){
							GameController.data.itemsA[e].SetActive(false);
							if (e == 2){
								GameController.data.itemsA[e].SetActive(true);
							}
						}
					}else GameController.armas--; 
				}

				if (GameController.armas == 1){
					if (GameController.muerto1 == true) {						
						for (int e=0; e < 6;e++){
							GameController.data.itemsA[e].SetActive(false);
							if (e == 1){
								GameController.data.itemsA[e].SetActive(true);
							}
						}
					}else GameController.armas--;
				}

				if (GameController.armas == 0){										
					for (int e=0; e < 6;e++){
						GameController.data.itemsA[e].SetActive(false);
						if (e == 0){
							GameController.data.itemsA[e].SetActive(true);
						}
					}
				}
			}
		}
	}
}