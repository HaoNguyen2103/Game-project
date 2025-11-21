using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    public Image weaponSlotImage;
    public Image helmetSlotImage;
    public Image shieldSlotImage;
    public Image bootsSlotImage;
    public Image ArmorSlotImage;
    public Image GlovesSlotImage;
    public Image CloaKSlotImage;
    public Image OffHandSlotImage;
    public Image Ring1SlotImage;
    public Image Ring2SlotImage;

    public void SetWeapon(Sprite icon) => weaponSlotImage.sprite = icon;
    public void SetHelmet(Sprite icon) => helmetSlotImage.sprite = icon;
    public void SetShield(Sprite icon) => shieldSlotImage.sprite = icon;
    public void SetBoots(Sprite icon) => bootsSlotImage.sprite = icon;
    public void SetArmor(Sprite icon) => ArmorSlotImage.sprite = icon;
    public void SetGloves(Sprite icon) => GlovesSlotImage.sprite = icon;
    public void SetCloak(Sprite icon) => CloaKSlotImage.sprite = icon;
    public void SetOffHand(Sprite icon) => OffHandSlotImage.sprite = icon;
    public void SetRing1(Sprite icon) => Ring1SlotImage.sprite = icon;
    public void SetRing2(Sprite icon) => Ring2SlotImage.sprite = icon;

}
