using UnityEngine;
using System.Collections;

public class MecanismoNivel4 : MonoBehaviour {

    public GameObject Bloque;
    int Mecanismo;
    bool Active = true;
    
    void Update() 
    {
        if(Mecanismo == 2)
        {
            Bloque.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D Other)
    {
        if(Other.gameObject.tag == "Bullet" && Active)
        {
            Active = false;
            Mecanismo++;
        }
    }
}
