using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //Variables publiques
    [Header("Mixer")]
    public AudioMixer mainMixer;
    [Header("Sources")]
    public AudioSource sourceJoueur;
    public AudioSource sourceMusic;

    [Header("Clips audio")]
    public AudioClip jump;
    public AudioClip spikesDeath;
    public AudioClip slamClip;
    public AudioClip hitmaker;
    public AudioClip clipFin;


    public void soundEffect(string name)
    {
        //Switch pour faire joueur les différents bruits du jeux.
        switch (name)
        {
            case "slam":
                StartCoroutine(slam());
                break;
            case "sautJoueur":
                StartCoroutine(sautJoueur());
                break;
            case "mortPikes":
                StartCoroutine(mortPikes());
                break;
            case "playerDamage":
                StartCoroutine(playerDamage());
                break;
            case "musicFin":
                StartCoroutine(musicFin());
                break;
        }
    }

    //Bruit de saut du joueur
    private IEnumerator sautJoueur()
    {
        sourceJoueur.PlayOneShot(jump);
        yield return new WaitForSeconds(jump.length);
    }
    //Bruit de la mort dans les pikes
    private IEnumerator mortPikes()
    {
        sourceJoueur.PlayOneShot(spikesDeath);
        yield return new WaitForSeconds(spikesDeath.length);
    }
    //Bruit de l'attérissage après slam
    private IEnumerator slam()
    {
        sourceJoueur.PlayOneShot(slamClip);
        yield return new WaitForSeconds(slamClip.length);
    }
    //Bruit lorsque le joueur se prends du degat
    private IEnumerator playerDamage()
    {
        sourceJoueur.PlayOneShot(hitmaker);
        yield return new WaitForSeconds(hitmaker.length);
    }
    //Musique qui est joueur lorsque la partie est gagnée
    private IEnumerator musicFin()
    {
        sourceMusic.Stop();
        sourceMusic.PlayOneShot(clipFin);
        yield return new WaitForSeconds(clipFin.length);
    }

    //Switch pour le contrôle des volumes
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
