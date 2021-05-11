using System.Collections;
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
    public AudioSource sourceMusic;

    [Header("Clips audio")]
    public AudioClip jump;
    public AudioClip spikesDeath;
    public AudioClip slamClip;
    public AudioClip hitmaker;
    public AudioClip clipFin;

    private float[] m_audioSpectrum;

    public static float spectrumValue { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        m_audioSpectrum = new float[128];
    }

    // Update is called once per frame
    void Update()
    {
        sourceMusic.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
            spectrumValue = m_audioSpectrum[0] * 100;
        //Debug.Log(spectrumValue);
    }

    public void soundEffect(string name)
    {
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
    private IEnumerator musicFin()
    {
        sourceMusic.Stop();
        sourceMusic.PlayOneShot(clipFin);
        yield return new WaitForSeconds(clipFin.length);
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
