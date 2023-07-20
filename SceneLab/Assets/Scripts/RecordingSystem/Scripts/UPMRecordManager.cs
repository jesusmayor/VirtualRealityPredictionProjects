using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class UPMRecordManager : MonoBehaviour
{
    static UPMRecordManager instance = null;
    static Coroutine mainLoopCoroutine = null;
    static HashSet<UPMRecordNode> observersSet = new HashSet<UPMRecordNode>();

    FileStream fs = null;

    [SerializeField]
    [Tooltip("Time elapsed until take a new sample.")]
    [Range(0.001f,10f)]
    float sampleRate = 0.1f;

    [SerializeField]
    [Tooltip("Should start sampling mannually? (use StartMainLoop)")]
    bool startSampling = false;

    [SerializeField]
    [Tooltip("Include or not the header in the csv.")]
    bool includeHeaders = true;

    [SerializeField]
    [Tooltip("File path were will be saved the data")]
    string fileName = null;

    [SerializeField]
    [Tooltip("Should start sampling mannually? (use StartMainLoop)")]
    string separator = ";";
    public string Separator
    {
        get => separator;
        set => separator = value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    static public UPMRecordManager GetInstance()
    {
        UPMRecordManager [] instances = FindObjectsOfType<UPMRecordManager>();
        instance = (instances.Length > 0) ? instances[0] : null;

        if (instance == null)
        {
            GameObject obj = new GameObject("UPMRecordManager[Default]");
            instance = obj.AddComponent<UPMRecordManager>();
        }

        return instance;
    }

    private void Start()
    {
        if (fileName == null || fileName == "")
            fileName = @"\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".csv";
        
        if (startSampling)
            if (instance == this) //won't be null, but checking anyways.
                StartMainLoop();

    }

    public void StartMainLoop()
    {
        Debug.Log("OutputFilePath" + Application.persistentDataPath + "\\.." + fileName);

        if (File.Exists(Application.persistentDataPath + "\\.." + fileName))
            File.Delete(Application.persistentDataPath + "\\.." + fileName);

        fs = File.Create(Application.persistentDataPath + "\\.." + fileName);

        if (includeHeaders)
        {
            string header = "Time_since_startup" + separator;
            foreach (UPMRecordNode node in observersSet)
            {
                header += node.Header();
            }
            if (header.EndsWith(separator))
                header = header.Remove(header.Length - 1, 1);

            header += "\n";

            byte[] info = new UTF8Encoding(true).GetBytes(header);
            fs?.Write(info, 0, info.Length);
        }

        mainLoopCoroutine = StartCoroutine(SampleMainLoop());
    }

    public bool StopMainLoop()
    {
        if (mainLoopCoroutine == null)
            return false;

        StopCoroutine(SampleMainLoop());
        mainLoopCoroutine = null;

        fs?.Dispose();

        return true;
    }

    public bool AddDatasetNode(UPMRecordNode node)
    {
        return observersSet.Add(node);
    }

    public bool RemoveDatasetNode(UPMRecordNode node)
    {
        return observersSet.Remove(node);
    }

    IEnumerator SampleMainLoop()
    {
        WaitForSeconds waitTime = new WaitForSeconds(sampleRate);
        while (true)
        {
            string row = Time.realtimeSinceStartup + separator;
            foreach (UPMRecordNode node in observersSet)
            {
                row += node.Sample();
            }
            if (row.EndsWith(separator))
                row = row.Remove(row.Length - 1, 1);

            row += "\n";

            byte[] info = new UTF8Encoding(true).GetBytes(row);
            fs?.Write(info, 0, info.Length);

            yield return waitTime;
        }
    }

    void OnApplicationQuit()
    {
        StopMainLoop();
    }
}
