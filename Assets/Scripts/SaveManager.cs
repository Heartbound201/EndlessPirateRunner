using System.Collections.Generic;

public class SaveManager : MonoSingleton<SaveManager>
{
    public List<PlayerShipPrototype> allPlayerShips = new List<PlayerShipPrototype>();
    
    public PlayerData playerData;
    
    public void Save()
    {
        SaveData saveData = new SaveData();
        
        saveData.gold = playerData.gold.Value;
        
        saveData.highscore = playerData.highscore;
        
        saveData.currentPlayerShip = allPlayerShips.IndexOf(playerData.currentShip);

        saveData.unlockedShips.Add(allPlayerShips.IndexOf(playerData.startingShip));
        foreach (PlayerShipPrototype shipPrototype in playerData.unlockedShips)
        {
            saveData.unlockedShips.Add(allPlayerShips.IndexOf(shipPrototype));
        }
        
        FileManager.WriteToFile(saveData);
    }

    public void Load()
    {
        SaveData saveData = FileManager.ReadFromFile();

        playerData.gold.Value = saveData.gold;

        playerData.currentShip = allPlayerShips[saveData.currentPlayerShip];
        
        playerData.unlockedShips.Clear();

        HashSet<PlayerShipPrototype> unlockedShips = new HashSet<PlayerShipPrototype>();
        unlockedShips.Add(playerData.startingShip);
        foreach (int index in saveData.unlockedShips)
        {
            unlockedShips.Add(allPlayerShips[index]);
        }
        playerData.unlockedShips = unlockedShips;
        
    }
}
