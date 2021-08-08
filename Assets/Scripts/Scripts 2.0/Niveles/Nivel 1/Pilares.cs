using UnityEngine;
using System.Collections;

public class Pilares : MonoBehaviour {

	bool Descenso;
    Vector3 move;

	void Update () 
	{
		if(Descenso)
		{
			Invoke("Derrumbar",.5f);
		}
	}

	void Derrumbar()
	{
        move.y = -5 * Time.deltaTime;

        transform.Translate(move);
	}

	void OnCollisionEnter2D(Collision2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Descenso = true;
		}
	}
}
