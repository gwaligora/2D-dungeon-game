using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    public Text levelText, healthPointText, silverText, expText, swordDamageText, swordLevelText;
    public Image swordSprite;
    public Image gunSprite;
    public RectTransform expBar;

    public static CharacterInfo instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateInfo()
    {
        swordSprite.sprite = GameManager.instance.swordsSprites[GameManager.instance.sword.swordLevel];
        healthPointText.text = GameManager.instance.player.healthPoint.ToString();
        silverText.text = GameManager.instance.silver.ToString();
        levelText.text = GameManager.instance.CurrentLevel().ToString();
        swordLevelText.text = GameManager.instance.sword.swordLevel.ToString();
        swordDamageText.text = GameManager.instance.sword.damage[GameManager.instance.sword.swordLevel].ToString();

        int currentLevel = GameManager.instance.CurrentLevel();

        if(currentLevel == GameManager.instance.expForLevels.Count)
        {
            expText.text = GameManager.instance.exp.ToString();
            expBar.localScale =  Vector3.one;
        }
        else
        {
            int prevExp = GameManager.instance.GetXpToLevel(currentLevel -1);
            int currentExp = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currentExp - prevExp;
            int currExpForLevel = GameManager.instance.exp - prevExp;

            float barCompletionRate = (float)currExpForLevel / (float)diff;
            expBar.localScale = new Vector3(barCompletionRate, 1, 1);
            expText.text = currExpForLevel.ToString() + "/" + diff.ToString();
        }
    }
}
