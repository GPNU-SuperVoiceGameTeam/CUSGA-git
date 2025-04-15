using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button[] wave;
    public List<Item> itemList = new List<Item>();
    public Image selectIcon;
    public Text selectText;
    public Text selectName;
    public Button equipButton;
    public Image equipment;
    public PlayerController player;
    public GameObject showSpecialWave;
    private Item item;

    void Start()
    {
        for(int i = 0; i < wave.Length; i++){
            int index = i;
            wave[i].onClick.AddListener(() => SelectWave(index));
            wave[i].interactable = false;
            wave[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
        }
        equipButton.onClick.AddListener(EquipItem);
    }
    void Update()
    {
        for(int i = 0; i < wave.Length; i++){
            if(itemList[i].isUnlocked == true){
                wave[i].interactable = true;
                wave[i].GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
            }
        }
    }
    void SelectWave(int index){
        item = itemList[index];
        selectIcon.sprite = item.itemImage;
        selectText.text = item.itemInfo;
        selectName.text = item.itemName;
    }

    void EquipItem(){
        // item.isEquipped = true;
        equipment.GetComponent<Image>().sprite = selectIcon.sprite;
        player.specialBullet = item.type;
        showSpecialWave.GetComponent<Image>().sprite = item.itemImage;
    }
}

