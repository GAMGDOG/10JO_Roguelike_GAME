using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeTransparency(0.3f); // ���콺�� �ö��� �� �������ϰ� ���� (���İ��� 1�� ����)
    }

    // ��ư���� ���콺�� ���������� �� ȣ��Ǵ� �Լ�
    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeTransparency(0f); // ���콺�� ���������� �� �����ϰ� ���� (���İ��� 0.5�� ����)
    }

    // �̹����� ���İ��� �����ϴ� �Լ�
    void ChangeTransparency(float alphaValue)
    {
        Color tempColor = buttonImage.color;
        tempColor.a = alphaValue; // ���İ� ����
        buttonImage.color = tempColor; // ����� ���İ��� �̹����� ����
    }
}
