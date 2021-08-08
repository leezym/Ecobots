using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Globales;

public class bossDestroy1 : MonoBehaviour {
	
	Army_Menu arm;

	void Start(){
		arm = GameObject.Find ("ScriptCanvas").GetComponent<Army_Menu> ();

		if (GameController.muerto1) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D other){
		if (other.collider.tag == "Bullet") {
			GameController.muerto1 = true;
			GameController.lvl = 0;
			Application.LoadLevel("Menu");
			GameController.data.gameArma.enabled = false;
            GameController.data.gameLife.enabled= false;
            GameController.data.HUD_canvas.enabled = false;
            GameController.data.Enemy_canvas.enabled = true;

            arm.Resetear();
			arm.PlaySound();	
		}
	}
}