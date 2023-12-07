using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public PlayerData playerData;
    Dictionary<string, Item> _items;
    public IDictionary ItemDict
    {
        get => _items;
        set
        {
            _items = (Dictionary<string, Item>)value;
            //_items = new Dictionary<string, int>();
            //Dictionary<string, Item> dict = value == null ? null : (Dictionary<string, Item>)value;
            //if (dict != null)
            //{
            //    foreach (var e in dict)
            //    {
            //        _items.Add(e.Key, e.Value.Property);
            //    }
            //}
        }
    }

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject(nameof(DataManager));
                instance = obj.AddComponent<DataManager>();
                instance.Initialize();
            }
            return instance;
        }
    }

    private DataManager() { }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Initialize();
        }
    }

    private void Start()
    {
        if (instance != this)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            Debug.Log($"{name} call {nameof(OnDestroy)}");
            SaveData();
        }
    }

    private void Initialize()
    {
        // ���� �������� �ʱ�ȭ
        if (!LoadData())
        {
            // �ҷ��� �� ���ٸ� �⺻������ ����
            Debug.Log($"{name} call {nameof(LoadData)} is fail. redirect {nameof(CreateDefaultData)}...");
            CreateDefaultData();
        }

        DontDestroyOnLoad(gameObject);
    }

    public bool SaveData()
    {
        try
        {
            // Save data..
            var playerDataJson = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString(nameof(playerData), playerDataJson);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        Debug.Log($"{name} call {nameof(SaveData)} is success");
        return true;
    }

    public bool LoadData()
    {
        // Set default data..
        playerData = new();
        // Load data..
        var playerDataJson = PlayerPrefs.GetString(nameof(playerData));
        var loadedPlayerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
        if (loadedPlayerData != null)
            playerData = loadedPlayerData;
        Debug.Log($"{name} call {nameof(LoadData)} is success");
        return true;
    }

    public void CreateDefaultData()
    {
        // ��� �����͸� �⺻������ ����...
        playerData = new();
    }
}

/// <summary>
/// �÷��̾� ĳ���Ϳ� ���õ� ���� �߿� ������ �ʿ��� �� ������ ���⿡ �߰����ֽø� �˴ϴ�.
/// </summary>
[System.Serializable]
public class PlayerData
{
    public float maxHp;
    public float currentHp;
    public float atk;
    public float speed;
    public int level;
    public int maxExp;
    public int currentExp;
    public int money;
    public int[] upgradeLevel = new int[3] { 0, 0, 0 };


    public PlayerData()
    {
        maxHp = 100;
        currentHp = 100;
        level = 1;
        atk = 10;
        speed = 1;
        maxExp = 10;
        currentExp = 0;
        money = 0;
    }

    public void SetDefaultInStageData()
    {
        maxHp = 100 + StatUpgrade.upgradeModifiers[0] * upgradeLevel[0];
        currentHp = maxHp;
        level = 1;
        maxExp = 10;
        currentExp = 0;
        atk = 10 + StatUpgrade.upgradeModifiers[1] * upgradeLevel[1];
        speed = 1 + StatUpgrade.upgradeModifiers[2] * upgradeLevel[2];
    }
}