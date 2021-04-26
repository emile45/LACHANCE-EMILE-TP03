using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    //Déclaration de mon GameManager
    public GameManager gameManager;
    public AudioManager audioManager;

    // Si une collision avec mes piques est détectée
    private void OnTriggerEnter(Collider other)
    {
        // Si la collision provient du joueur
        if (other.CompareTag("Player"))
        {
            //Effet sonore
            audioManager.mortPikes();
            // boucle qui vas tuer le joueur peut importe la vie qui lui reste
            int x = GameManager.nbrVies;
            for(int i=0; i <= x; i++)
            {
                gameManager.enleverVie();
            }

        }
    }
}
