using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item", order = 1)]
public class Item : ScriptableObject
{
    public PlayerController.specialBulletType type;
    public string itemName;
    public Sprite itemImage;
    [TextArea]
    public string itemInfo;

    public bool isUnlocked;
    public bool isEquipped;

}
