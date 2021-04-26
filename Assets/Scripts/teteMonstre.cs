using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teteMonstre : MonoBehaviour
{
    private Monstre monstre;
    // Start is called before the first frame update
    void Start()
    {
        monstre = GetComponentInParent<Monstre>();
    }

    //appel la fonction qui tue le monstre si le joueur saute dessu
    private void OnTriggerEnter(Collider other)
    {
        if (monstre)
            monstre.headCollision(other);
        
    }
}
