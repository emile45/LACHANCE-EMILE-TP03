using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem sangMortJoueur;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activerSang()
    {
        sangMortJoueur.Play();
    }
}
