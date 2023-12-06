using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    public Rigidbody2D _rigidbody;
    public SpriteRenderer _sprite;
    AudioSource ApplyDamage;


    public float maxHp;     //�ִ�ü��
    public float hp;        //ü��
    public float atk;       //���ݷ� ����
    public float speed;     //�̵��ӵ� ����
    public int level;       //����
    public int currentExp;  //����ġ   
    public int maxExp;
    public int money;       //��
    public bool isDead;
    
    int layer_name;
    private float _expWeight = 1.5f;

    public Player()
    {
        maxHp = 100; //�⺻ �ִ� HP
        hp = maxHp;  //�ִ� HP -> �������� ���帶�� �ִ� HP�� �ʱ�ȭ
        atk = 1;     //���ݷ� ����(���� ������ * atk)
        speed = 1;   //�̵��ӵ� ����
        level = 1;   //���� ����(���� ����, ���� Ŭ���� �� �ʱ�ȭ - �������� Ŭ���� �ƴ�)
        currentExp = 0;     //���� exp(���� ����, ���� Ŭ���� �� �ʱ�ȭ - �������� Ŭ���� �ƴ�)
        maxExp = 50;
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
        currentExp = playerData.currentExp;
        money = playerData.money;
        isDead = false;
    }

    private void Awake()
    {
        layer_name = LayerMask.NameToLayer("Player");
        this.gameObject.layer = layer_name;
        _rigidbody = GetComponent<Rigidbody2D>();
        ApplyDamage = GetComponent<AudioSource>();
    }

    private void Update()
    { //�� ������ �÷��̾� ü�¹� ���� 
        ChangeHpBar(hp);
    }

    private void ChangeHpBar(float hp) //���� ü�� ü�¹ٿ� ǥ��
    {
        hpBar.fillAmount = hp / maxHp;
    }

    private void OnCollisionEnter2D(Collision2D collision)  //�� ����ü�� ���� �� ������ ����
    {
        layer_name = LayerMask.NameToLayer("Attack");
        if (GameManager.Instance.player.isDead)
            return;
        else
        {
            if (GameManager.Instance.player.hp <= 0)
            {
                isDead = true;
                GameManager.Instance.GameOver();
            }
            if (collision.gameObject.layer == layer_name)
            {
                ApplyDamage.Play();
                OnDamage(CheckDamage(collision)); //���� ���� �������� ���� ����
            }
        }
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
                GameManager.Instance.GameOver();
            }
            if (collision.gameObject.layer == layer_name)
            {
                ApplyDamage.Play();
                OnDamage(CheckDamage(collision));
            }
        }
    }

    void OnDamage(float damage)
    {
        Debug.Log("Attacked");
        if (GetComponent<ItemManager>().HaveActivatedShield())
        {
            gameObject.layer = 20;
            Invoke("OffDamage", 1);
            return;
        }
        _sprite.color = new Color(1, 1, 1, 0.4f);
        GameManager.Instance.player.hp -= damage;
        gameObject.layer = 20;
        Invoke("OffDamage", 1);

    }//�������� ���� ���

    void OffDamage()
    {
        _sprite.color = new Color(1, 1, 1, 1);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }//�������� �ް� �����ð� ���� ��������

    public void GetExp(int _exp)
    {
        currentExp += _exp;
        while (currentExp >= maxExp)
        {
            GameManager.Instance.uiManager.LvFlag++;
            level++;
            currentExp -= maxExp;
            maxExp = (int)(_expWeight * maxExp);
            _expWeight *= 1.2f;
            Debug.Log($"player level up, LvFlag: {GameManager.Instance.uiManager.LvFlag}");
        }
    }//����ġ ȹ�� ��

    float CheckDamage(Collision2D collision)
    {
        float _damage = 0;
        var obj = collision.gameObject;

        if (obj.tag == "Monster")
        {
            _damage = obj.GetComponent<Monster>().Damege;
        }
        else if(obj.tag =="Attack")
        {
            _damage = obj.GetComponent<AttackHit>().damage;
        }
        return _damage;
    }
}
