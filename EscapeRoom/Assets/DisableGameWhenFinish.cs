using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameWhenFinish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void FinisHim()
    {
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(8f);
        Application.Quit(); 
    }
}
