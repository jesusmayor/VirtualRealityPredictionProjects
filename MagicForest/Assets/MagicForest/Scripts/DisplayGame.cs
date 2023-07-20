using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGame : MonoBehaviour
{

    static DisplayGame instance;
    [SerializeField]
    TextMesh textbox;
    [SerializeField]
    AudioSource audioSource;
    int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        if (textbox == null)
        {
            textbox = GetComponent<TextMesh>(); 
            textbox.text = "Puntos: " + points; 
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public static DisplayGame GetInstance()
    {
        return instance;
    }

    public void NewPoint()
    {
        audioSource.Play();
        points++;
        textbox.text = "Puntos: " + points;
        if (points == 10)
        {
            textbox.color = Color.green;
            StartCoroutine(CloseAtFiveuSeconds());
        }
    }

    IEnumerator CloseAtFiveuSeconds()
    {
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
