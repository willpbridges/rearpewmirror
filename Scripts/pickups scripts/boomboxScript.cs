using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomboxScript : MonoBehaviour
{
    public float radius;
    public AudioSource music;
    private AudioSource mainMusic;
    // Start is called before the first frame update
    void Start()
    {
        mainMusic = GameObject.FindGameObjectWithTag("Initializer").GetComponent<AudioSource>();
       mainMusic.Pause();
       music.time = 23;
       music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (music.time < 33 && music.time > 25)
        {
            destroyMirrorsInRadius(radius);
        }
        else if(music.time >= 33)
        {
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            {
                if(g.name != "Head")
                {
                    g.GetComponent<mirrorBreaker>().broken = false;
                }
            }
            music.Stop();
            mainMusic.UnPause();
            Destroy(gameObject);
        }
    }


    void destroyMirrorsInRadius(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        List<GameObject> players = new List<GameObject>();
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].tag == "Player" && colliders[i].name != "Head")
            {
                players.Add(colliders[i].gameObject);
            }
        }
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(players.Contains(g) && g.name != "Head")
            {
                g.GetComponent<mirrorBreaker>().broken = true;
            }
            else if(!players.Contains(g) && g.name != "Head")
            {
                g.GetComponent<mirrorBreaker>().broken = false;
            }
        }
    }
}
