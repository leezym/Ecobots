using UnityEngine;
using System.Collections;

public class Mecanismo : MonoBehaviour {

	public GameObject Bloque;
	public GameObject Puas;

	bool Active1 = false, Active2 = false;
		
	void OnCollisionEnter2D(Collision2D Other)
	{
		if(Other.gameObject.tag == "Bullet")
		{
			if(this.gameObject.tag == "Bloque1")
			{
				Bloque.SetActive (false);	
				Destroy (Other.gameObject);
				Active1 = true;
			}

			if(this.gameObject.tag == "Bloque2")
			{
				Puas.SetActive (false);	
				Destroy (Other.gameObject);
				Active2 = true;
			}
		}
	}
}
