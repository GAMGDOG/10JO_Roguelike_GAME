using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIManager : MonoBehaviour
{
    private Canvas CurrentUI;
    private Canvas curUI;
    public static bool isUpgrade = false;
    private int currentStage;
    private int LvFlag;
    private bool isAlive = true;
    private string[] itemNames = new string[] { " ", "��", "�罿", "��", "��", "�η��", "�ҳ���", "��", "�ź���", "�ҷ���", "��" };
    private void Awake()
    {
        currentStage = GameManager.stageCount;
        if (currentStage == 0)
        {
            if (isUpgrade)
            {
                CurrentUI = Resources.Load<Canvas>("UI\\UpgradeUI");
            }
            else
            {
                CurrentUI = Resources.Load<Canvas>("UI\\StartSceneUI");
            }
        }
        else
        {
            CurrentUI = Resources.Load<Canvas>("UI\\StageUI");
        }
    }

    private void Start()
    {
        curUI = Instantiate(CurrentUI);
        if (currentStage == 0)
        {

        }
        else
        {
            StartCoroutine(ShowStageName());
        }
        if (currentStage > 0)
        {
            LvFlag = GameManager.Instance.player.level;
        }
    }

    private void Update()
    {
        if (currentStage == 0)
        {
            if (isUpgrade)
            {
                ShowUpgradeData();
            }
            else
            {
                ShowStartData();
            }
        }
        else
        {
            ShowStageData();

            if (LvFlag != GameManager.Instance.player.level)
            {
                LvFlag = GameManager.Instance.player.level;
                SelectItem();
            }

            if (GameManager.Instance.player.hp <= 0&&isAlive == true)
            {
                isAlive = false;
                ShowGameOver();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && isAlive == false)
            {
                GameManager.stageCount = 0;
                SceneManager.LoadScene("GameStartScene");
            }
        }
    }

    private void ShowStageData()
    {
        ChangeStageName();
        ShowTime();
        ShowExp();
        ShowGold();
        ShowLevel();
        ShowItem();
    }

    private void ShowStartData()
    {
        ShowGold();
    }

    private void ShowUpgradeData()
    {
        ShowGold();
        ShowUpgradeLevel();
    }

    public void ChangeStageName()
    {
        switch (GameManager.stageCount)
        {
            case 1:
                curUI.GetComponentInChildren<TMP_Text>().text = "�˼�����";
                break;
            case 2:
                curUI.GetComponentInChildren<TMP_Text>().text = "��������";
                break;
            case 3:
                curUI.GetComponentInChildren<TMP_Text>().text = "�������";
                break;
            default: break;
        }
    }

    //���׷��̵� ���� �����̴� ����
    private void ShowUpgradeLevel()
    {
        //�����͸Ŵ������� ��������� �߰� ����.
        //curUI.GetComponentsInChildren<Slider>()[0].value = DataManager.Instance.playerData.UpgradeLv[0];
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
            GoldText.text = DataManager.Instance.playerData.money.ToString();
            //GoldText.text = GameManager.Instance.player.money.ToString();
        }
        else
        {
            Debug.Log("Gold is null");
        }
    }

    //������ �ִ� ������ �»�ܿ� ���
    private void ShowItem()
    {
        GameObject item = curUI.transform.Find("Items").gameObject;

        //foreach(Define.EItemType i in GameManager.Instance.items)
        //{
        //    Image nitem = item.transform.Find($"Item{(int)i}").GetComponent<Image>();
        //    nitem.transform.Find("ItemImage").gameObject.SetActive(true);
        //}

        for (int i = 1; i < 9; i++)
        {
            Image nitem = item.transform.Find($"Item{i}").GetComponent<Image>();
            var obj = nitem.transform.Find("ItemImage").gameObject;
            if (obj != null)
            {
                obj.SetActive(true);
            }
            else Debug.Log($"{i}��°���� ���� �߻�");
        }
    }

    //�������� ������ ����â ���
    private void SelectItem()
    {
        Time.timeScale = 0;
        curUI.transform.Find("SelectItem").gameObject.SetActive(true);

        //���� ������ 3�� ������ ���� ���� ����
        int[] randomItemNum = new int[3];
        int index = 0;
        while (index < 3)
        {
            int rnum = UnityEngine.Random.Range(1, 11);
            if (Array.IndexOf(randomItemNum, rnum) == -1)
            {
                randomItemNum[index] = rnum;
                index++;
            }
        }

        var select = curUI.transform.Find("SelectItem").gameObject;
        for (int i = 1; i < 4; i++)
        {
            //������ �̹��� ����
            Image selectItem = select.transform.Find($"Item{i}Border").GetComponent<Image>();
            var obj = selectItem.transform.Find("ItemImage").gameObject.GetComponent<Image>();
            if (obj != null)
            {
                obj.sprite = Resources.Load<Sprite>($"UI\\Item\\Item{randomItemNum[i - 1]}");
            }

            //������ �̸� ����
            TMP_Text itemName = selectItem.transform.Find("ItemText").GetComponent<TMP_Text>();
            itemName.text = itemNames[randomItemNum[i-1]];

            //�����ϸ� â ��������
            Button selectButton = select.transform.Find($"Item{i}Border").GetComponent<Button>();
            selectButton.onClick.AddListener(ChoiceItem);
        }
    }

    //���ý� ������ ����, â ����
    private void ChoiceItem()
    {
        Time.timeScale = 1;
        curUI.transform.Find("SelectItem").gameObject.SetActive(false);
    }

    private void ShowGameOver()
    {
        Time.timeScale = 0;
        curUI.transform.Find("GameOver").gameObject.SetActive(true);
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

