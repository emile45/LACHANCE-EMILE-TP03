using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsMonstre : MonoBehaviour
{
    private Monstre monstre;
    private bool playerAttackedRecently=false;

    void Start()
    {
        //récupération et assignation de la variable du monstre
        monstre = GetComponentInParent<Monstre>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Appel la fonction qui retire de la vie du joueur si il touche le monstre
        if (other.CompareTag("Player") && !playerAttackedRecently)
        {
            playerAttackedRecently = true;
            monstre.bodyCollision(other);
            //Ceci empêche le monstre d'attaquer le joueur trop rapidement
            StartCoroutine(delaisEntreAttaques());
        }
    }

    private IEnumerator delaisEntreAttaques()
    {
        //Attends X secondes
        yield return new WaitForSeconds(2f);
        playerAttackedRecently = false;
    }
}
