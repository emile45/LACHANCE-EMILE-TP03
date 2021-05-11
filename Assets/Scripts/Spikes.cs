using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    //Déclaration de mon GameManager
    public GameManager gameManager;
    public AudioManager audioManager;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    // Si une collision avec mes piques est détectée
    private void OnTriggerEnter(Collider other)
    {
        // Si la collision provient du joueur
        if (other.CompareTag("Player") && !PlayerMovements.isGrounded && rb.velocity.y < 0f)
        {
            //Effet sonore
            audioManager.soundEffect("mortPikes");
            gameManager.tuerOneShot();
        }
    }
}
