using System.IO;
using UnityEngine;

public class FileManager
{
    private static readonly string SAVE_FILENAME = "/save.dat";
    public static void WriteToFile(SaveState saveState)
    {
        File.WriteAllText(Application.persistentDataPath + SAVE_FILENAME, saveState.ToJson());
    }

    public static SaveState ReadFromFile()
    {
        SaveState save = new SaveState();
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