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
    //private bool isAlive = true;
    private GameObject square;

    // [������] �������� �� ���� �ؼ� ������ ������ ���������� �� �� �ְ� Count�ϴ� �뵵�� �ٲ���ϴ�.
    public int LvFlag;
    private GameObject itemSelectWindow;
    Dictionary<string, Item> myItems;
    private string[] itemNames = new string[] { "��", "��", "�ź���", "��", "�ҳ���", "��", "�η��", "�罿", "�ҷ���", "��" };

    #region LifeCycle
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

        // [������] GameManager���� �̺�Ʈ ��� ...
        if (GameManager.Instance)
            GameManager.Instance.OnStageFail.AddListener(ShowGameOver);
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
            itemSelectWindow = curUI.transform.Find("SelectItem").gameObject;
            myItems = (Dictionary<string, Item>)GameManager.Instance.player.GetComponent<ItemManager>().ItemDict;
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

            if (LvFlag > 0 && !itemSelectWindow.activeSelf)
            {
                Debug.Log($"open select window, LvFlag: {LvFlag}");
                SelectItem();
            }

            if (currentStage == 2)
            {
                square.transform.Rotate(Vector3.back, 10f * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.player.isDead)
            {
                GameManager.stageCount = 0;
                SceneManager.LoadScene("GameStartScene");
            }
        }
    }
    #endregion

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
        curUI.GetComponentInChildren<Slider>().value = GameManager.Instance.player.currentExp;
        curUI.GetComponentInChildren<Slider>().maxValue = GameManager.Instance.player.maxExp;
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
        foreach (var i in myItems)
        {
            if (i.Value.Type == Define.EItemType.ElixirHerbs || i.Value.Type == Define.EItemType.Mountine)
                continue;

            if (i.Value.Lv > 0)
            {
                Image nitem = item.transform.Find($"Item{(int)i.Value.Type + 1}")?.GetComponent<Image>();
                nitem.transform.Find("ItemImage").gameObject.SetActive(true);
            }
            else
            {
                Image nitem = item.transform.Find($"Item{(int)i.Value.Type + 1}")?.GetComponent<Image>();
                nitem.transform.Find("ItemImage").gameObject.SetActive(false);
            }
        }
    }

    //�������� ������ ����â ���
    private void SelectItem()
    {
        Time.timeScale = 0;
        itemSelectWindow.SetActive(true);

        //���׷��̵� ������ ������ �迭 ��������
        Define.EItemType[] upitems = GameManager.Instance.player.GetComponent<ItemManager>().GetUpgradableItems();

        //���� ������ 3�� ����
        System.Random random = new System.Random();
        upitems = upitems.OrderBy(x => random.Next()).Take(3).ToArray();

        for (int i = 0; i < upitems.Length; i++)
        {
            //������ �̹��� ����
            Image selectItem = itemSelectWindow.transform.Find($"Item{i + 1}Border").GetComponent<Image>();
            var obj = selectItem.transform.Find("ItemImage").gameObject.GetComponent<Image>();
            if (obj != null)
            {
                obj.sprite = Resources.Load<Sprite>($"UI\\Item\\Item{(int)upitems[i] + 1}");
            }

            //������ �̸� ����
            TMP_Text itemName = selectItem.transform.Find("ItemText").GetComponent<TMP_Text>();
            itemName.text = itemNames[(int)upitems[i]];

            //���׷��̵� ȿ�� ���� ����
            TMP_Text description = selectItem.transform.Find("Description").GetComponent<TMP_Text>();
            foreach(var item in myItems)
            {
                if(item.Value.Type == upitems[i])
                {
                    string[] _comments = item.Value.Comments;   //null
                    description.text = _comments[item.Value.Lv];
                }
            }

            Button selectButton = itemSelectWindow.transform.Find($"Item{i + 1}Border").GetComponent<Button>();
            int index = i;
            // [������] â�� ���� ������ �̺�Ʈ�� ��ϵż�, ������ ��ϵ� ������ Remove�ϵ��� ����
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => ChoiceItem(upitems[index]));
        }
    }

    //���ý� ������ ����, â ����
    private void ChoiceItem(Define.EItemType item)
    {
        Time.timeScale = 1;
        LvFlag--;
        itemSelectWindow.SetActive(false);
        curUI.transform.Find("SelectItem").gameObject.SetActive(false);

        //������ ����
        GameManager.Instance.player.GetComponent<ItemManager>().AddOrUpgradeItem(item);

        ////UI���� ����
        //if (!myItems.Contains(item) && (int)item < 8)
        //{
        //    myItems.Add(item);
        //}
    }

    //���ӿ��� �ǳ�
    private void ShowGameOver()
    {
        GameObject gameover = curUI.transform.Find("GameOver").gameObject;
        gameover.SetActive(true);
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

