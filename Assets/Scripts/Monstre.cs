using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monstre : MonoBehaviour
{
    //Privates
    private Rigidbody playerRb;
    private NavMeshAgent navMeshAgent;
    private bool agentBusy = false;
    private Vector3 initialPos;
    private Vector3 lastKnownVector;
    private AudioSource sourceMonstre;
    private bool stopWalkingNoise=false;

    //Vitesse
    public float speed = 3f;


    private GameManager gameManager;
    private AudioManager audioManager;
    private PlayerMovements playerMovements;


    [Header("Clips audio")]
    public AudioClip walking;
    public AudioClip mortMontre;


    [Header("Others")]
    public GameObject monstre_gmObject;
    public Collider colliderCorp;

    void Start()
    {
        //Récupération et assignation des variables
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialPos = transform.position;
        sourceMonstre = GetComponent<AudioSource>();

        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        playerMovements = FindObjectOfType<PlayerMovements>();

    }
    void Update()
    {
        //Si l'agent n'est pas occupé, on le fait bouger.
        if(!agentBusy)
            StartCoroutine(Patrol(getRandomDestination(), speed));
    }

    //tuer le monstre
    public void headCollision(Collider collider){
        if (playerRb.velocity.y < 0f)
        {
            if (!stopWalkingNoise)
            {
                stopWalkingNoise = true;
                sourceMonstre.Stop();
            }
            StartCoroutine(mortMonstre());
            audioManager.soundEffect("mortMonstre");
        }
            
    }
    //Fonction publique pour quand le joueur entre en conatct avec un monstre.
    public void bodyCollision(Collider collider)
    {
        gameManager.enleverVie();
    }
    Vector3 getRandomDestination()
    {
        //Cette fonction retourne un position aléatoire dans un radius de 5 autour de la position du monstre

        float zLimit = Random.Range(initialPos.z - 5f, initialPos.z + 5f);
        
        //stocker le dernier vecteur évite que le monstre essaie d'allez dans le vide.
        lastKnownVector = new Vector3(initialPos.x, initialPos.y, zLimit);
        return lastKnownVector;
    }

    //Faire patrouille le monstre
    IEnumerator Patrol(Vector3 destination, float speed)
    {
        agentBusy = true;

        // Modifier la vitesse du PNJ
        navMeshAgent.speed = speed;

        // Me déplacer vers la destination.
        navMeshAgent.SetDestination(destination);

        // Tant que je ne suis pas rendu à destination, je ne fait rien d'autre.
        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance < 0.01f)
        {
            yield return null;
        }
        // Rendu à destination, je prends une pause.
        yield return new WaitForSeconds(1f);

        // Commencer une nouvelle patrouille
        agentBusy = false;
    }

    public void Walk()
    {
        if(!stopWalkingNoise)
            StartCoroutine(monstreWalk());
    }
    private IEnumerator monstreWalk()
    {
        sourceMonstre.PlayOneShot(walking);
        yield return new WaitForSeconds(walking.length);
    }
    private IEnumerator mortMonstre()
    {
        colliderCorp.enabled = false;
        if (!sourceMonstre.isPlaying) {
            sourceMonstre.PlayOneShot(mortMontre);
            yield return new WaitForSeconds(mortMontre.length);
            Object.Destroy(monstre_gmObject);
        }

    }
}
