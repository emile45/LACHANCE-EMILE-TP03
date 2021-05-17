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
    private bool justHit = false;
    private GameObject slime;
    public GameObject sangMonstre;

    //Vitesse
    public float speed = 3f;


    private GameManager gameManager;
    private AudioManager audioManager;

    // Publiques
    [Header("Clips audio")]
    public AudioClip walking;
    public AudioClip mortMontre;


    [Header("Others")]
    public GameObject monstre_gmObject;
    public Collider colliderCorp;
    public int nbrViesM;

    void Start()
    {
        //Récupération et assignation des variables
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialPos = transform.position;
        sourceMonstre = GetComponentInParent<AudioSource>();

        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        slime = GameObject.Find("Slime");

    }
    void Update()
    {
        //Si l'agent n'est pas occupé, on le fait bouger.
        if(!agentBusy)
            StartCoroutine(Patrol(getRandomDestination(), speed));        
    }

    //tuer le monstre
    public void headCollision(Collider collider){
        //Si le joueur entre en collision avec la tête...
        if (playerRb.velocity.y < -0.01f && justHit == false)
        {
            justHit = true;
            if (nbrViesM == 1)
                nbrViesM--;
            else
            {
                PlayerMovements.onTeteMonstre = true;
                StartCoroutine(monstreDamageDelay());
            }
            //Si le monstre n'a plus de vie...
            if (nbrViesM == 0)
            {
                //Instantiation du sang
                GameObject sang = Instantiate(sangMonstre, navMeshAgent.transform.position, Quaternion.identity);
                //Faire jouer le bruit de mort du monstre
                sourceMonstre.PlayOneShot(mortMontre);

                //Si c'est le boss, on appel un autre fonction 
                if (monstre_gmObject.name == "MonstreBleu")
                    gameManager.mortBoss();

                //Destruction du monstre et sang
                Object.Destroy(monstre_gmObject);
                Destroy(sang, 4f);
                
            }
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
        //Si le monstre ne marche pas déjà, on le fait marcher
        if(!stopWalkingNoise)
            StartCoroutine(monstreWalk());
    }
    private IEnumerator monstreWalk()
    {
        //Bruits de déplacements du monstre
        sourceMonstre.PlayOneShot(walking);
        yield return new WaitForSeconds(walking.length);
    }

    private IEnumerator mortMonstre()
    {
        //désactiver le collider du monstre pour ne pas qu'il puisse donné des dégâts
        colliderCorp.enabled = false;
        //Jouer le bruit de mort du monstre.
        if (!sourceMonstre.isPlaying)
            yield return new WaitForSeconds(mortMontre.length);

        
    }

    //délais entre les attaques du monstre
    private IEnumerator monstreDamageDelay()
    {
        yield return new WaitForSeconds(1f);
        justHit = false;
        nbrViesM--;
    }
}
