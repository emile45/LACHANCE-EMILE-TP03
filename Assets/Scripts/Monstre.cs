using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstre : MonoBehaviour
{
    public Transform feetPosition;
    public Transform TransformMonstre;

    private Rigidbody playerRb;
    private Rigidbody monsterRb;
    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        monsterRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float angle = Vector3.Angle(playerRb.transform.up + playerRb.transform.position, transform.up + transform.position);

        //Debug.Log(angle);
        
    }
    public void headCollision(Collider collider){
        if(playerRb.velocity.y < 0f)
            Debug.Log("monstre tué");
    
    }
}
