using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public static bool gameOver = false;
    public Text timer;
    public GameObject capText;
    public GameObject contestedText;
    private bool blueCapping;
    private bool orangeCapping;

    private bool blueControlled = false;
    private bool orangeControlled = false;

    public int blueOnObj = 0;
    public int orangeOnObj = 0;
    public float blueTimeLeft = 30;
    public float orangeTimeLeft = 30;

    private bool blueWon;
    private bool orangeWon;

    public float capTimeInit;
    private float capTimeLeft;

    public Color orangeColor;
    public Color blueColor;
    private Color lastColor;
    private Renderer rend;
    private float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        capTimeLeft = capTimeInit;
        rend = GetComponent<Renderer>();
        rend.material.color = Color.black;
        lastColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        //tick down time if controlled and not contested
        if (blueControlled && orangeOnObj < 1 && !(blueWon || orangeWon))
        {
            blueTimeLeft -= Time.deltaTime;
            timer.text = ((int)blueTimeLeft).ToString();
            timer.color = new Color(0f, 0.431f, 1f, 1f);
            contestedText.SetActive(false);
        }
        if (orangeControlled && blueOnObj < 1 && !(blueWon || orangeWon))
        {
            orangeTimeLeft -= Time.deltaTime;
            timer.text = ((int)orangeTimeLeft).ToString();
            timer.color = new Color(1f, 0.475f, 0f, 1f);
            contestedText.SetActive(false);
        }

        //turn down cap time if either side is capping
        if (orangeCapping || blueCapping)
        {
            capTimeLeft -= Time.deltaTime;
            if (orangeCapping)
            {
                capText.GetComponent<Text>().color = orangeColor;
                capText.SetActive(true);
                rend.material.color = Color.Lerp(lastColor, orangeColor, t);
            }
            if (blueCapping)
            {
                capText.GetComponent<Text>().color = blueColor;
                capText.SetActive(true);
                rend.material.color = Color.Lerp(lastColor, blueColor, t);
            }
            if (t < capTimeInit)
            {
                t += Time.deltaTime / capTimeInit;
            }
        }
        else
        {
            t = 0;
            rend.material.color = Color.Lerp(rend.material.color, lastColor, Time.deltaTime);
            capText.SetActive(false);
        }
        //if cap complete, set obj control
        if (capTimeLeft <= 0)
        {
            if (blueCapping)
            {
                blueControlled = true;
                orangeControlled = false;
                lastColor = blueColor;
            }
            if (orangeCapping)
            {
                orangeControlled = true;
                blueControlled = false;
                lastColor = orangeColor;
            }
            t = 0;
            capText.SetActive(false);
        }
        //win states
        if (blueTimeLeft <= 0 || orangeTimeLeft <= 0)
        {
            if (blueTimeLeft <= 0)
            {
                blueWon = true;
                timer.text = "Blue Team Wins!";
            }
            if (orangeTimeLeft <= 0)
            {
                orangeWon = true;
                timer.text = "Orange Team Wins";
            }
            gameOver = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!blueWon && !orangeWon)
        {
            InputManager input = other.GetComponent<InputManager>();
            int pNum = -1;
            if (input)
            {
                pNum = (int)input.playerNumber;
            }

            //add blue player to the player count
            if (pNum != -1 && pNum % 2 == 0) // blue players have even player number
            {
                blueOnObj += 1;
                //set capping if needed
                if (orangeOnObj == 0 && blueOnObj > 0 && !blueControlled && !blueCapping)
                {
                    blueCapping = true;
                }

            }
            //same as above code
            if (pNum != -1 && pNum % 2 != 0) // orange players have odd player number
            {
                orangeOnObj += 1;
                if (orangeOnObj > 0 && blueOnObj == 0 && !orangeControlled && !orangeCapping)
                {
                    orangeCapping = true;
                }
            }
            //if no one on point, reset capping
            if (orangeOnObj != 0 && blueOnObj != 0)
            {
                orangeCapping = false;
                blueCapping = false;
                capTimeLeft = capTimeInit;
                contestedText.SetActive(true);
            }

        }
    }
    public void check()
    {
        if (orangeOnObj > 0 && blueOnObj == 0 && !orangeControlled && !orangeCapping)
        {
            contestedText.SetActive(false);
            orangeCapping = true;
        }
        else if (orangeOnObj == 0 && blueOnObj > 0 && !blueControlled && !blueCapping)
        {
            contestedText.SetActive(false);
            blueCapping = true;
        }
        else if (orangeOnObj != 0 && blueOnObj != 0)
        {
            orangeCapping = false;
            blueCapping = false;
            capTimeLeft = capTimeInit;
            contestedText.SetActive(true);
        }
        else if (blueOnObj == 0)
        {
            blueCapping = false;
        }
        else if (orangeOnObj == 0)
        {
            orangeCapping = false;
        }
        else if (orangeOnObj + blueOnObj == 0)
        {
            orangeCapping = false;
            blueCapping = false;
            capTimeLeft = capTimeInit;
            contestedText.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!blueWon && !orangeWon)
        {
            InputManager input = other.GetComponent<InputManager>();
            int pNum = -1;
            if (input)
            {
                pNum = (int)input.playerNumber;
            }

            //set count values each time player exits
            if (pNum != -1 && pNum % 2 == 0) // blue players have even player number
            {
                blueOnObj -= 1;

            }
            if (pNum != -1 && pNum % 2 != 0) // orange players have odd player number
            {
                orangeOnObj -= 1;
            }
            //set capping if point is uncontested now
            if (orangeOnObj > 0 && blueOnObj == 0 && !orangeControlled && !orangeCapping)
            {
                orangeCapping = true;
                contestedText.SetActive(false);
            }
            if (orangeOnObj == 0 && blueOnObj > 0 && !blueControlled && !blueCapping)
            {
                blueCapping = true;
                contestedText.SetActive(false);
            }
            //stop capping if no one is on point
            if (blueOnObj == 0)
            {
                blueCapping = false;
            }
            if (orangeOnObj == 0)
            {
                orangeCapping = false;
            }
            if (orangeOnObj + blueOnObj == 0)
            {
                capTimeLeft = capTimeInit;
            }
        }

    }
}
