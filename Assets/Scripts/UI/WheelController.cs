using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    public GameObject wheelUI; // ееее UI ееееее
    public Button[] options; // еееее??е?ееее
    private bool isWheelActive = false; // е?еее?ееее
    public int selectedOption = 0; // ее??ее?ееееее
    public float timeSpeed = 0.2f;
    void Start()
    {
        
    }
    void Update()
    {
        // еееееее?ее???еее
        if (Input.GetMouseButton(1)) // ееее?ее?е?ееееее 1
        {
            if (!isWheelActive)
            {
                ShowWheel(); // ее?ееее
                Time.timeScale = timeSpeed;
            }
        }
        else
        {
            if (isWheelActive)
            {
                HideWheel(); // ееееееее
                Time.timeScale = 1;
            }
        }

        // ееееее?ее?еее??ееееее?ее?ее
        if (isWheelActive)
        {
            UpdateSelectedOption();
        }

        // ееееееееееее
        if (isWheelActive && Input.GetMouseButtonDown(0)) // еееееее?е?ееееее 0
        {
            ConfirmSelection();
        }
    }

    // ее?еее??еее
    private void ShowWheel()
    {
        wheelUI.SetActive(true); // ееееееее UI
        isWheelActive = true; // ееееееее?ееее??
        UpdateSelectedOption(); // ее?ее?ее?ее?ее

    }

    // еееееее??еее
    private void HideWheel()
    {
        wheelUI.SetActive(false); // ееееееее UI
        isWheelActive = false; // ееееееее??ееее??
    }

    // ееее?ее?ее
    private void UpdateSelectedOption()
    {
        Vector2 mousePosition = Input.mousePosition;
        int newSelectedOption = -1;

        // ееееееее?е?еееееее?еее?ее?ееее
        for (int i = 0; i < options.Length; i++)
        {
            RectTransform optionRectTransform = options[i].GetComponent<RectTransform>();
            if (RectTransformOverlap(mousePosition, optionRectTransform))
            {
                newSelectedOption = i;
                break;
            }
        }

        // еее?ее?е?еее?ееееее?ее??
        if (newSelectedOption != selectedOption)
        {
            selectedOption = newSelectedOption;
            HighlightSelectedOption();
        }
    }

    // ееееее?ее??ее?ее
    private void HighlightSelectedOption()
    {
        // ?ее??е?еее
        foreach (Button option in options)
        {
            option.GetComponent<Image>().color = Color.white; // ?ееее?
        }

        // ееееее??ее?ее
        if (selectedOption >= 0 && selectedOption < options.Length)
        {
            options[selectedOption].GetComponent<Image>().color = Color.yellow; // ееееее?
        }
    }

    // ?ее?ее
    private void ConfirmSelection()
    {
        if (!isWheelActive) return;

        if (selectedOption >= 0 && selectedOption < options.Length)
        {
            Debug.Log("?ееее?ее: " + options[selectedOption].name);
            // еееееееееееее?ее?еееее?е
            HideWheel(); // ееееееее
        }
    }

    // еееееее?еее?ее RectTransform ее
    private bool RectTransformOverlap(Vector2 mousePosition, RectTransform rectTransform)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePosition, null, out localPoint);
        return rectTransform.rect.Contains(localPoint);
    }
}
