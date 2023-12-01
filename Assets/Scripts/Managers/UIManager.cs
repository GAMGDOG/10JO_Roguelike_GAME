using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Canvas CurrentUI;
    private Canvas curUI;
    //private GameObject curStageGrid;

    private Stage currentStage = Stage.Stage1;

    private void Awake()
    {
        
        if (currentStage == Stage.Start)
        {
            CurrentUI = Resources.Load<Canvas>("UI\\StartSceneUI");
        }
        else if (currentStage == Stage.Upgrade)
        {
            CurrentUI = Resources.Load<Canvas>("UI\\UpgradeUI");
        }
        else
        {
            CurrentUI = Resources.Load<Canvas>("UI\\StageUI");
        }
    }

    private void Start()
    {
        curUI = Instantiate(CurrentUI); 
        if (currentStage == Stage.Start)
        {

        }
        else if (currentStage == Stage.Upgrade)
        {
            
        }
        else
        {
            StartCoroutine(ShowStageName());
        }
    }

    private void Update()
    {
        if (currentStage != Stage.Start)
        {
            ShowStageData();
        }
        else if (currentStage == Stage.Start)
        {
            ShowStartData();
        }
    }

    private void ShowStageData()
    {
        ChangeStageName();
        ShowTime();
        ShowExp();
        ShowGold();
        ShowLevel();
    }

    private void ShowStartData()
    {
        ShowGold();
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
        curUI.GetComponentInChildren<Slider>().value = GameManager.Instance.player.exp / GameManager.Instance.player.level * 5;
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
        curUI.transform.Find("SelectItem").gameObject.SetActive(true);
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
    Start,
    Upgrade
}
