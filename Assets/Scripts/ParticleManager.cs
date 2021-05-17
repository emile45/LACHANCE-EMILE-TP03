using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem sangMortJoueur;



    public void activerSang()
    {
        //activer le sang du joueur
        sangMortJoueur.Play();
    }
}
