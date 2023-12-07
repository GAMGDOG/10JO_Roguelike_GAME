using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryTeller : MonoBehaviour
{
    private int currentStage;
    private Canvas canvas;
    private TMP_Text storyText;
    private TMP_Text skipText;
    private float fadeDuration = 1f;
    private float pauseDuration = 1.5f;
    private Color newcolor;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        currentStage = GameManager.stageCount;
        storyText = canvas.GetComponentsInChildren<TMP_Text>()[0];
        skipText = canvas.GetComponentsInChildren<TMP_Text>()[1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.ToNextStage();
        }
    }

    private void Start()
    {
        storyText.color = new Color(1f, 1f, 1f, 1f);
        StartCoroutine(ShowSkipText());
        newcolor = storyText.color;
        switch (currentStage)
        {
            case 0:
                StartCoroutine(ShowStory0());
                break;

            case 1:
                StartCoroutine(ShowStory1());
                break;

            case 2:
                StartCoroutine(ShowStory2());
                break;

            case 3:
                StartCoroutine(ShowStory3());
                break;

            default: break;
        }
    }
    private IEnumerator ShowStory0()
    {
        storyText.color = newcolor;
        storyText.fontSize = 80;
        StartCoroutine(ShowText("\"���� �����?\""));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        storyText.fontSize = 40;
        StartCoroutine(ShowText("���� �и�... �׷�..! ���ó�� �������鿡��..\r\n\nTIL�� �����϶�� �ϴٰ�.."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 80;
        StartCoroutine(ShowText("\"�׸�.. ������� �ɷ���.. �׾�����..\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 40;
        StartCoroutine(ShowText("����� ���� �ϰ������ ������..! \r\n\n��Ű.. �䵵 ����ϰ�.."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 80;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"����������!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"������!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 40;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"�װ� ������ �ð��� �����! �Ŵ�����! ���� �����̿���! \n\n�ϵ� �������� �������� �����ղ��� ���ϼ̾��!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 40;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"���� ���� �帱 �״� \n\n� �̰��� Ż���ؾ��ؿ�!\""));
        yield return new WaitForSecondsRealtime(3);

        GameManager.ToNextStage();
    }

    private IEnumerator ShowStory1()
    {
        storyText.color = newcolor;
        storyText.fontSize = 60;
        StartCoroutine(ShowText("\"���..���.. �ܿ� �س´�! \n\n������� �����ַ� ���ٴ�..����.. �������\""));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"�ƴϿ���. �Ŵ�����. �� ���ϴ°� �ֽ��ϴ�. \n\n���ư��ø� ����? ^^*\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"�������� ����ġ�� ȯ���� �� �ִٰ�? �װ� ������?\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"�������ݾƿ�. �ȵǴ� �� �����!\"\r\n\n\n���ڽ� �� 4�� ���� ��� �ȾƸ����ž�.."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"�װ� �׷���! �� ��������! \n\n�߽ɺη� �ٰ��� ���� �� ������ ����� ��Ÿ����! \n\n�����ؿ�!!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 80;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"�˾Ҿ�!!\""));
        yield return new WaitForSecondsRealtime(3);
        GameManager.ToNextStage();
    }

    private IEnumerator ShowStory2()
    {
        storyText.color = newcolor;
        storyText.fontSize = 80;
        StartCoroutine(ShowText("\"...��ġ����?\""));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"�������̿���?! �� ���� �ϸ� �ٽ� ��Ƴ��ٱ���! \n\n������!!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"���� �������̿���. \n\n�������� �����ļ� ���� �������� ���ư��ڱ���!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"�׷�.. �� �������� ����ġ��.. �׳࿡��.. ����Ұž�..\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"�� ��¥! �׷� �� ���� ���󱸿�!! �÷��� ���ݾƿ�!!!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.red;
        StartCoroutine(ShowText("\"�����ϴ�! ���Ⱑ ����� ������!! \n\n������ ���� �����ָ�!!!\""));
        yield return new WaitForSecondsRealtime(3);

        GameManager.ToNextStage();
    }

    private IEnumerator ShowStory3()
    {
        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.white;
        StartCoroutine(ShowText("�������� �������� ������ ����������."));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"�Ͼ�..�Ͼ�.. ���� ��� �Ǵ°ž�..? \n\nȯ���Ǵ°� Ȯ����?\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"��.. ���� �Ŵ������� �̽����� ���ư��� �ſ���..\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"�׷� ������.. \n\n��? ���� �� ���� ������� �����־�\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"...\"\r\n\n\n��������� ���� ������."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.white;
        StartCoroutine(ShowText("�� ���� ���� ����� ���� �־����� \n\n������� ���� �״�δ�. ��°��..?"));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 80;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"����.. �ƴ���? ����� ..?\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"..����� ���� �� �밡�� ���� ���� �Ǿ����.. \n\n������ �̾������� ������.. \n\n���� �������ϱ�!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"��..!?\"\r\n\n�� ���� ���� ���� ������ ���´�."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"�ູ�ϼ���.\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.white;
        StartCoroutine(ShowText("���� �ߴ� ���� õ��\r\n\n�߻߻� ���� ������� �鸰��."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.white;
        StartCoroutine(ShowText("�������... ����..."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.white;
        StartCoroutine(ShowText("...�˺���"));
        yield return new WaitForSecondsRealtime(3);
        GameManager.ToNextStage();
    }

    private IEnumerator ShowText(string text)
    {
        Debug.Log(text + " Start");
        storyText.text = text;
        Color originalColor = storyText.color;
        Color transparentColor = originalColor;
        transparentColor.a = 0f;

        // ������ �ؽ�Ʈ ��Ÿ����
        float counter = 0;

        while (counter < fadeDuration)
        {
            counter += 0.01f;
            float alpha = Mathf.Lerp(transparentColor.a, originalColor.a, counter / fadeDuration);
            storyText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        Debug.Log(text + "Wait");
        // ���� �ð� ���
        yield return new WaitForSecondsRealtime(pauseDuration);

        // ������ �ؽ�Ʈ �������
        counter = 0;

        while (counter < fadeDuration)
        {
            counter += 0.01f;
            float alpha = Mathf.Lerp(originalColor.a, transparentColor.a, counter / fadeDuration);
            storyText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        Debug.Log(text + " End");
    }

    private IEnumerator ShowSkipText()
    {
        Color originalColor = skipText.color;
        Color transparentColor = originalColor;
        transparentColor.a = 0f;

        // ������ �ؽ�Ʈ ��Ÿ����
        float counter = 0;

        while (counter < fadeDuration)
        {
            counter += 0.01f;
            float alpha = Mathf.Lerp(transparentColor.a, originalColor.a, counter / fadeDuration);
            skipText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
    }
}
