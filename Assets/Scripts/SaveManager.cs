using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private SaveData _saveData;
    private string _path;
    public static SaveManager Singleton;

    private string _testPath;
    private SaveBlocks _saveBlocks;


    private void Awake()
    {
#if UNITY_EDITOR
        _path = Path.Combine(Application.dataPath, "save.json");
        _testPath = Path.Combine(Application.dataPath, "elseSave.json");
#else
        _path = Path.Combine(Application.persistentDataPath, "save.json");
        _testPath = Path.Combine(Application.persistentDataPath, "elseSave.json");

#endif

        _saveData = new SaveData();
        _saveBlocks = new SaveBlocks();

        LoadGameData();

        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    public void SaveGameData()
    {
        var json = JsonUtility.ToJson(_saveData);
        File.WriteAllText(_path, json);

        var testJson = JsonUtility.ToJson(_saveBlocks);
        File.WriteAllText(_testPath, testJson);
        Debug.Log(_saveBlocks.blockWheats);
    }

    public void SetCoins(int coins) => _saveData.coins = coins;
    public void SetPosition(Vector3 position) => _saveData.playerPosition = position;
    public int GetCoins() => _saveData.coins;
    public Vector3 GetPosition() => _saveData.playerPosition;

    public void SetBlocks(int block) => _saveBlocks.blockWheats = block;
    public int GetBlocksCount() => _saveBlocks.blockWheats;
   

    private void OnDestroy()
    {
        SaveGameData();
        
    }

    private void LoadGameData()
    {
        if (File.Exists(_path))
        {
            var loadDate = File.ReadAllText(_path);
            _saveData = JsonUtility.FromJson<SaveData>(loadDate);
        }

        if (File.Exists(_testPath))
        {
            var load = File.ReadAllText(_testPath);
            _saveBlocks = JsonUtility.FromJson<SaveBlocks>(load);
        }
    }
}
