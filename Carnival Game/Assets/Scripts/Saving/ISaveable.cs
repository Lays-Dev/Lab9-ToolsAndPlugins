// from the slides
public interface ISaveable
{
    string SaveID { get; }
    TransformSaveData SavedData { get; }
    void LoadFromData(TransformSaveData data);
}
