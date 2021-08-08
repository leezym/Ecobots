using UnityEngine;
using System.Collections;

public class Estalactita : MonoBehaviour {

    public GameObject PosInicial;
    public GameObject PosFinal;

	bool Detected,Atack = false;
	
	void Update () 
    {
        DetectedPlayer();

        if(Detected)
        {
			Atack = true;
        }

		if(Atack)
		{
			transform.Translate(new Vector3(0, -30, 0) * Time.deltaTime);
		}
	}

    //Detectar 
    void DetectedPlayer()
    {
        Debug.DrawLine(PosInicial.transform.position, PosFinal.transform.position, Color.blue);
        Detected = Physics2D.Linecast(PosInicial.transform.position, PosFinal.transform.position, 1 << LayerMask.NameToLayer("Player"));
    }

    void OnColliderEnter2D(Collision2D Other)
    {
        if(Other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
