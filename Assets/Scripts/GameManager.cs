using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static int nbrVies =4;

    [Header("Scripts")]
    public UI_Script uI_Script;
    public PlayerMovements scriptMouvements;
    public Ragdoll ragdoll;


    [Header("Managers")]
    public AudioManager audioManager;
    public ParticleManager particleManager;



    // Start is called before the first frame update
    void Start()
    {
        //Lignes pour faire fonctionner WebGl
        #if !UNITY_EDITOR && UNITY_WEBGL			
        UnityEngine.WebGLInput.captureAllKeyboardInput = false;
        #endif

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enleverVie() {
        //Si le joueur à encore de la vie...
        if(nbrVies > 0)
        {
            // Faire joueur le bruit de dégâts
            audioManager.soundEffect("playerDamage");
            //Enlever un coeur à l'écran
            uI_Script.enleverVie();
            //Décrémentation de la variable interne pour la vie.
            nbrVies -= 1;
        }
        //Si il n'y a plus de vie
        if(nbrVies == 0)
        {
            //la partie est perdu
            partiePerdu();
        }
    }
    private void partiePerdu()
    {
        //Toggle du ragdoll
        ragdoll.Die();
        //Désactive les mouvements
        scriptMouvements.enabled = false;
        // Affiche le bon canvas
        uI_Script.trigger_GameOver();
        //mettre les particules de sang
        particleManager.activerSang();
    }

    public void tuerOneShot()
    {
        //tant qu'il y a de la vie on l'enlève.
        int x = GameManager.nbrVies;
        for (int i = 0; i <= x; i++)
        {
            enleverVie();
        }
    }
    public void mortBoss()
    {
        //affichage du canvas approprié à la victoire
        uI_Script.trigger_GameWon();
    }
}
