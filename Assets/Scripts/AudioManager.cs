using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    public AudioSource sourceJoueur;
    public AudioSource sourceMonstre;

    public AudioClip monstreMort;
    public AudioClip jump;
    public AudioClip spikesDeath;

    //Audio clips


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            mortMonstre();
        }
    }

    public void mortMonstre()
    {
        sourceMonstre.PlayOneShot(monstreMort);
    }

    public void sautJoueur()
    {
        sourceJoueur.PlayOneShot(jump);
    }
    public void mortPikes()
    {
        sourceJoueur.PlayOneShot(spikesDeath);
    }

}
