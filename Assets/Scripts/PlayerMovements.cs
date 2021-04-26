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

    public static bool inMotion;

    private Vector3 rotPlayer;

    private bool isGrounded;

    private ConstantForce constantForce_player;

    private bool slam;

    public AudioManager audioManager;



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

        //Si le joueur ne touche pas le sol, ses mouvements sont réduits de 50%
        if (!isGrounded && !slam)
        {
            // Vertical (W, S et Joystick avant/arrière)
            inputHorizontal = Input.GetAxis("Horizontal") / 2;
        }
        // Si le joueur touche le sol, vitesse normale.
        if(isGrounded && !slam)
        {
            inputHorizontal = Input.GetAxis("Horizontal");
        }

        //Récupération de la rotation du joueur
        rotPlayer = rb.rotation.eulerAngles;

        //Détermine la vitesse du joueur et d'animation.
        animationSpeed = Mathf.Lerp(animationSpeed, 2f, lerpSpeed);
        speed = Mathf.Lerp(speed, speedRunning, lerpSpeed);
        
        //Détermine si le joueur bouge.
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



        //Détermine la direction de déplacement du personnage.
        moveDirection = transform.forward * inputHorizontal;

        //Sauter
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            // Bruits de saut
            audioManager.sautJoueur();
            new WaitForSeconds(audioManager.jump.length);
            //ne touche pas le sol
            isGrounded = false;
            //Déclenche l'animation
            playerAnimator.SetTrigger("Jump");
            //modifie la vélocitée
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        //Slam seulement si le joueur est dans le airs
        if (Input.GetKeyDown(KeyCode.S) && isGrounded == false)
        {
            //déclenche l'animation
            playerAnimator.SetBool("Crouch", true);
            //modifie la vélocitée et ajoute la force vers le bas.
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * 50f, ForceMode.VelocityChange);

            //slam?
            slam = true;


        }
        //Si le joueur relâche la touche, on arrête
        if (Input.GetKeyUp(KeyCode.S))
        {
            //arrête l'animation
            playerAnimator.SetBool("Crouch", false);
            //enlève la force.
            constantForce_player.force = Vector3.zero;
            //slam?
            slam = false;
        }
        if (!inMotion || !isGrounded)
            playerAnimator.SetFloat("Horizontal", 0f);
            
    }
    private void FixedUpdate()
    {
        //Si le joueur SLAM, on fait rien
        if (slam)
            return;

        // Déplacer le personnage selon le vecteur de direction
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
        rb.AddExplosionForce(5f, posMonstre.position, 5f, 0.5f, ForceMode.Impulse);
        rb.AddForce(-posMonstre.forward *7, ForceMode.VelocityChange);
        rb.velocity = new Vector2(rb.velocity.x, 2);
    }

}
