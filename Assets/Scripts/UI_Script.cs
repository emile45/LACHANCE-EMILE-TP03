using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour
{
    //HUD    
    [Header("Coeurs et Reglages")]
    public RawImage coeur1;
    public RawImage coeur2;
    public RawImage coeur3;
    public RawImage coeur4;
    public Button btnReglages;

    //Menu Reglages
    [Header("Menu Reglages")]
    public Button btnQuitterMenu;
    public GameObject menuOptions;
    public GameObject txtPause;
    public Slider sliderMaster;
    public Slider sliderMusic;
    public Slider sliderJoueur;
    public Slider sliderMonstres;


    //Éléments du menu Partie Perdue
    [Header("Éléments de partie perdue et autres")]
    public GameObject canvasPrincipal;
    public GameObject gameOver;
    public Button btnRecommencer;
    public Button btnQuitterPartie;

    [Header("Éléments de partie gangnée")]
    public GameObject canvasFin;
    public GameObject cameraFin;
    public Renderer backgroundFin;
    public Button btnQuitFin;

    [Header("Managers")]
    public AudioManager audioManager;

    //Liste contenant les coeurs du joueur
    private List<RawImage> listVies = new List<RawImage>();
    private List<Color> listColors = new List<Color>();
    private int nbrVies;

    private float updateInterval = 80; // in frames
    private bool colorFlash=false;


    void Awake()
    {
        // Setup des sliders
        sliderSetup(sliderMaster);
        sliderSetup(sliderMusic);
        sliderSetup(sliderJoueur);
        sliderSetup(sliderMonstres);


        nbrVies = GameManager.nbrVies;

        listVies.Add(coeur1);
        listVies.Add(coeur2);
        listVies.Add(coeur3);
        listVies.Add(coeur4);

        //Assignation des EventListener
        btnReglages.onClick.AddListener(btnReglages_OnClick);
        btnQuitterMenu.onClick.AddListener(btnReglages_OnClick);
        btnQuitterPartie.onClick.AddListener(btnQuitterPartie_OnClick);
        btnQuitFin.onClick.AddListener(btnQuitterPartie_OnClick);
        btnRecommencer.onClick.AddListener(btnRecommencer_OnClick);
        sliderMaster.onValueChanged.AddListener(slider_Master);
        sliderMusic.onValueChanged.AddListener(slider_Music);
        sliderJoueur.onValueChanged.AddListener(slider_Joueur);
        sliderMonstres.onValueChanged.AddListener(slider_Monstres);

        menuOptions.SetActive(false);
        gameOver.SetActive(false);
        canvasFin.SetActive(false);
        cameraFin.SetActive(false);

        PlayerMovements.justRespawned = false;

        //Ajout des couleurs de background a une liste.
        listColors.Add(Color.green);
        listColors.Add(Color.blue);
        listColors.Add(Color.cyan);
        listColors.Add(Color.red);
        listColors.Add(Color.magenta);
        listColors.Add(Color.yellow);
    }
    void sliderSetup(Slider slider)
    {
        //valeurs par défaut des sliders
        slider.minValue = 0.001f;
        slider.maxValue = 1.6f;


        slider.value = 0.4f;
    }


    // Update is called once per frame
    void Update()
    {
        //Si P on ouvre le menu
        if (Input.GetKeyDown(KeyCode.P))
        {
            btnReglages_OnClick();
        }

        //appel du fixed update pour faire flasher le text de pause et les couleurs à la fin 
        FixedUpdate();

    }

    public void enleverVie()
    {
        //enlever un coeur de l'écran
        listVies[GameManager.nbrVies - 1].enabled = false;
    }

    private void btnReglages_OnClick()
    {
        //Ouvrire le menu réglages s'il est fermé et vis-versa
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
        //remettre le compteur de vie a 4
        GameManager.nbrVies = 4;
        //charger la scène d'acceuil
        SceneManager.LoadScene("Accueil");
    }
    private void btnRecommencer_OnClick()
    {
        //recharger la scène avec les paramètres par défaut lorsque le bouton recommencer est appuyer
        PlayerMovements.justRespawned = true;
        SceneManager.LoadScene("Main");
        GameManager.nbrVies = 4;
        Time.timeScale = 1;
        //Attendre avant de changer le booléen "justRespawned"
        StartCoroutine(justRespawnedEnd());
    }

    public void trigger_GameOver()
    {
        //afficher le canvas de partie perdu
        gameOver.SetActive(true);
    }

    public void trigger_GameWon()
    {
        //Changer la caméra, canvas fin et désactiver le canvas principal
        cameraFin.SetActive(true);
        canvasFin.SetActive(true);
        canvasPrincipal.SetActive(false);

        //Musique de fin
        audioManager.soundEffect("musicFin");
        colorFlash = true;
    }
    private void FixedUpdate()
    {
        //réduire la vitesse d'update
        if (Time.frameCount % this.updateInterval != 0) return;

        //flasher le text en pause
        if (Time.timeScale == 0)     
        {
            if (txtPause.activeSelf == true)
                txtPause.SetActive(false);
            else
                txtPause.SetActive(true);
        }
        //flasher le couleurs
        if (colorFlash)
        {
            backgroundFin.material.color = listColors[Random.Range(0, listColors.Count)];
        }

    }
    //Contrôle de slider Master
    private void slider_Master(float Value)
    {
        audioManager.sliderControl("Master", Value);
    }
    //Contrôle de slider Music
    private void slider_Music(float Value)
    {
        audioManager.sliderControl("Music", Value);
    }
    //Contrôle de slider Joueur
    private void slider_Joueur(float Value)
    {
        audioManager.sliderControl("Joueur", Value);
    }
    //Contrôle de slider Monstres
    private void slider_Monstres(float Value)
    {
        audioManager.sliderControl("Monstres", Value);
    }


    private IEnumerator justRespawnedEnd()
    {        //Attendre avant de changer le booléen "justRespawned"
        yield return new WaitForSeconds(2f);
        PlayerMovements.justRespawned = false;
    }
}
