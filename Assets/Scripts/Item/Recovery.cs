using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Recovery : Item
{
    private void Awake()
    {
        Type = Define.EItemType.ElixirHerbs;

        _comments[0] = "ü�� 100% ȸ��";
        _comments[1] = "ü�� 100% ȸ��";
        _comments[2] = "ü�� 100% ȸ��";
        _comments[3] = "ü�� 100% ȸ��";
        _comments[4] = "ü�� 100% ȸ��";
    }

    public override void Upgrade()
    {
        Player.hp = Player.maxHp;
    }
}
