using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerAndGameEnd : MonoBehaviour
{

    float startTime = 0;
    bool playing = false;
    public TextMesh text;
    public float timeRecording = 180;
    public AudioClip[] audios;
    AudioSource aSource;

    void Start()
    {
        text = GetComponent<TextMesh>();
        aSource = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    public void Play()
    {
        startTime = Time.realtimeSinceStartup;
        playing = true;
        StartCoroutine(GnomeSound());
    }

    public IEnumerator GnomeSound()
    {
        while (playing)
        {
            yield return new WaitForSeconds(Random.Range(30, 40));
            aSource.PlayOneShot(audios[Random.Range(0, audios.Length)]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            float timeToDisplay = Time.realtimeSinceStartup - startTime;
            if (timeToDisplay > timeRecording)
            {
                playing = false;
                Application.Quit();
            }
            else
                DisplayTime(timeRecording - timeToDisplay);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        text.text = "" + Mathf.FloorToInt(timeToDisplay / 60) + ":" + Mathf.FloorToInt(timeToDisplay % 60).ToString("00");
    }
}
