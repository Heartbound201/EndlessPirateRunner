using System.IO;
using UnityEngine;

public static class FileManager
{
    private static readonly string SAVE_FILENAME = "/save.dat";
    public static void WriteToFile(SaveData saveData)
    {
        File.WriteAllText(Application.persistentDataPath + SAVE_FILENAME, saveData.ToJson());
    }

    public static SaveData ReadFromFile()
    {
        SaveData save = new SaveData();
        try
        {
            string json = File.ReadAllText(Application.persistentDataPath + SAVE_FILENAME);
            save.FromJson(json);
        }
        catch (FileNotFoundException fnf)
        {
            Debug.LogWarning("no save file found");
        }

        return save;
    }
}