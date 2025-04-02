using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text dialogueText; // 对话文本UI元素
    public GameObject dialogueBubble; // 对话气泡UI元素
    public PlayerController playerController; // 玩家控制器

    private string[] dialogues = new string[]
    {
        "这是第一段对话。",
        "这是第二段对话。",
        "这是第三段对话。"
    };

    private int currentDialogueIndex = 0;
    private bool isDialogueActive = false;
    private bool isPlayerInputEnabled = true;

    void Start()
    {
        
        HideDialogue();
    }

    void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            ShowNextDialogue();
        }
    }

    public void StartDialogue()
    {
        if (!isDialogueActive)
        {
            playerController.canMove = false; // 禁止玩家移动
            dialogueBubble.SetActive(true);
            currentDialogueIndex = 0;
            ShowNextDialogue();
        }
    }

    private void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            EndDialogue();
            playerController.canMove = true; // 允许玩家移动
        }
    }

    private void ShowDialogue()
    {
        dialogueBubble.SetActive(true);
        isDialogueActive = true;
        DisablePlayerInput(); // 禁用玩家输入
    }

    private void HideDialogue()
    {
        dialogueBubble.SetActive(false);
        isDialogueActive = false;
        EnablePlayerInput(); // 启用玩家输入
    }

    private void EndDialogue()
    {
        HideDialogue();
    }

    private void DisablePlayerInput()
    {
        isPlayerInputEnabled = false;
    }

    private void EnablePlayerInput()
    {
        isPlayerInputEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }
}



