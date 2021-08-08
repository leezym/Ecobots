using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Globales;

public class bossDestroy3 : MonoBehaviour {
	
	public Canvas [] canvasGroup;
	Army_Menu reset;
	
	void Start(){
		reset = GameObject.Find ("ScriptCanvas").GetComponent<Army_Menu> ();
		if (GameController.muerto3) {
			Destroy(gameObject);
		}
		canvasGroup = FindObjectsOfType (typeof(Canvas)) as Canvas[];
	}
	
	void OnCollisionEnter2D (Collision2D other){
		if (other.collider.tag == "Bullet") {
			Destroy(other.gameObject);
			Destroy(gameObject);
			GameController.muerto3 = true;
			GameController.lvl = 0;
			Application.LoadLevel("Menu");
			
			for(int i=0;i<canvasGroup.Length;i++){
				if (canvasGroup[i].name == "Armas" || canvasGroup[i].name == "Life" || canvasGroup[i].name == "HUD"){
					canvasGroup[i].enabled = false;				
				}
				if (canvasGroup[i].name == "EnemyMenu"){
					canvasGroup[i].enabled = true;
				}
			}
			
			reset.Resetear();
		}
	}
}