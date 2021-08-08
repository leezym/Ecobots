using UnityEngine;
using System.Collections;
using Globales;
using UnityEngine.UI;

public class LevelEnabled : MonoBehaviour {

	public GameObject gameController;
	private AsyncOperation async;


	void Start(){
		GameController.data.imagenCarga.enabled = false;
		gameController.SetActive (true);
		GameController.data.HUD_canvas.enabled = false;
		GameController.data.gameLife.enabled = false;
		GameController.data.gameArma.enabled = false;
		GameController.data.gameLifeP.enabled = false;
		GameController.data.gameArmaP.enabled = false;
		GameController.data.gameMusicaP.enabled = false;
		GameController.data.hud_p.enabled = false;
		GameController.data.Pause_canvas.enabled = false;
		GameController.data.gameOver_canvas.enabled = false;
	}

	void Update(){
		if (GameController.activarCarga) 
		{
			StartCoroutine (CargarImagen ());
		//	GameController.activarCarga = false;
		}
	}

	//Asigna un valor a cada nivel para luego ser cargado
	public void savedLevel_1(){
		GameController.lvl = 1;
		CargarLvl();
		Application.LoadLevel("LVL1");
	}

	public void savedLevel_2(){
		GameController.lvl = 2;
        CargarLvl();
		Application.LoadLevel("LVL2");
	}

	public void savedLevel_3(){
		GameController.lvl = 3;
        CargarLvl();
		Application.LoadLevel("LVL3");
	}

	public void savedLevel_4(){
		GameController.lvl = 4;
        CargarLvl();
		Application.LoadLevel("LVL4");
	}

	public void savedLevel_5(){
		GameController.lvl = 5;
        CargarLvl();
		Application.LoadLevel("LVL5");
	}

	public void BackMenu(){
		GameController.data.Enemy_canvas.enabled = false;
		GameController.data.Main_canvas.enabled = true;
	}

	public void Guardar(){
		if (!GameController.saved)
			GameController.saved = true;
	}

    void CargarLvl()
    {
 //       GameController.data.HUD_canvas.enabled = true;
 //       GameController.data.gameLife.enabled = true;
 //       GameController.data.gameArma.enabled = true;
		GameController.data.iconoJefe.SetActive(false);
        GameController.data.Enemy_canvas.enabled = false;
		GameController.ActiveInputs = false;
		GameController.ActiveInstantiate = false;

		GameController.isActive = false;
		GameController.zonaJefe = true;
        GameController.Mecanicas = false;
		GameController.ActivarDialogo = false;
		GameController.isActiveBoss = false;
		GameController.isActiveBossDead = false;

		GameController.data.imagenCarga.enabled = true;
		GameController.activarCarga = true;
		GameController.cancelarActivarCarga = false;
		GameController.data.barraCarga.value = 0;
    }


	IEnumerator CargarImagen()
	{

		GameController.data.barraCarga.value += GameController.valueC * Time.deltaTime;

		if (GameController.data.barraCarga.value == GameController.data.barraCarga.maxValue)
		{
			
			if (GameController.lvl == 1)
				async = Application.LoadLevelAsync ("LVL1");

			if (GameController.lvl == 2)
				async = Application.LoadLevelAsync ("LVL2");

			if (GameController.lvl == 3)
				async = Application.LoadLevelAsync ("LVL3");

			if (GameController.lvl == 4)
				async = Application.LoadLevelAsync ("LVL4");

			if (GameController.lvl == 5)
				async = Application.LoadLevelAsync ("LVL5");

			GameController.activarCarga = false;
			GameController.cancelarActivarCarga = true;
			yield return async;
		}
	}
}
