using UnityEngine;

public abstract class UPMRecordNode : MonoBehaviour
{
    protected UPMRecordManager recordManager = null;

    // Start register to the Subject this observer.
    protected virtual void OnEnable()
    {
        if (recordManager == null)
            recordManager = UPMRecordManager.GetInstance();
        recordManager.AddDatasetNode(this);
    }

    public abstract string Header();

    public abstract string Sample();

    protected virtual void OnDisable()
    {
        recordManager.RemoveDatasetNode(this);
    }
}
