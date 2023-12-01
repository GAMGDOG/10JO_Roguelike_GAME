using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //private GameObject[] StageGrid;
    private Canvas StageUI;
    private Canvas curUI;
    //private GameObject curStageGrid;

    private Stage currentStage = Stage.Stage1;

    private void Awake()
    {
        //StageGrid = Resources.LoadAll<GameObject>("UI\\Stage");
        StageUI = Resources.Load<Canvas>("UI\\StageUI");
    }

    private void Start()
    {
        curUI = Instantiate(StageUI);
        //curStageGrid = Instantiate(StageGrid[(int)currentStage]);

        StartCoroutine(ShowStageName());
    }

    private void Update()
    {
        if(currentStage != Stage.Start)
        {
            ShowData();
        }
    }
   

    private void ShowData()
    {
        ChangeStageName();
        ShowTime();
        ShowExp();
        ShowGold();
        ShowLevel();
    }

    public void ChangeStageName()
    {
        switch ((Stage)GameManager.stageCount)
        {
            case Stage.Stage1:
                curUI.GetComponentInChildren<TMP_Text>().text = "�˼�����";
                break;
            case Stage.Stage2:
                curUI.GetComponentInChildren<TMP_Text>().text = "��������";
                break;
            case Stage.Stage3:
                curUI.GetComponentInChildren<TMP_Text>().text = "�������";
                break;
            default: break;
        }
    }
    //�ð� ǥ��
    private void ShowTime()
    {
        int remainedtime = (int)(120f - GameManager.Instance.stageLapseTime);
        curUI.transform.Find("Timer").GetComponent<TMP_Text>().text = (remainedtime / 60).ToSafeString() + " : " + (remainedtime % 60).ToSafeString();
    }

    //���� ǥ��
    private void ShowLevel()
    {
        curUI.GetComponentInChildren<Slider>().transform.Find("Level").GetComponent<TMP_Text>().text = "LV " + GameManager.Instance.player.level.ToString();
    }

    //����ġ ǥ��, �ִ� ����ġ �ʿ���. �ϴ� 10���� ����.
    private void ShowExp()
    {
        curUI.GetComponentInChildren<Slider>().value = GameManager.Instance.player.exp / 10;
    }

    //��� ǥ��
    private void ShowGold()
    {
        GameObject gold = curUI.transform.Find("Gold").gameObject;
        if (gold != null)
        {
            TMP_Text GoldText = gold.GetComponentInChildren<TMP_Text>();
            GoldText.text = GameManager.Instance.player.money.ToString();
        }
        else
        {
            Debug.Log("Gold is null");
        }

    }

    //�������� ������ ����â ���
    private void SelectItem()
    {
        Time.timeScale = 0;
    }

    private IEnumerator ShowStageName()
    {
        if (curUI != null)
        {
            TMP_Text StageName = curUI.GetComponentInChildren<TMP_Text>();
            if (StageName != null)
            {
                yield return new WaitForSecondsRealtime(2f);

                StageName.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Text ��ã��");
            }
        }
        else
        {
            Debug.LogError("curUI is null.");
        }
    }
}

public enum Stage
{
    Start,
    Stage1,
    Stage2,
    Stage3,
}
