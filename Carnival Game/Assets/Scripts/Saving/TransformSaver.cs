using UnityEngine;

// saves and loads one transform. attach to player and each target
public class TransformSaver : MonoBehaviour, ISaveable
{
    [SerializeField] private string saveID;

    public string SaveID
    {
        get { return saveID; }
        set { saveID = value; }
    }

    public TransformSaveData SavedData
    {
        get
        {
            TransformSaveData result = new TransformSaveData();
            result.saveID = saveID;
            result.localPosition = transform.localPosition;
            result.localRotation = transform.localRotation;
            result.localScale = transform.localScale;
            result.active = gameObject.activeSelf;
            return result;
        }
    }

    public void LoadFromData(TransformSaveData data)
    {
        transform.localPosition = data.localPosition;
        transform.localRotation = data.localRotation;
        transform.localScale = data.localScale;
        gameObject.SetActive(data.active);
    }
}
