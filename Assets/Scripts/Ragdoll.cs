using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] ragdollRbs;
    Animator animaterPLayer;

    Rigidbody rb;

    bool isDead;

    public bool debugKill;

    // Start is called before the first frame update
    void Awake()
    {
        // Lister tous les Rbs
        ragdollRbs = GetComponentsInChildren<Rigidbody>();

        animaterPLayer = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

        // Désactiver le Ragdoll
        toggleRagdoll(false);

        animaterPLayer.enabled = true;
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (debugKill)
        {
            Die();
            debugKill = false;
        }

    }

    public void Die()
    {
        //activer le ragdoll s'il ne l'est pas déjà
        if (isDead)
            return;

        isDead = true;
        toggleRagdoll(true);
    }

    void toggleRagdoll(bool value)
    {
        // Mettre le Kinematic à !value
        foreach (Rigidbody rb in ragdollRbs)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rb.isKinematic = !value;
        }

        // Animator
        animaterPLayer.enabled = !value;
    }
}
