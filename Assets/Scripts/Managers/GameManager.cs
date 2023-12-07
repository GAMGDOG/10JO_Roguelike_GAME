using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;
    public static int stageCount = 0;
    private static GameObject nextStagePrefab = null;

    public float stageLapseTime;
    public float bossZenTime = 20f;
    public bool bossZen = false;

    /// <summary>
    /// player ��ü ���� <br/>
    /// </summary>
    public Player player;

    /// <summary>
    /// stage�� ������ monster���� ���� ����Ʈ <br/>
    /// </summary>
    public List<Monster> monsters;

    /// <summary>
    /// stage�� ������ item���� ���� ����Ʈ <br/>
    /// </summary>
    public List<DroppableItem> items;

    public PoolManager poolManager;
    public UIManager uiManager;

    [Header("Events")]
    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;
    public UnityEvent OnStageClear;
    public UnityEvent OnStageFail;
    public UnityEvent NextSceneDataSaveHandler;

    [Header("Prefabs")]
    public GameObject poolManagerPrefab;
    public GameObject UIManagerPrefab;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Data Manager�� ���� ��� ������ �ʿ� ���� ��?
            }
            return instance;
        }
    }

    private GameManager() { }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initialize();
        }
    }

    private void Initialize()
    {
        Debug.Log($"{nameof(GameManager)} call {nameof(Initialize)}.");
        OnGameStart.AddListener(LoadStage);
        OnStageClear.AddListener(LootAllItems);
        OnStageClear.AddListener(() => { StartCoroutine(WaitNextStage()); });
        OnStageFail.AddListener(() => { Time.timeScale = 0; });
        NextSceneDataSaveHandler.AddListener(SavePlayerData);
    }

    protected virtual void Start()
    {
        Time.timeScale = 1f;
        OnGameStart?.Invoke();
    }

    protected virtual void Update()
    {
        stageLapseTime += Time.deltaTime;

        // Debug Code
        if (Input.GetKeyDown(KeyCode.Space))
            GameOver(true);
    }

    public virtual void GameOver(bool isGameClear = false)
    {
        //Time.timeScale = 0f;
        OnGameOver?.Invoke();
        if (isGameClear) OnStageClear?.Invoke();
        else OnStageFail?.Invoke();
    }

    void PlayerInstatiate()
    {
        if (!player)
        {
            var obj = Resources.Load<GameObject>("Player");
            player = Instantiate(obj).GetComponent<Player>();

            player.level = DataManager.Instance.playerData.level;
            player.maxExp = DataManager.Instance.playerData.maxExp;
            player.currentExp = DataManager.Instance.playerData.currentExp;
            player.money = DataManager.Instance.playerData.money;
            player.hp = DataManager.Instance.playerData.currentHp;
            player.maxHp = DataManager.Instance.playerData.maxHp;
            player.atk = DataManager.Instance.playerData.atk;
            player.speed = DataManager.Instance.playerData.speed;
        }
    }

    void StageInstantiate()
    {
        if (nextStagePrefab != null)
            Instantiate(nextStagePrefab);
    }

    private void LoadStage()
    {
        StageInstantiate();
        PlayerInstatiate();
        poolManager = Instantiate(poolManagerPrefab).GetComponent<PoolManager>();
        uiManager = Instantiate(UIManagerPrefab).GetComponent<UIManager>();
        Resources.UnloadUnusedAssets();
        bossZen = false;
    }

    IEnumerator WaitNextStage()
    {
        yield return new WaitForSeconds(3f);
        //[��ȣ] �ٷ� �Ѿ�� �ʰ� �ƽ����� �̵��ϰ� �߽��ϴ�.
        //ToNextStage();
        instance?.NextSceneDataSaveHandler?.Invoke();
        SceneManager.LoadScene("StoryScene");
    }

    public static void ToNextStage()
    {
        stageCount++;
        //instance?.SaveNextData?.Invoke();
        string path = "Prefab/Stage/" + $"Stage{stageCount}Grid";
        nextStagePrefab = Resources.Load<GameObject>(path);
        if (nextStagePrefab != null)
            SceneManager.LoadScene("StageScene");
        else
        {
            stageCount = 0;
            SceneManager.LoadScene("GameStartScene");
        }
    }

    public void SavePlayerData()
    {
        DataManager.Instance.playerData.level = player.level;
        DataManager.Instance.playerData.maxExp = player.maxExp;
        DataManager.Instance.playerData.currentExp = player.currentExp;
        DataManager.Instance.playerData.money = player.money;
        DataManager.Instance.playerData.currentHp = player.hp;
        DataManager.Instance.playerData.speed = player.speed;
        DataManager.Instance.playerData.atk = player.atk;
    }

    public void LootAllItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
                items[i].AutoLooting();
        }
    }
}