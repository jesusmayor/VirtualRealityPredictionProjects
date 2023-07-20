using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class UPMRecordXR : UPMRecordNode
{
    List<InputDevice> fetchedDevices = new List<InputDevice>();

    [SerializeField]
    protected bool exportForwardVector = true;
    [SerializeField]
    protected bool exportPosition = true;
    [SerializeField]
    protected bool exportRotation = true;
    [SerializeField]
    protected bool exportVelocity = true;
    [SerializeField]
    protected bool exportAcceleration = true;
    [SerializeField]
    protected bool exportAngularAcceleration = true;
    [SerializeField]
    protected bool exportAngularVelocity = true;
   

    protected override void OnEnable()
    {
        InputDevices.GetDevices(fetchedDevices);

        base.OnEnable();
    }

    public override string Header()
    {
        string built = "";
        
        foreach (InputDevice device in fetchedDevices)
        {
            built += (exportPosition) ? GetUsageOfThisDevice(CommonUsages.devicePosition, device, true) : "";
            built += (exportRotation) ? GetUsageOfThisDevice(CommonUsages.deviceRotation, device, true) : "";
            built += (exportForwardVector) ? GetForwardDevice(device, true) : "";
            built += (exportVelocity) ? GetUsageOfThisDevice(CommonUsages.deviceVelocity, device, true) : "";
            built += (exportAcceleration) ? GetUsageOfThisDevice(CommonUsages.deviceAcceleration, device, true) : "";
            built += (exportAngularAcceleration) ? GetUsageOfThisDevice(CommonUsages.deviceAngularAcceleration, device, true) : "";
            built += (exportAngularVelocity) ? GetUsageOfThisDevice(CommonUsages.deviceAngularVelocity, device, true) : "";
        }

        return built;
    }

    public override string Sample()
    {
        return ToString();
    }

    public override string ToString()
    {
        string built = "";

        foreach (InputDevice device in fetchedDevices)
        {
            built += (exportPosition) ? GetUsageOfThisDevice(CommonUsages.devicePosition, device) : "";
            built += (exportRotation) ? GetUsageOfThisDevice(CommonUsages.deviceRotation, device) : "";
            built += (exportForwardVector) ? GetForwardDevice(device) : "";
            built += (exportVelocity) ? GetUsageOfThisDevice(CommonUsages.deviceVelocity, device) : "";
            built += (exportAcceleration) ? GetUsageOfThisDevice(CommonUsages.deviceAcceleration, device) : "";
            built += (exportAngularAcceleration) ? GetUsageOfThisDevice(CommonUsages.deviceAngularAcceleration, device) : "";
            built += (exportAngularVelocity) ? GetUsageOfThisDevice(CommonUsages.deviceAngularVelocity, device) : "";
        }

        return built;
    }

    private string GetUsageOfThisDevice (InputFeatureUsage<Vector3> usage, InputDevice device, bool header = false)
    {
        string sep = recordManager.Separator;
        
        if (device.isValid && device.TryGetFeatureValue(usage, out Vector3 deviceFeature))
            if (header)
                return device.name + "_" + usage.name + "_x" + sep + device.name + "_" + usage.name + "_y" + sep + device.name + "_" + usage.name + "_z" + sep;
            else
                return deviceFeature.x + sep + deviceFeature.y + sep + deviceFeature.z + sep;
        else
        {
            Debug.LogWarning("Error Fetching value: " + device.name + "_" + usage.name);
            return " "+ sep + " " + sep + " " + sep;
        }
    }

    private string GetUsageOfThisDevice(InputFeatureUsage<Quaternion> usage, InputDevice device, bool header = false)
    {
        string sep = recordManager.Separator;

        if (device.isValid && device.TryGetFeatureValue(usage, out Quaternion deviceFeature))
            if (header)
                return device.name + "_" + usage.name + "_w" + sep + device.name + "_" + usage.name + "_x" + sep + device.name + "_" + usage.name + "_y" + sep + device.name + "_" + usage.name + "_z" + sep;
            else
                return deviceFeature.w + sep + deviceFeature.x + sep + deviceFeature.y + sep + deviceFeature.z + sep;
        else
        {
            Debug.LogWarning("Error Fetching value: " + device.name + "_" + usage.name);
            return " " + sep + " " + sep + " " + sep + " " + sep;
        }
    }

    private string GetForwardDevice(InputDevice device, bool header = false)
    {
        string sep = recordManager.Separator;

        if (device.isValid && device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion deviceFeature))
        {
            Vector3 forwardVector = deviceFeature * Vector3.forward;
            if (header)
                return device.name + "_Forward_x" + sep + device.name + "_Forward_y" + sep + device.name + "_Forward_z" + sep;
            else
                return forwardVector.x + sep + forwardVector.y + sep + forwardVector.z + sep;
        }
        else
        {
            Debug.LogWarning("Error Fetching value: " + device.name + "_Forward");
            return " " + sep + " " + sep + " " + sep;
        }
    }
}
