using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    // Variables publiques 
    public float speedWalking;
    public float speedRunning;
    public float jumpHeight = 1f;

    public Transform feetPos;

    // Variables privées
    private float inputHorizontal;

    private Rigidbody rb;

    private Animator playerAnimator;

    private bool isGrounded;

    private float speed = 0.1f;

    private float animationSpeed = 1;

    private float lerpSpeed = 0.08f;

    private Vector3 moveDirection;

    private bool inMotion;


    void Awake()
    {
        // Récupératione et assignation des variables du personnage.
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Vérifier si l'on touche le sol
        isGrounded = Physics.CheckSphere(feetPos.position, 0.15f, 1, QueryTriggerInteraction.Ignore);

        // Vertical (W, S et Joystick avant/arrière)
        inputHorizontal = Input.GetAxis("Horizontal");

        Vector3 rotPlayer = rb.rotation.eulerAngles;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Courir
            animationSpeed = Mathf.Lerp(animationSpeed, 2f, lerpSpeed);
            speed = Mathf.Lerp(speed, speedRunning, lerpSpeed);

        }
        else
        {
            // Marche
            animationSpeed = Mathf.Lerp(animationSpeed, 1f, lerpSpeed);
            speed = Mathf.Lerp(speed, speedWalking, lerpSpeed);
        }


        if(inputHorizontal > 0f)
        {
            playerAnimator.SetTrigger("Forward");
            inMotion = true;
            rotPlayer.y = 0f;
            rb.rotation = Quaternion.Euler(rotPlayer);
        }
        if(inputHorizontal < 0f)
        {
            playerAnimator.SetTrigger("Backwards");
            inMotion = true;
            rotPlayer.y = 180f;
            rb.rotation = Quaternion.Euler(rotPlayer);
        }
        if(inputHorizontal == 0)
            inMotion = false;

        playerAnimator.SetBool("inMotion", inMotion);

        moveDirection = transform.forward * inputHorizontal;

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            playerAnimator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }
    private void FixedUpdate()
    {
        // Déplacer le personnage selon le vecteur de direction
        if(rb.rotation.y == 0)
            rb.MovePosition(rb.position + moveDirection.normalized * speed * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position - moveDirection.normalized * speed * Time.fixedDeltaTime);
    }   
}
