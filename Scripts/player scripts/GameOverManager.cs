using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject UIParent;
    public Button restart;
    public Button quit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!UIParent.activeSelf && Objective.gameOver)
        {
            UIParent.SetActive(true);
        }
        
    }
}
