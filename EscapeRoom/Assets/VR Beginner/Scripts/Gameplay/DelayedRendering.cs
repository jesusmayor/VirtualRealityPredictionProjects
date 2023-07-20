using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedRendering : MonoBehaviour
{

    public float time;
    Renderer renderer;

    void OnEnable()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        StartCoroutine(RenderingAt());
    }


    IEnumerator RenderingAt()
    {
        yield return new WaitForSeconds(time);
        renderer.enabled = true;
    }
}
