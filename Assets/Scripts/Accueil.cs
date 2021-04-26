using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Accueil : MonoBehaviour
{
    //Déclaration des variables publiques
    public Button btnJouer;
    public Button btnInstructions;

    void Awake()
    {
        //Assignation des EventListener
        btnJouer.onClick.AddListener(btnJouer_OnClick);
        btnInstructions.onClick.AddListener(btnInstructions_OnClick);

    }

    void btnJouer_OnClick()
    {
        SceneManager.LoadScene("Main");
    }
    void btnInstructions_OnClick()
    {

    }
}
