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
        #if !UNITY_EDITOR && UNITY_WEBGL			
        UnityEngine.WebGLInput.captureAllKeyboardInput = false;
        #endif

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enleverVie() {
        if(nbrVies > 0)
        {
            audioManager.soundEffect("playerDamage");
            uI_Script.enleverVie();
            nbrVies -= 1;
        }
        if(nbrVies == 0)
        {
            partiePerdu();
        }
    }
    private void partiePerdu()
    {
        ragdoll.Die();
        scriptMouvements.enabled = false;
        uI_Script.trigger_GameOver();
        particleManager.activerSang();
    }

    public void tuerOneShot()
    {
        int x = GameManager.nbrVies;
        for (int i = 0; i <= x; i++)
        {
            enleverVie();
        }
    }
}
