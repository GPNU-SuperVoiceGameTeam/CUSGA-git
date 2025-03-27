using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    public GameObject wheelUI; // 轮盘 UI 的引用
    public Button[] options; // 轮盘中的四个选择项
    private bool isWheelActive = false; // 是否显示轮盘
    public int selectedOption = 0; // 当前选中的选项索引
    public float timeSpeed = 0.2f;

    void Update()
    {
        // 检测鼠标右键是否被按下
        if (Input.GetMouseButton(1)) // 鼠标右键的按钮索引是 1
        {
            if (!isWheelActive)
            {
                ShowWheel(); // 显示轮盘
                Time.timeScale = timeSpeed;
            }
        }
        else
        {
            if (isWheelActive)
            {
                HideWheel(); // 隐藏轮盘
                Time.timeScale = 1;
            }
        }

        // 如果轮盘处于激活状态，更新选中的选项
        if (isWheelActive)
        {
            UpdateSelectedOption();
        }

        // 检测鼠标左键点击
        if (isWheelActive && Input.GetMouseButtonDown(0)) // 鼠标左键的按钮索引是 0
        {
            ConfirmSelection();
        }
    }

    // 显示轮盘的方法
    private void ShowWheel()
    {
        wheelUI.SetActive(true); // 激活轮盘 UI
        isWheelActive = true; // 设置轮盘为激活状态
        UpdateSelectedOption(); // 初始化选中第一个选项

    }

    // 隐藏轮盘的方法
    private void HideWheel()
    {
        wheelUI.SetActive(false); // 禁用轮盘 UI
        isWheelActive = false; // 设置轮盘为未激活状态
    }

    // 更新选中的选项
    private void UpdateSelectedOption()
    {
        Vector2 mousePosition = Input.mousePosition;
        int newSelectedOption = -1;

        // 遍历所有选项，检查鼠标是否在某个选项上
        for (int i = 0; i < options.Length; i++)
        {
            RectTransform optionRectTransform = options[i].GetComponent<RectTransform>();
            if (RectTransformOverlap(mousePosition, optionRectTransform))
            {
                newSelectedOption = i;
                break;
            }
        }

        // 如果选中的选项发生变化，更新选中状态
        if (newSelectedOption != selectedOption)
        {
            selectedOption = newSelectedOption;
            HighlightSelectedOption();
        }
    }

    // 高亮显示当前选中的选项
    private void HighlightSelectedOption()
    {
        // 取消之前的高亮
        foreach (Button option in options)
        {
            option.GetComponent<Image>().color = Color.white; // 默认颜色
        }

        // 高亮当前选中的选项
        if (selectedOption >= 0 && selectedOption < options.Length)
        {
            options[selectedOption].GetComponent<Image>().color = Color.yellow; // 高亮颜色
        }
    }

    // 确认选择
    private void ConfirmSelection()
    {
        if (!isWheelActive) return;

        if (selectedOption >= 0 && selectedOption < options.Length)
        {
            Debug.Log("选中了选项: " + options[selectedOption].name);
            // 在这里可以添加选中选项后的逻辑
            HideWheel(); // 隐藏轮盘
        }
    }

    // 检查鼠标是否在某个 RectTransform 内
    private bool RectTransformOverlap(Vector2 mousePosition, RectTransform rectTransform)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePosition, null, out localPoint);
        return rectTransform.rect.Contains(localPoint);
    }
}
