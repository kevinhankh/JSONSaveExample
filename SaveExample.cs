using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*  
 *  Author: Kevin Han from Pondering Pixel
 *  Date: May 2023
 *  
 *  Description: 
 *  An example of saving/loading game data to a JSON file.
 *  
 *  Instructions:
 *  - Create an empty game object and attach this script to it.
 *  - Open the console tab to view saving system in action.
 *  - Press play in the Unity editor to begin. NewGame() will be automatically called to set initial data.
 *  - Press the 'S' key to save data to a Json file.
 *  - Press the 'L' key to load data from a Json file.
 *  - Press the 'D' key to delete the Json file.
 *  - Press the 'C' key to change the player data values.
 *  - Press the 'N' key to reset the player data to initial values.
 *  - Try changing the player data, then save, then load to see the content of the Json file change.
 *  - Saving the data will show the location of the Json file. Open the file using a text editor like Notepad or a web browser to view the data.
 *  - You can also try to change the data, save, and stop the Unity editor. Press play again and load the data to see that it loads the previous run of the game.
 */
public class PlayerData
{
    public int health;
    public int gold;
    public Vector3 position;
}

public class SaveExample : MonoBehaviour
{
    PlayerData playerData;
    string saveFilePath;

    void Start()
    {
        playerData = new PlayerData();
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
        NewGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();

        if (Input.GetKeyDown(KeyCode.D))
            DeleteSaveFile();

        if (Input.GetKeyDown(KeyCode.N))
            NewGame();

        if (Input.GetKeyDown(KeyCode.C))
            ChangeData();
    }

    public void SaveGame()
    {
        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

            Debug.Log("Load game complete! \nPlayer health: " + playerData.health + ", Player gold: " + playerData.gold + ", Player Position: (x = " + playerData.position.x + ", y = " + playerData.position.y + ", z = " + playerData.position.z + ")");
        }
        else
            Debug.Log("There is no save files to load!");
            
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }

    public void NewGame()
    {
        playerData.health = 100;
        playerData.gold = 5;
        playerData.position = Vector3.zero;

        Debug.Log("New game! \nPlayer health: " + playerData.health + ", Player gold: " + playerData.gold + ", Player Position: (x = " + playerData.position.x + ", y = " + playerData.position.y + ", z = " + playerData.position.z + ")");
    }

    public void ChangeData()
    {
        playerData.health = 42;
        playerData.gold = 123;
        playerData.position = new Vector3(4, 5, 6);

        Debug.Log("Data has been updated! \nPlayer health: " + playerData.health + ", Player gold: " + playerData.gold + ", Player Position: (x = " + playerData.position.x + ", y = " + playerData.position.y + ", z = " + playerData.position.z + ")");
    }
}
