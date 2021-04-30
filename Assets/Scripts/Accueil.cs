﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Accueil : MonoBehaviour
{
    //Déclaration des variables publiques
    [Header("Boutons")]
    public Button btnJouer;
    public Button btnInstructions;
    public Button btnFleche;
    public Button btnFlecheRetour;
    public Button btnFlecheFin;

    [Header("Game Objects")]
    public GameObject menuTouches;
    public GameObject instructions;
    public GameObject canvasPrincipale;
    public GameObject canvasFin;


    private float m_alpha;
    void Awake()
    {
        //Assignation des EventListener
        btnJouer.onClick.AddListener(btnJouer_OnClick);
        btnInstructions.onClick.AddListener(btnInstructions_OnClick);
        btnFleche.onClick.AddListener(btnFleche_OnClick);
        btnFlecheRetour.onClick.AddListener(btnFlecheRetour_OnClick);
        btnFlecheFin.onClick.AddListener(btnFlecheFin_OnClick);

        menuTouches.SetActive(false);
        instructions.SetActive(false);
        canvasFin.SetActive(false);

    }

    void btnJouer_OnClick()
    {
        SceneManager.LoadScene("Main");
    }
    void btnInstructions_OnClick()
    {
        menuTouches.SetActive(true);

    }

    void btnFleche_OnClick()
    {
        instructions.SetActive(true);

    }
    void btnFlecheRetour_OnClick()
    {
        instructions.SetActive(false);
    }
    void btnFlecheFin_OnClick()
    {
        instructions.SetActive(false);
        menuTouches.SetActive(false);
        canvasPrincipale.SetActive(false);
        canvasFin.SetActive(true);
    }
}
