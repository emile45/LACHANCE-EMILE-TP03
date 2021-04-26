using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour
{
    //HUD    
    public RawImage coeur1;
    public RawImage coeur2;
    public RawImage coeur3;
    public RawImage coeur4;
    public Button btnReglages;

    //Menu Reglages
    public Button btnQuitterMenu;
    public GameObject menuOptions;
    public GameObject txtPause;

    //Éléments du menu Partie Perdue
    public GameObject gameOver;
    public Button btnRecommencer;
    public Button btnQuitterPartie;

    //Liste contenant les coeurs du joueur
    private List<RawImage> listVies = new List<RawImage>();
    private int nbrVies;

    private int updateInterval = 80; // in frames


    void Start()
    {
        nbrVies = GameManager.nbrVies;

        listVies.Add(coeur1);
        listVies.Add(coeur2);
        listVies.Add(coeur3);
        listVies.Add(coeur4);

        //Assignation des EventListener
        btnReglages.onClick.AddListener(btnReglages_OnClick);
        btnQuitterMenu.onClick.AddListener(btnReglages_OnClick);
        btnQuitterPartie.onClick.AddListener(btnQuitterPartie_OnClick);
        btnRecommencer.onClick.AddListener(btnRecommencer_OnClick);

        menuOptions.SetActive(false);
        gameOver.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            btnReglages_OnClick();
        }
        FixedUpdate();
    }

    public void enleverVie()
    {
        listVies[GameManager.nbrVies - 1].enabled = false;
    }

    private void btnReglages_OnClick()
    {
        if (menuOptions.activeSelf == false)
        {
            menuOptions.SetActive(true);
            btnReglages.enabled = false;
            Time.timeScale = 0;
        }    
        else
        {
            menuOptions.SetActive(false);
            btnReglages.enabled = true;
            Time.timeScale = 1;
        }
            
    }
    private void btnQuitterPartie_OnClick()
    {
        SceneManager.LoadScene("Accueil");
    }
    private void btnRecommencer_OnClick()
    {
        SceneManager.LoadScene("Main");
        GameManager.nbrVies = 4;
    }

    public void trigger_GameOver()
    {
        gameOver.SetActive(true);
    }
    private void FixedUpdate()
    {
        if(Time.timeScale == 0)
            if (Time.frameCount % this.updateInterval != 0) return;
        {
            if (txtPause.activeSelf == true)
                txtPause.SetActive(false);
            else
                txtPause.SetActive(true);
        }
    }
}
