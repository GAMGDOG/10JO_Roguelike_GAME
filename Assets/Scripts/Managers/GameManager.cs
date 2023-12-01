using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;
    public static int stageCount;

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
    public List<GameObject> monsters;

    /// <summary>
    /// stage�� ������ item���� ���� ����Ʈ <br/>
    /// ���Ŀ� �ڷ����� dropable item���� ���� Ŭ������ �ٲ����.
    /// </summary>
    public List<GameObject> items;

    [Header("Events")]
    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;
    public UnityEvent OnStageClear;
    public UnityEvent OnStageFail;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Data Manager�� ���� ��� ������ �ʿ� ���� ��?
                throw new NullReferenceException();
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
        OnGameStart.AddListener(StageInstantiate);
        OnGameStart.AddListener(PlayerInstantiate);
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

    void StageInstantiate()
    {
        string path = "Prefab/Stage/" + $"Stage{stageCount:00}";
        var obj = Resources.Load<GameObject>(path);
        if (obj != null)
            Instantiate(obj);
        else
            SceneManager.LoadScene("GameStartScene");
    }

    void PlayerInstantiate()
    {
        var obj = Resources.Load("Player");
        player = Instantiate(obj).GetComponent<Player>();
    }

    void ToNextStage()
    {
        stageCount++;
        SceneManager.LoadScene("StageScene");
    }
}