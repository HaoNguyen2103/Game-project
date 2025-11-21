using UnityEngine;
using UnityEngine.UI;

public class UseWeaponButton : MonoBehaviour
{
    public CharacterInfoUI characterInfoUI;
    public Sprite itemIcon;

    public enum EquipmentType { Weapon, Helmet, Shield, Boots, Armor, Gloves, Cloak, OffHand, Ring1, Ring2 }
    public EquipmentType equipmentType;

    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickEquip);
    }
    private void OnClickEquip()
    {
        switch (equipmentType)
        {
            case EquipmentType.Weapon:
                characterInfoUI.SetWeapon(itemIcon);
                break;
            case EquipmentType.Helmet:
                characterInfoUI.SetHelmet(itemIcon);
                break;
            case EquipmentType.Shield:
                characterInfoUI.SetShield(itemIcon);
                break;
            case EquipmentType.Boots:
                characterInfoUI.SetBoots(itemIcon);
                break;
            case EquipmentType.Armor:
                characterInfoUI.SetArmor(itemIcon);
                break;
            case EquipmentType.Gloves:
                characterInfoUI.SetGloves(itemIcon);
                break;
            case EquipmentType.Cloak:
                characterInfoUI.SetCloak(itemIcon);
                break;
            case EquipmentType.OffHand:
                characterInfoUI.SetOffHand(itemIcon);
                break;
            case EquipmentType.Ring1:
                characterInfoUI.SetRing1(itemIcon);
                break;
            case EquipmentType.Ring2:
                characterInfoUI.SetRing2(itemIcon);
                break;

        }
    }
}
