using System;
using UnityEngine;

// object's saved location
[Serializable]
public class TransformSaveData
{
    public string saveID;
    public Vector3 localPosition;
    public Quaternion localRotation;
    public Vector3 localScale;
    public bool active;
}

// for JsonUtility
[Serializable]
public class LocationSaveFile
{
    public TransformSaveData[] objects;
}
