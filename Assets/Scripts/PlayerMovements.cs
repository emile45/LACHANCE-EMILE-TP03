using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    // Variables publiques 
    public float speedRunning;

    public float multipChute=2.5f;
    public float multipJumpRapide=2f;
    public float jumpHeight =2f;
    public Transform feetPos;



    // Variables privées
    private float inputHorizontal;

    private Rigidbody rb;

    private Animator playerAnimator;

    private float speed = 0.1f;

    private float animationSpeed = 1;

    private float lerpSpeed = 0.08f;

    private Vector3 moveDirection;

    private bool inMotion;

    private Vector3 rotPlayer;

    private bool isGrounded;

    private ConstantForce constantForce_player;

    private bool slam;

    void Awake()
    {
        // Récupératione et assignation des variables du personnage.
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        constantForce_player = GetComponent<ConstantForce>();
    }

    void Update()
    {
        // Vérifier si l'on touche le sol
        isGrounded = Physics.CheckSphere(feetPos.position, 0.15f, 1, QueryTriggerInteraction.Ignore);

        if (!isGrounded && !slam)
        {
            // Vertical (W, S et Joystick avant/arrière)
            inputHorizontal = Input.GetAxis("Horizontal") / 2;
        }
        if(isGrounded && !slam)
        {
            inputHorizontal = Input.GetAxis("Horizontal");
        }

        rotPlayer = rb.rotation.eulerAngles;

        animationSpeed = Mathf.Lerp(animationSpeed, 2f, lerpSpeed);
        speed = Mathf.Lerp(speed, speedRunning, lerpSpeed);

        inMotion = Mathf.Abs(inputHorizontal) > 0f;
        playerAnimator.SetBool("inMotion", inMotion);

        if (inputHorizontal > 0f)
        {
            playerAnimator.SetFloat("Horizontal", inputHorizontal * animationSpeed);
            rotPlayer.y = 0f;
            rb.rotation = Quaternion.Euler(rotPlayer);
        }
        if (inputHorizontal < 0f)
        {
            playerAnimator.SetFloat("Horizontal", inputHorizontal * animationSpeed);
            rotPlayer.y = 180f;
            rb.rotation = Quaternion.Euler(rotPlayer);
        }




        moveDirection = transform.forward * inputHorizontal;


        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isGrounded = false;
            playerAnimator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        //Slam
        if (Input.GetKeyDown(KeyCode.S) && isGrounded == false)
        {
            playerAnimator.SetBool("Crouch", true);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * 50f, ForceMode.VelocityChange);
            slam = true;


        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.SetBool("Crouch", false);
            constantForce_player.force = Vector3.zero;
            slam = false;
        }
            
            
    }
    private void FixedUpdate()
    {
        if (slam)
            return;
        // Déplacer le personnage selon le vecteur de direction
        if(rotPlayer.y == 0f)
            rb.MovePosition(rb.position + moveDirection.normalized * speed * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position - moveDirection.normalized * speed * Time.fixedDeltaTime);
    }

}
