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



    // Variables priv?es
    private float inputHorizontal;

    private Rigidbody rb;

    private Animator playerAnimator;

    private float speed = 0.1f;

    private float animationSpeed = 1;

    private float lerpSpeed = 0.08f;

    private Vector3 moveDirection;

    public static bool inMotion;

    private Vector3 rotPlayer;

    public static bool isGrounded;

    private ConstantForce constantForce_player;

    private bool slam;

    private bool slamClipPlayed=false;

    public static bool justRespawned=false;

    [Header("Managers")]
    public AudioManager audioManager;
    public GameManager gameManager;


    void Awake()
    {
        // R?cup?ratione et assignation des variables du personnage.
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        constantForce_player = GetComponent<ConstantForce>();

    }

    void Update()
    {
        // V?rifier si l'on touche le sol
        isGrounded = Physics.CheckSphere(feetPos.position, 0.15f, 1, QueryTriggerInteraction.Ignore);

        //Si le joueur ne touche pas le sol, ses mouvements sont r?duits de 50%
        if (!isGrounded && !slam)
        {
            // Vertical (W, S et Joystick avant/arri?re)
            inputHorizontal = Input.GetAxis("Horizontal") / 2;
        }
        // Si le joueur touche le sol, vitesse normale.
        if(isGrounded && !slam)
        {
            inputHorizontal = Input.GetAxis("Horizontal");
        }

        //R?cup?ration de la rotation du joueur
        rotPlayer = rb.rotation.eulerAngles;

        //D?termine la vitesse du joueur et d'animation.
        animationSpeed = Mathf.Lerp(animationSpeed, 2f, lerpSpeed);
        speed = Mathf.Lerp(speed, speedRunning, lerpSpeed);
        
        //D?termine si le joueur bouge.
        inMotion = Mathf.Abs(inputHorizontal) > 0f;
        playerAnimator.SetBool("inMotion", inMotion);

        //Si le joueur vas vers la droite
        if (inputHorizontal > 0f)
        {
            //Changement dans le blendtree
            playerAnimator.SetFloat("Horizontal", inputHorizontal * animationSpeed);

            //changement de la rotation pour que le joueur regarde vers la droite
            rotPlayer.y = 0f;
            rb.rotation = Quaternion.Euler(rotPlayer);
        }
        //Si le joueur vas vers la gauche
        if (inputHorizontal < 0f)
        {
            //Changement dans le blendtree
            playerAnimator.SetFloat("Horizontal", inputHorizontal * animationSpeed);

            //changement de la rotation pour que le joueur regarde vers la droite
            rotPlayer.y = 180f;
            rb.rotation = Quaternion.Euler(rotPlayer);
        }



        //D?termine la direction de d?placement du personnage.
        moveDirection = transform.forward * inputHorizontal;

        //Sauter
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            // Bruits de saut
            audioManager.soundEffect("sautJoueur");
            new WaitForSeconds(audioManager.jump.length);
            //ne touche pas le sol
            isGrounded = false;
            //D?clenche l'animation
            playerAnimator.SetTrigger("Jump");
            //modifie la v?locit?e
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        //Slam seulement si le joueur est dans le airs
        if (Input.GetKeyDown(KeyCode.S) && isGrounded == false)
        {
            //d?clenche l'animation
            playerAnimator.SetBool("Crouch", true);
            //modifie la v?locit?e et ajoute la force vers le bas.
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * 20f, ForceMode.VelocityChange);

            //slam?
            slam = true;


        }
        if(slam && isGrounded && !slamClipPlayed)
        {
            audioManager.soundEffect("slam");
            slamClipPlayed = true;
            StartCoroutine(slamEnd());
        }
            

        //Si le joueur rel?che la touche, on arr?te
        if (Input.GetKeyUp(KeyCode.S))
        {
            endSlam();
        }
        if (!inMotion || !isGrounded)
            playerAnimator.SetFloat("Horizontal", 0f);

        //Vérifier si on tombe dans le vide
        if (rb.position.y <= 50f && !justRespawned)
            gameManager.tuerOneShot();
    }
    private void FixedUpdate()
    {
        //Si le joueur SLAM, on fait rien
        if (slam)
            return;

        // D?placer le personnage selon le vecteur de direction
        if(rotPlayer.y == 0f)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * speed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position - moveDirection.normalized * speed * Time.fixedDeltaTime);
        }
    }

    public void repousserJoueur(Transform posMonstre)
    {
        rb.AddForce(-posMonstre.forward *5f, ForceMode.VelocityChange);
        rb.velocity = new Vector2(rb.velocity.x, 1.5f);
    }

    private IEnumerator slamEnd()
    {
        yield return new WaitForSeconds(0.15f);
        endSlam();
    }

    private void endSlam()
    {
        //arr?te l'animation
        playerAnimator.SetBool("Crouch", false);
        //enl?ve la force.
        constantForce_player.force = Vector3.zero;
        //slam?
        slam = false;
        slamClipPlayed = false;
    }
}
