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

    /// <summary>
    /// player ��ü ���� <br/>
    /// ���Ŀ� �ڷ����� �÷��̾� Ŭ������ �ٲ����.
    /// </summary>
    public Player player;

    /// <summary>
    /// stage�� ������ monster���� ���� ����Ʈ <br/>
    /// ���Ŀ� �ڷ����� ���͵��� �ֻ��� Ŭ������ �ٲ����.
    /// </summary>
    public List<Monster> monsters;

    /// <summary>
    /// stage�� ������ item���� ���� ����Ʈ <br/>
    /// ���Ŀ� �ڷ����� droppable item���� ���� Ŭ������ �ٲ����.
    /// </summary>
    public List<DroppableItem> items;

    public PoolManager poolManager;

    [Header("Events")]
    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;
    public UnityEvent OnStageClear;
    public UnityEvent OnStageFail;

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
        OnStageClear.AddListener(ToNextStage);
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
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
        if (isGameClear) OnStageClear?.Invoke();
        else OnStageFail?.Invoke();
    }

    void PlayerInstatiate()
    {
        var obj = Resources.Load<GameObject>("Player");
        player = Instantiate(obj).GetComponent<Player>();
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
        Instantiate(UIManagerPrefab);
        Resources.UnloadUnusedAssets();
    }

    public static void ToNextStage()
    {
        stageCount++;

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
}