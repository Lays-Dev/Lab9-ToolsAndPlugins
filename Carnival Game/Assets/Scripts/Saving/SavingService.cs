using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// score goes in a binary file, locations go in a JSON file
public static class SavingService
{
    private const string SCORE_FILE = "score.dat";
    private const string LOCATION_FILE = "locations.json";

    public static string PathForFilename(string filename)
    {
        var folderToStoreFilesIn = Application.persistentDataPath;
        var path = System.IO.Path.Combine(folderToStoreFilesIn, filename);
        return path;
    }

    public static void SaveGame()
    {
        SaveScore();
        SaveLocations();
    }

    public static void LoadGame()
    {
        LoadScore();
        LoadLocations();
    }

    static void SaveScore()
    {
        ScoreSave scoreSave = new ScoreSave();
        scoreSave.score = 0;

        if (ScoreSubject.Instance != null)
            scoreSave.score = ScoreSubject.Instance.score;

        // from slide binary example
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        formatter.Serialize(stream, scoreSave);
        byte[] binaryData = stream.ToArray();

        var outputPath = PathForFilename(SCORE_FILE);
        File.WriteAllBytes(outputPath, binaryData);
        Debug.LogFormat("Wrote saved score to {0}", outputPath);
    }

    static void LoadScore()
    {
        var dataPath = PathForFilename(SCORE_FILE);

        if (File.Exists(dataPath) == false)
        {
            Debug.LogErrorFormat("No file exists at {0}", dataPath);
            return;
        }

        byte[] binaryData = File.ReadAllBytes(dataPath);
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream(binaryData);
        ScoreSave scoreSave = (ScoreSave)formatter.Deserialize(stream);

        if (ScoreSubject.Instance != null)
            ScoreSubject.Instance.LoadScore(scoreSave.score);
    }

    static void SaveLocations()
    {
        var allSaveableObjects = Object
            .FindObjectsOfType<MonoBehaviour>(true)
            .OfType<ISaveable>();

        List<TransformSaveData> savedObjects = new List<TransformSaveData>();

        foreach (var saveableObject in allSaveableObjects)
            savedObjects.Add(saveableObject.SavedData);

        LocationSaveFile file = new LocationSaveFile();
        file.objects = savedObjects.ToArray();

        var outputPath = PathForFilename(LOCATION_FILE);

        // from cheat sheet JSON files
        using (StreamWriter writer = File.CreateText(outputPath))
        {
            writer.WriteLine(JsonUtility.ToJson(file, true));
        }

        Debug.LogFormat("Wrote saved game to {0}", outputPath);
    }

    static void LoadLocations()
    {
        var dataPath = PathForFilename(LOCATION_FILE);

        if (File.Exists(dataPath) == false)
        {
            Debug.LogErrorFormat("No file exists at {0}", dataPath);
            return;
        }

        var text = File.ReadAllText(dataPath);
        LocationSaveFile file = JsonUtility.FromJson<LocationSaveFile>(text);

        if (file == null || file.objects == null)
        {
            Debug.LogErrorFormat("Data at {0} is not a JSON object", dataPath);
            return;
        }

        // match each saved id to the object that has that id
        var allLoadableObjects = Object
            .FindObjectsOfType<MonoBehaviour>(true)
            .OfType<ISaveable>()
            .ToDictionary(o => o.SaveID, o => o);

        for (int i = 0; i < file.objects.Length; i++)
        {
            TransformSaveData objectData = file.objects[i];

            if (allLoadableObjects.ContainsKey(objectData.saveID))
            {
                var loadableObject = allLoadableObjects[objectData.saveID];
                loadableObject.LoadFromData(objectData);
            }
        }
    }
}

// object the binary formatter can write
[System.Serializable]
public class ScoreSave
{
    public int score;
}
