using UnityEngine;

public class UPMRecordTransform : UPMRecordNode
{
    [SerializeField]
    protected bool exportPosition = true;
    [SerializeField]
    protected bool exportLocalPosition = false;
    [SerializeField]
    protected bool exportQuaternion = true;
    [SerializeField]
    protected bool exportLocalQuaternion = false;
    [SerializeField]
    protected bool exportEulerRotation = true;
    [SerializeField]
    protected bool exportLocalEulerRotation = false;
    [SerializeField]
    protected bool exportLocalScale = false;

    public override string Header()
    {
        string objName = gameObject.name;
        string sep = recordManager.Separator;
        string built = "";
        if (exportPosition)
            built += objName + "_X" + sep + objName + "_Y" + sep + objName + "_Z" + sep;
        if (exportLocalPosition)
            built += objName + "_LocalX" + sep + objName + "_LocalY" + sep + objName + "_LocalZ" + sep;
        if (exportQuaternion)
            built += objName + "_QuatW" + sep + objName + "_QuatX" + sep + objName + "_QuatY" + sep + objName + "_QuatZ" + sep;
        if (exportLocalQuaternion)
            built += objName + "_LocalQuatW" + sep + objName + "_LocalQuatX" + sep + objName + "_LocalQuatY" + sep + objName + "_LocalQuatZ" + sep;
        if (exportEulerRotation)
            built += objName + "_EulerX" + sep + objName + "_EulerY" + sep + objName + "_EulerZ" + sep;
        if (exportLocalEulerRotation)
            built += objName + "_LocalEulerX" + sep + objName + "_LocalEulerY" + sep + objName + "_LocalEulerZ" + sep;
        if (exportLocalScale)
            built += objName + "_LocalScaleX" + sep + objName + "_LocalScaleY" + sep + objName + "_LocalScaleZ" + sep;

        return built;
    }

    public override string Sample()
    {
        return ToString();
    }

    public override string ToString()
    {
        string sep = recordManager.Separator;
        string built = "";

        if (exportPosition)
            built += transform.position.x + sep + transform.position.y + sep + transform.position.z + sep;
        if (exportLocalPosition)
            built += transform.localPosition.x + sep + transform.localPosition.y + sep + transform.localPosition.z + sep;
        if (exportQuaternion)
            built += transform.rotation.w + sep + transform.rotation.x + sep + transform.rotation.y + sep + transform.rotation.z + sep;
        if (exportLocalQuaternion)
            built += transform.localRotation.w + sep + transform.localRotation.x + sep + transform.localRotation.y + sep + transform.localRotation.z + sep;
        if (exportEulerRotation)
            built += transform.eulerAngles.x + sep + transform.eulerAngles.y + sep + transform.eulerAngles.z + sep;
        if (exportLocalEulerRotation)
            built += transform.localEulerAngles.x + sep + transform.localEulerAngles.y + sep + transform.localEulerAngles.z + sep;
        if (exportLocalScale)
            built += transform.localScale.x + sep + transform.localScale.y + sep + transform.localScale.z + sep;

        return built;
    }
}
