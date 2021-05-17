using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollider : MonoBehaviour
{
    //Récupération des dépendances
    private GameManager gameManager;
    private GameObject bullet;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        bullet = GameObject.Find("Balle(Clone)");
    }


    private void OnTriggerEnter(Collider other)
    {
        //Si la balle entre en collision avec le joueur
        if (other.CompareTag("Player"))
        {
            gameManager.enleverVie();
            Destroy(bullet);
        }
    }
}
