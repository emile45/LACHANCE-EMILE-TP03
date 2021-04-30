﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Mixer")]
    public AudioMixer mainMixer;
    [Header("Sources")]
    public AudioSource sourceJoueur;
    public AudioSource sourceMonstre;

    [Header("Clips audio")]
    public AudioClip monstreMort;
    public AudioClip jump;
    public AudioClip spikesDeath;
    public AudioClip slamClip;
    public AudioClip monstreDeplacement;
    public AudioClip hitmaker;
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

    public void soundEffect(string name)
    {
        switch (name)
        {
            case "slam":
                StartCoroutine(slam());
                break;
            case "mortMonstre":
                StartCoroutine(mortMonstre());
                break;
            case "sautJoueur":
                StartCoroutine(sautJoueur());
                break;
            case "mortPikes":
                StartCoroutine(mortPikes());
                break;
            case "monstreWalk":
                StartCoroutine(monstreWalk());
                break;
            case "playerDamage":
                StartCoroutine(playerDamage());
                break;
        }
    }

    private IEnumerator mortMonstre()
    {
        sourceMonstre.PlayOneShot(monstreMort);
        yield return new WaitForSeconds(monstreMort.length);
    }
    private IEnumerator monstreWalk()
    {
        sourceMonstre.PlayOneShot(monstreDeplacement);
        yield return new WaitForSeconds(monstreDeplacement.length);
    }
    private IEnumerator sautJoueur()
    {
        sourceJoueur.PlayOneShot(jump);
        yield return new WaitForSeconds(jump.length);
    }
    private IEnumerator mortPikes()
    {
        sourceJoueur.PlayOneShot(spikesDeath);
        yield return new WaitForSeconds(spikesDeath.length);
    }

    private IEnumerator slam()
    {
        sourceJoueur.PlayOneShot(slamClip);
        yield return new WaitForSeconds(slamClip.length);
    }

    private IEnumerator playerDamage()
    {
        sourceJoueur.PlayOneShot(hitmaker);
        yield return new WaitForSeconds(hitmaker.length);
    }

    public void sliderControl(string source, float value)
    {
        switch (source)
        {
            case "Master":
                mainMixer.SetFloat("Master", Mathf.Log(value) * 20f);
                break;
            case "Music":
                mainMixer.SetFloat("Music", Mathf.Log(value) * 20f);
                break;
            case "Joueur":
                mainMixer.SetFloat("Player", Mathf.Log(value) * 20f);
                break;
            case "Monstres":
                mainMixer.SetFloat("Monstre", Mathf.Log(value) * 20f);
                break;
        }
    }
}
