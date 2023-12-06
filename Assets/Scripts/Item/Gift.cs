using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Gift : Item
{
    ItemManager _itemManager;
    Define.EItemType[] _types = new Define.EItemType[] {
        Define.EItemType.Stone, 
        Define.EItemType.ElixirHerbs, 
        Define.EItemType.PineCone };

    private void Awake()
    {
        Type = Define.EItemType.Mountine;

        _comments[0] = "�� ��ȭ\n�̵� �ӵ� ����\nü�� ȸ��";
        _comments[1] = "�� ��ȭ\n�̵� �ӵ� ����\nü�� ȸ��";
        _comments[2] = "�� ��ȭ\n�̵� �ӵ� ����\nü�� ȸ��";
        _comments[3] = "�� ��ȭ\n�̵� �ӵ� ����\nü�� ȸ��";
        _comments[4] = "�� ��ȭ\n�̵� �ӵ� ����\nü�� ȸ��";

        _itemManager = GetComponentInParent<ItemManager>();
    }

    public override void Upgrade()
    {
        foreach(var type in _types)
        {
            _itemManager.AddOrUpgradeItem(type);
        }
    }
}
