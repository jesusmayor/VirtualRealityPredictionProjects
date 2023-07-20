using UnityEngine;

public class WhenGrab : MonoBehaviour
{
    MeshRenderer mr;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }


    public void Interaccion()
    {
        DisplayGame.GetInstance().NewPoint();
        mr.enabled = false;
        this.enabled = false;
        col.enabled = false;
    }
}
