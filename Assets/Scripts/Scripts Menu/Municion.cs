using UnityEngine;
using System.Collections;
using Globales;

public class Municion : MonoBehaviour {

	public Sprite[] colorM;
	
	void Update(){
		colorMaterial ();
	}

	void colorMaterial(){

		for (int i=0;i<colorM.Length;i++){
			if (GameController.armas == i) {
				gameObject.GetComponent<SpriteRenderer> ().enabled = colorM [i];
			//	gameObject.GetComponent<Renderer>().material = colorM[i];
			}			
		}
	}
}