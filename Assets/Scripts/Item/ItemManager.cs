using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    Dictionary<string, Item> _items = new Dictionary<string, Item>();
    // [������] �� ��ȯ �� ���� ������ ���� ��ųʸ��� ���� ������ ������Ƽ �߰�
    public IDictionary ItemDict { get => _items; }

    Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();

        for(int i = 0; i < (int)Define.EItemType.Max; ++i)
        {
            var item = AddItem((Define.EItemType)i);
            item.Player = _player;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.NextSceneDataSaveHandler.AddListener(() => { DataManager.Instance.ItemDict = _items; });
        if (DataManager.Instance.ItemDict != null && DataManager.Instance.ItemDict.Count > 0)
        {
            Dictionary<string, Item> dict = (Dictionary<string, Item>)DataManager.Instance.ItemDict;
            foreach (var e in dict)
                _items[e.Key].SetProperty(e.Value.Property);
        }
        else
        {
            // Test Code
            // AddOrUpgradeItem(Define.EItemType.Stone);
        }
    }

    private void Update()
    {
        // Test Code
        GetUpgradeInput();        
    }

    private void GetUpgradeInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddOrUpgradeItem(Define.EItemType.Moon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddOrUpgradeItem(Define.EItemType.Turtle);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddOrUpgradeItem(Define.EItemType.Stone);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddOrUpgradeItem(Define.EItemType.Sun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddOrUpgradeItem(Define.EItemType.PineCone);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            AddOrUpgradeItem(Define.EItemType.Water);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            AddOrUpgradeItem(Define.EItemType.ElixirHerbs);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            AddOrUpgradeItem(Define.EItemType.Crane);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            AddOrUpgradeItem(Define.EItemType.Mountine);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            AddOrUpgradeItem(Define.EItemType.Deer);
        }

    }

    /// <summary>
    /// ������ Ÿ���� int ������ ����ؼ� Add Or Upgrade Item �Լ��� ȣ���մϴ�.
    /// </summary>
    /// <param name="type"></param>
    public void AddOrUpgradeItem(int type)
    {
        Define.EItemType itemType = (Define.EItemType)type;
        AddOrUpgradeItem(itemType);
    }

    /// <summary>
    /// �������� �߰��ϰų� ���׷��̵� �� �� ����մϴ�.
    /// </summary>
    /// <param name="itemType"></param>
    public void AddOrUpgradeItem(Define.EItemType itemType)
    {
        if(_items.ContainsKey(itemType.ToString()))
        {
            UpgradeItem(itemType);
        }
        else
        {
            AddItem(itemType);
        }
    }

    private Item AddItem(Define.EItemType itemType)
    {
        GameObject go_Item = new GameObject(itemType.ToString() + "Item");
        go_Item.transform.parent = transform;
        go_Item.transform.localPosition = Vector3.zero;
        Item item = null;
        
        switch(itemType)
        {
            case Define.EItemType.Moon:
                item = go_Item.AddComponent<MoonRotator>();
                break;

            case Define.EItemType.Turtle:
                item = go_Item.AddComponent<Turtle>();                
                break;

            case Define.EItemType.Stone:
                item = go_Item.AddComponent<StoneItem>();
                break;

            case Define.EItemType.Sun:
                item = go_Item.AddComponent<SunItem>();
                break;

            case Define.EItemType.PineCone:
                item = go_Item.AddComponent<PineConeItem>();
                break;

            case Define.EItemType.Water:
                item = go_Item.AddComponent<WaterRayzer>();
                break;

            case Define.EItemType.ElixirHerbs:
                item = go_Item.AddComponent<Recovery>();
                break;

            case Define.EItemType.Crane:
                item = go_Item.AddComponent<Haste>();
                break;

            case Define.EItemType.Mountine:
                item = go_Item.AddComponent<Gift>();
                break;

            case Define.EItemType.Deer:
                item = go_Item.AddComponent<DeerItem>();
                break;
        }

        _items.Add(itemType.ToString(), item);
        return item;
    }

    private void UpgradeItem(Define.EItemType itemType)
    {
        _items[itemType.ToString()].Upgrade();
    }

    /// <summary>
    /// �ִ� ������ ���� ���� �������� ����� �����ɴϴ�.
    /// </summary>
    /// <returns></returns>
    public Define.EItemType[] GetUpgradableItems()
    {
        List<Define.EItemType> items = new List<Define.EItemType>();

        foreach(var item in _items)
        {
            if(item.Value.Lv < Item.MaxLevel)
            {
                items.Add(item.Value.Type);
            }
        }
        return items.ToArray();
    }

    /// <summary>
    /// ���带 ������ ������ True �� ��ȯ �մϴ�. ��ȯ�� ���ÿ� 1ȸ �����մϴ�.
    /// </summary>
    /// <returns></returns>
    public bool HaveActivatedShield()
    {
        var shield = (Turtle)_items["Turtle"];
        if (shield == null) return false;

        return shield.Count > 0;
    }
}