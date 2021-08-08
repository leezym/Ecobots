using UnityEngine;
using System.Collections;

public class ActivarPuas : MonoBehaviour {

    public GameObject Puas;

    bool Habilitar = true;

    void Start() 
    {
        StartCoroutine(Active());
    }

    IEnumerator Active() 
    {
        while(Habilitar)
        {
            Puas.SetActive(true);

            yield return new WaitForSeconds(2);

            Puas.SetActive(false);

            yield return new WaitForSeconds(2);
        }
    }
}
