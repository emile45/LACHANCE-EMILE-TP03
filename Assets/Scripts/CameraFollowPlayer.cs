using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Ce script s'assigne sur la caméra et sert à suivre un objet ayant la tag "Player"

    //Variables de distance entre Camera et Player
    public float DistanceX;
    public float DistanceY;

    //Transforms du joueur et de la Camera
    private Transform player;
    private Transform camPos;

    void Awake()
    {
        //Recherche des objets dans la scène pour assigner à mes variables
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camPos = GetComponent<Transform>();
    }

    void Update()
    {
        //Création du nouveau vecteur de position de camera selon mes variables publiques
        camPos.position = new Vector3(player.position.x + DistanceX, player.position.y + DistanceY, player.position.z);
    }
}
