using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallShooter : MonoBehaviour
{
    public Transform barrel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void Shoot()
    {
        // Créer un rayon qui pointe vers l'avant du pistolet
        Ray pistolRay = new Ray(barrel.position, barrel.forward);
        RaycastHit hit;


    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(barrel.position, barrel.up * 50f);
    }

}
