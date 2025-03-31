using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    public GameObject wheelUI; // ���� UI ������
    public Button[] options; // �����嘘?�?����
    private bool isWheelActive = false; // �?���?����
    public int selectedOption = 0; // ��??��?������
    public float timeSpeed = 0.2f;
    void Start()
    {
        
    }
    void Update()
    {
        // �������?��???���
        if (Input.GetMouseButton(1)) // ����?��?�?������ 1
        {
            if (!isWheelActive)
            {
                ShowWheel(); // ��?����
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

        // ������?��?���??������?��?��
        if (isWheelActive)
        {
            UpdateSelectedOption();
        }

        // ������������
        if (isWheelActive && Input.GetMouseButtonDown(0)) // �������?�?������ 0
        {
            ConfirmSelection();
        }
    }

    // ��?���??���
    private void ShowWheel()
    {
        wheelUI.SetActive(true); // �������� UI
        isWheelActive = true; // ��������?����??
        UpdateSelectedOption(); // ��?��?��?��?��

    }

    // �������??���
    private void HideWheel()
    {
        wheelUI.SetActive(false); // �������� UI
        isWheelActive = false; // ��������??����??
    }

    // ����?��?��
    private void UpdateSelectedOption()
    {
        Vector2 mousePosition = Input.mousePosition;
        int newSelectedOption = -1;

        // ��������?�?�������?���?��?����
        for (int i = 0; i < options.Length; i++)
        {
            RectTransform optionRectTransform = options[i].GetComponent<RectTransform>();
            if (RectTransformOverlap(mousePosition, optionRectTransform))
            {
                newSelectedOption = i;
                break;
            }
        }

        // ���?��?�?���?������?��??
        if (newSelectedOption != selectedOption)
        {
            selectedOption = newSelectedOption;
            HighlightSelectedOption();
        }
    }

    // ������?��??��?��
    private void HighlightSelectedOption()
    {
        // ?��??�?���
        foreach (Button option in options)
        {
            option.GetComponent<Image>().color = Color.white; // ?����?
        }

        // ������??��?��
        if (selectedOption >= 0 && selectedOption < options.Length)
        {
            options[selectedOption].GetComponent<Image>().color = Color.yellow; // ������?
        }
    }

    // ?��?��
    private void ConfirmSelection()
    {
        if (!isWheelActive) return;

        if (selectedOption >= 0 && selectedOption < options.Length)
        {
            Debug.Log("?����?��: " + options[selectedOption].name);
            // �������������?��?�����?�
            HideWheel(); // ��������
        }
    }

    // �������?���?�� RectTransform ��
    private bool RectTransformOverlap(Vector2 mousePosition, RectTransform rectTransform)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePosition, null, out localPoint);
        return rectTransform.rect.Contains(localPoint);
    }
}
