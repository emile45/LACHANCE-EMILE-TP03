using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsMonstre : MonoBehaviour
{
    private Monstre monstre;

    void Start()
    {
        //récupération et assignation de la variable du monstre
        monstre = GetComponentInParent<Monstre>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Appel la fonction qui retire de la vie du joueur si il touche le monstre
        if (other.CompareTag("Player"))
            monstre.bodyCollision(other);

    }
}
