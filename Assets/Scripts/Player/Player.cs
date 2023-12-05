using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    public Rigidbody2D _rigidbody;
    public SpriteRenderer _sprite;
    Animator anim;          //�̵� �ִϸ��̼� �߰� ���� 
    
    public float maxHp;     //�ִ�ü��
    public float hp;        //ü��
    public float atk;       //���ݷ� ����
    public float speed;     //�̵��ӵ� ����
    public int level;       //����
    public int exp;         //����ġ   
    public int money;       //��
    public bool isDead;

    int layer_name;

    public Player()
    {
        maxHp = 100; //�⺻ �ִ� HP
        hp = maxHp;  //�ִ� HP -> �������� ���帶�� �ִ� HP�� �ʱ�ȭ
        atk = 1;     //���ݷ� ����(���� ������ * atk)
        speed = 1;   //�̵��ӵ� ����
        level = 1;   //���� ����(���� ����, ���� Ŭ���� �� �ʱ�ȭ - �������� Ŭ���� �ƴ�)
        exp = 0;     //���� exp(���� ����, ���� Ŭ���� �� �ʱ�ȭ - �������� Ŭ���� �ƴ�)
        money = 0;   //���� gold(����ȭ��, �������ͽ� ��ȭ ȭ�鿡�� ����ϴ� ������ maxHp, atk, speed�� ���������� ����)
                     //�����Ҷ� ���� �ʿ��� money ����
        isDead = false;
    }

    public Player(PlayerData playerData)
    {
        maxHp = playerData.maxHp;
        atk = playerData.atk;
        speed = playerData.speed;
        level = playerData.level;
        exp = playerData.currentExp;
        money = playerData.money;
        isDead = false;
    }

    private void Awake()
    {
        layer_name = LayerMask.NameToLayer("Player");
        this.gameObject.layer = layer_name;
        _rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ChangeHpBar(hp); //�� ������ �÷��̾� ü�¹� ���� 
    }

    private void ChangeHpBar(float hp) //���� ü�� ü�¹ٿ� ǥ��
    {
        hpBar.fillAmount = hp / maxHp;
    }

    private void OnCollisionStay2D(Collision2D collision) //���� ���� �� ������ ����
    {
        layer_name = LayerMask.NameToLayer("Monster");
        if (GameManager.Instance.player.isDead)
            return;
        else
        {
            if (GameManager.Instance.player.hp <= 0)
            {
                isDead = true;
                anim.SetTrigger("isDead");
                GameManager.Instance.GameOver();
            }
            if (collision.gameObject.layer == layer_name)
            {
                OnDamage();
            }
        }
    }

    void OnDamage()
    {
        Debug.Log("Attacked");
        if (GetComponent<ItemManager>().HaveActivatedShield()) return;
        _sprite.color = new Color(1, 1 , 1, 0.4f);
        GameManager.Instance.player.hp -= Time.deltaTime * 10;
        Invoke("OffDamage", 1);
    }//�������� ���� ���

    void OffDamage()
    {
        _sprite.color = new Color(1, 1, 1, 1);
    }

    public void GetExp(int _exp)
    {
        exp += _exp;
        if(exp > level*5)
        {
            level++;
            exp -= level * 5;
        }
    }//����ġ ȹ�� ��
}
