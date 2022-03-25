using UnityEngine;
using UnityEngine.UI;

public class EnchantingMenu : MonoBehaviour
{
    private int sword2Level;
    private int sword2Damage;
    public static EnchantingMenu instance;
    public Sprite nullSprite;
    public Text yourSilverText, upgradeCostText, sword1DamageText, Sword1LevelText, sword2DamageText, Sword2LevelText;
    public Image sword1, sword2;

    private void Awake()
    {
        instance = this;
    }

    public void OnCilckUpgrade()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }
    
    public void UpdateMenu()
    {
        if(GameManager.instance.sword.swordLevel < 9)
        {
            sword2Level = GameManager.instance.sword.swordLevel + 1;
            sword2Damage = GameManager.instance.sword.damage[sword2Level];
            sword1.sprite = GameManager.instance.swordsSprites[GameManager.instance.sword.swordLevel];
            sword2.sprite = GameManager.instance.swordsSprites[GameManager.instance.sword.swordLevel + 1];
            yourSilverText.text = GameManager.instance.silver.ToString();
            upgradeCostText.text = GameManager.instance.swordEnchantPrices[GameManager.instance.sword.swordLevel].ToString();
            sword1DamageText.text = GameManager.instance.sword.damage[GameManager.instance.sword.swordLevel].ToString();
            Sword1LevelText.text = Sword2LevelText.text = GameManager.instance.sword.swordLevel.ToString();
            sword2DamageText.text = sword2Damage.ToString();
            Sword2LevelText.text = sword2Level.ToString();
        }
        else
        {
            sword2DamageText.text = null;
            Sword2LevelText.text = null;
            sword2.sprite = nullSprite;
            upgradeCostText.text = null; 
        }
    }
}
