using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static int nbrVies =4;

    public UI_Script uI_Script;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enleverVie() {
        if(nbrVies > 0)
        {
            uI_Script.enleverVie();
            nbrVies -= 1;
        }
        if(nbrVies == 0)
        {
            partiePerdu();
        }
    }
    private void partiePerdu()
    {
        uI_Script.trigger_GameOver();   
    }
}
