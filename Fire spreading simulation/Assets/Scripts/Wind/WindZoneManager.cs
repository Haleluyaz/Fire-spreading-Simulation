using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneManager : Singleton<WindZoneManager>
{
    public Transform windZoneTrans;

    // Call in UI
    public void UpdateRotation()
    {
        windZoneTrans.localEulerAngles = new Vector3(0, UIDebug.Instance.windDirection.value, 0);
    }
}