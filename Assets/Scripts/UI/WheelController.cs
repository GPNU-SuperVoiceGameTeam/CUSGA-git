using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    public GameObject wheelUI; // ���� UI ������
    public Button[] options; // �����е��ĸ�ѡ����
    private bool isWheelActive = false; // �Ƿ���ʾ����
    public int selectedOption = 0; // ��ǰѡ�е�ѡ������
    public float timeSpeed = 0.2f;

    void Update()
    {
        // �������Ҽ��Ƿ񱻰���
        if (Input.GetMouseButton(1)) // ����Ҽ��İ�ť������ 1
        {
            if (!isWheelActive)
            {
                ShowWheel(); // ��ʾ����
                Time.timeScale = timeSpeed;
            }
        }
        else
        {
            if (isWheelActive)
            {
                HideWheel(); // ��������
                Time.timeScale = 1;
            }
        }

        // ������̴��ڼ���״̬������ѡ�е�ѡ��
        if (isWheelActive)
        {
            UpdateSelectedOption();
        }

        // ������������
        if (isWheelActive && Input.GetMouseButtonDown(0)) // �������İ�ť������ 0
        {
            ConfirmSelection();
        }
    }

    // ��ʾ���̵ķ���
    private void ShowWheel()
    {
        wheelUI.SetActive(true); // �������� UI
        isWheelActive = true; // ��������Ϊ����״̬
        UpdateSelectedOption(); // ��ʼ��ѡ�е�һ��ѡ��

    }

    // �������̵ķ���
    private void HideWheel()
    {
        wheelUI.SetActive(false); // �������� UI
        isWheelActive = false; // ��������Ϊδ����״̬
    }

    // ����ѡ�е�ѡ��
    private void UpdateSelectedOption()
    {
        Vector2 mousePosition = Input.mousePosition;
        int newSelectedOption = -1;

        // ��������ѡ��������Ƿ���ĳ��ѡ����
        for (int i = 0; i < options.Length; i++)
        {
            RectTransform optionRectTransform = options[i].GetComponent<RectTransform>();
            if (RectTransformOverlap(mousePosition, optionRectTransform))
            {
                newSelectedOption = i;
                break;
            }
        }

        // ���ѡ�е�ѡ����仯������ѡ��״̬
        if (newSelectedOption != selectedOption)
        {
            selectedOption = newSelectedOption;
            HighlightSelectedOption();
        }
    }

    // ������ʾ��ǰѡ�е�ѡ��
    private void HighlightSelectedOption()
    {
        // ȡ��֮ǰ�ĸ���
        foreach (Button option in options)
        {
            option.GetComponent<Image>().color = Color.white; // Ĭ����ɫ
        }

        // ������ǰѡ�е�ѡ��
        if (selectedOption >= 0 && selectedOption < options.Length)
        {
            options[selectedOption].GetComponent<Image>().color = Color.yellow; // ������ɫ
        }
    }

    // ȷ��ѡ��
    private void ConfirmSelection()
    {
        if (!isWheelActive) return;

        if (selectedOption >= 0 && selectedOption < options.Length)
        {
            Debug.Log("ѡ����ѡ��: " + options[selectedOption].name);
            // ������������ѡ��ѡ�����߼�
            HideWheel(); // ��������
        }
    }

    // �������Ƿ���ĳ�� RectTransform ��
    private bool RectTransformOverlap(Vector2 mousePosition, RectTransform rectTransform)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePosition, null, out localPoint);
        return rectTransform.rect.Contains(localPoint);
    }
}
