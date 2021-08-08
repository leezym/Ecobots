using UnityEngine;
using System.Collections;
using Globales;

public class MoverPuertaEnemy : MonoBehaviour {

    Vector3 Move;
    public GameObject player;
    public GameObject puerta;
    
    bool mover = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void activarDialogo()
    {
        GameController.isActive = true;
       // GameController.isActiveBossDead = true;
        GameController.isActiveBoss = true;
    }

    void transladarPuerta()
    {
        puerta.transform.Translate(0, -10, 0);
        mover = false;
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.gameObject.tag == "Player" && mover)
        {
            puerta.transform.Translate(0, 10, 0);
            player.transform.Translate(5, 0, 0);
            GameController.ActiveInputs = false;
            //Boss.SetActive(true);
            Invoke("transladarPuerta", 0.5f);

            GameController.ActivarDialogo = true;
            
			if (GameController.isActiveBoss)
			{
				GameController.data.iconoJefe.SetActive (true);
				GameController.ActivarDialogo = false;
				GameController.ActiveInputs = true;
				GameController.Mecanicas = true;
			}else if (GameController.ActivarDialogo)
            {
                Invoke("activarDialogo", 1f);
            }           
         }
    }
}
