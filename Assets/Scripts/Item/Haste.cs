using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class Haste : Item
{
    float _defaultPlayerSpeed;

    private void Awake()
    {
        Type = Define.EItemType.Crane;

        _comments[0] = "�̵� �ӵ� ����";
        _comments[1] = "�̵� �ӵ� ����";
        _comments[2] = "�̵� �ӵ� ����";
        _comments[3] = "�̵� �ӵ� ����";
        _comments[4] = "�̵� �ӵ� ����";

        if (Player)
        {
            _defaultPlayerSpeed = Player.speed;
        }
    }

    public override void Upgrade()
    {
        if (IsMaxLevel()) return;
        ++Lv;
        Player.speed += 0.1f;
    }
}
