using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<Define.EItemType> myItems = new List<Define.EItemType>() { Define.EItemType.Stone };
    private bool isAlive = true;
    private GameObject square;
    private string[] itemNames = new string[] { "��", "��", "�ź���", "��", "�ҳ���", "��", "�η��", "�罿", "�ҷ���", "��" };
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
            if (currentStage == 2)
            {
                square = Instantiate((GameObject)Resources.Load("UI\\Square"));
            }
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

            if (currentStage == 2)
            {
                square.transform.Rotate(Vector3.back, 10f * Time.deltaTime);
            }

            if (GameManager.Instance.player.hp <= 0 && isAlive == true)
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

    #region StartScene
    private void ShowStartData()
    {
        ShowGold();
    }
    #endregion

    #region UpgradeScene
    private void ShowUpgradeData()
    {
        ShowGold();
        ShowUpgradeLevel();
    }

    //���׷��̵� ���� �����̴� ����
    private void ShowUpgradeLevel()
    {
        Slider[] upgrade = curUI.GetComponentsInChildren<Slider>();

        for (int i = 0; i < upgrade.Length; i++)
        {
            if (upgrade[i] != null)
            {
                upgrade[i].value = DataManager.Instance.playerData.upgradeLevel[i];
            }
            else
            {
                Debug.Log("is Null");
            }

            TMP_Text[] info = upgrade[i].GetComponentsInChildren<TMP_Text>();
            info[0].text = "LV." + DataManager.Instance.playerData.upgradeLevel[i].ToString();
            info[2].text = ((DataManager.Instance.playerData.upgradeLevel[i] + 1) * 100).ToString();
        }


    }
    #endregion

    #region StageScene
    private void ShowStageData()
    {
        ChangeStageName();
        ShowTime();
        ShowExp();
        ShowGold();
        ShowLevel();
        ShowItem();
    }

    //�������� �̸� ���
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

    //�ð� ǥ��
    private void ShowTime()
    {
        //2���� �������� �پ�鵵�� ����
        int remainedtime = (int)(120f - GameManager.Instance.stageLapseTime);
        remainedtime = remainedtime <= 0 ? 0 : remainedtime;
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
        curUI.GetComponentInChildren<Slider>().value = GameManager.Instance.player.exp;
        curUI.GetComponentInChildren<Slider>().maxValue = GameManager.Instance.player.level * 5;
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
        foreach (Define.EItemType i in myItems)
        {
            Image nitem = item.transform.Find($"Item{(int)i + 1}").GetComponent<Image>();
            nitem.transform.Find("ItemImage").gameObject.SetActive(true);
        }
    }

    //�������� ������ ����â ���
    private void SelectItem()
    {
        Time.timeScale = 0;
        curUI.transform.Find("SelectItem").gameObject.SetActive(true);

        //���׷��̵� ������ ������ �迭 ��������
        Define.EItemType[] upitems = GameManager.Instance.player.GetComponent<ItemManager>().GetUpgradableItems();

        //���� ������ 3�� ����
        System.Random random = new System.Random();
        upitems = upitems.OrderBy(x => random.Next()).Take(3).ToArray();

        var select = curUI.transform.Find("SelectItem").gameObject;
        for (int i = 0; i < upitems.Length; i++)
        {
            //������ �̹��� ����
            Image selectItem = select.transform.Find($"Item{i + 1}Border").GetComponent<Image>();
            var obj = selectItem.transform.Find("ItemImage").gameObject.GetComponent<Image>();
            if (obj != null)
            {
                obj.sprite = Resources.Load<Sprite>($"UI\\Item\\Item{(int)upitems[i] + 1}");
            }

            //������ �̸� ����
            TMP_Text itemName = selectItem.transform.Find("ItemText").GetComponent<TMP_Text>();
            itemName.text = itemNames[(int)upitems[i]];

            //������ ������ ����
            Button selectButton = select.transform.Find($"Item{i + 1}Border").GetComponent<Button>();
            int index = i;
            selectButton.onClick.AddListener(() => ChoiceItem(upitems[index]));
        }
    }

    //���ý� ������ ����, â ����
    private void ChoiceItem(Define.EItemType item)
    {
        Time.timeScale = 1;
        curUI.transform.Find("SelectItem").gameObject.SetActive(false);
        //������ ����
        GameManager.Instance.player.GetComponent<ItemManager>().AddOrUpgradeItem(item);
        if (!myItems.Contains(item) && (int)item < 8)
        {
            myItems.Add(item);
        }

    }

    //���ӿ��� �ǳ�
    private void ShowGameOver()
    {
        Time.timeScale = 0;
        GameObject gameover = curUI.transform.Find("GameOver").gameObject;

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
    #endregion
}

