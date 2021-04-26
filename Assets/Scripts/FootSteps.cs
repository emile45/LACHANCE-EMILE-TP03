﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioClip foot1;
    public AudioClip foot2;
    public AudioClip foot3;
    public AudioClip foot4;
    public AudioClip foot5;
    public AudioClip foot6;
    public AudioClip foot7;
    public AudioClip foot8;
    public AudioClip foot9;
    public AudioClip foot10;


    private AudioSource audioSource;
    private List<AudioClip> listSteps = new List<AudioClip>();

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        listSteps.Add(foot1);
        listSteps.Add(foot2);
        listSteps.Add(foot3);
        listSteps.Add(foot4);
        listSteps.Add(foot5);
        listSteps.Add(foot6);
        listSteps.Add(foot7);
        listSteps.Add(foot8);
        listSteps.Add(foot9);
        listSteps.Add(foot10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Step()
    {
        // Faire joueur le son
        audioSource.PlayOneShot(listSteps[Random.Range(0, listSteps.Count)]);

    }
}