using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Globales;

public class Main_Menu : MonoBehaviour {

	public GameObject continuar, musica, Option_canvas, Ayuda_canvas;

	void Start (){
		Option_canvas.SetActive (false);
		Ayuda_canvas.SetActive (false);
		GameController.data.Main_canvas.enabled = true;
		continuar.SetActive (false);
		musica.SetActive (false);
		GameController.data.Enemy_canvas.enabled = false;
	}

	void Update(){
		if (GameController.saved)
			continuar.SetActive (true);
	}

	public void PlayGame() {
		GameController.data.Main_canvas.enabled = false;
		GameController.saved = false;
		GameController.data.Enemy_canvas.enabled = true;

		//Jefes
		GameController.muerto1 = false;
		GameController.muerto2 = false;
		GameController.muerto3 = false;
		GameController.muerto4 = false;
		GameController.muerto5 = false;
	}
	
	public void ExitGame () {
		Application.Quit ();
	}

	public void OptionsGame(){
		Option_canvas.SetActive (true);
		GameController.data.Main_canvas.enabled = false;
		musica.SetActive (true);	
	}

	public void AyudaGame(){
		Ayuda_canvas.SetActive (true);
		GameController.data.Main_canvas.enabled = false;
	}

	public void ContinueGame(){
		GameController.data.Main_canvas.enabled = false;
		GameController.data.Enemy_canvas.enabled = true;
	}

	public void BackGame(){
		Ayuda_canvas.SetActive (false);
		Option_canvas.SetActive (false);
		musica.SetActive (false);
		GameController.data.Main_canvas.enabled = true;
	}
}