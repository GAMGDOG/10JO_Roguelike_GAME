using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageSceneUI : MonoBehaviour
{
    Stage currentStage;
    private Canvas curUI;
    public void ShowData()
    {
        ChangeStageName();
        ShowTime();
        ShowExp();
        ShowGold();
        ShowLevel();
    }
    public void ChangeStageName()
    {
        switch (currentStage)   //GameManager�� ���� Static�̶� ���� �Ұ�..
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
}
