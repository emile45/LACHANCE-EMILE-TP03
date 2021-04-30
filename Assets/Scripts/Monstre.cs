using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monstre : MonoBehaviour
{
    private Rigidbody playerRb;

    private NavMeshAgent navMeshAgent;
    private bool agentBusy = false;

    private Vector3 initialPos;

    private Vector3 lastKnownVector;

    public Transform monstre;

    public GameObject monstre_gmObject;

    public GameManager gameManager;

    public AudioManager audioManager;

    public float speed = 3f;

    public PlayerMovements playerMovements;

    void Start()
    {
        //Récupération et assignation des variables
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialPos = transform.position;
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
            audioManager.soundEffect("mortMonstre");
            Object.Destroy(monstre_gmObject);
        }
            
    }
    //Fonction publique pour quand le joueur entre en conatct avec un monstre.
    public void bodyCollision(Collider collider)
    {
        gameManager.enleverVie();
        playerMovements.repousserJoueur(monstre);
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
        audioManager.soundEffect("monstreWalk");
    }
}
