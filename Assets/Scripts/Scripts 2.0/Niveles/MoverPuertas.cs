using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Globales;

public class MoverPuertas : MonoBehaviour {

	Vector3 Move;
	GameObject player;
	public GameObject puerta;
	bool mover = true;

	void Start (){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

	void transladarPuerta (){
		puerta.transform.Translate(0,-10,0);
		mover = false;
	}

	void OnTriggerEnter2D(Collider2D Col)
	{
		if(Col.gameObject.tag == "Player" && mover)
		{
			puerta.transform.Translate(0,10,0);
			player.transform.Translate(5,0,0) ;
			Invoke ("transladarPuerta", 0.5f);

		}
	}
}
