using UnityEngine;
using System.Collections;
using Globales;

public class DañoNivel : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D Col)
	{
        if(Col.gameObject.tag == "Player")
        {
			if(GameController.vidas < 3){
				if (GameController.vidas < 2){
					Application.LoadLevel(Application.loadedLevel);
				}
				GameController.data.vidasA [GameController.vidas].SetActive (false);
				GameController.data.vidasPA [GameController.vidas].SetActive (false);
				GameController.data.sliderHealth.value = GameController.SaludMax;
				GameController.data.sliderHealthP.value = GameController.SaludMax;
				GameController.vidas ++;
				GameController.Mecanicas = false;
			}
        }
	}
}