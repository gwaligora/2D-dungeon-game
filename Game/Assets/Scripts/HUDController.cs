using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public RectTransform expBarHUD;
    public Text levelText;
    public RectTransform healthBarHUD;
    public GameObject hud;

    private void Update()
    {
        if (GameManager.instance.player.canMove == false)
            hud.SetActive(false);
        else
            hud.SetActive(true);


        levelText.text = "Lvl. " + GameManager.instance.CurrentLevel().ToString();
        int currentLevel = GameManager.instance.CurrentLevel();
        OnChangeHP();

        if (currentLevel == GameManager.instance.expForLevels.Count)
        {
            expBarHUD.localScale = Vector3.one;
        }
        else
        {
            int prevExp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentExp = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currentExp - prevExp;
            int currExpForLevel = GameManager.instance.exp - prevExp;

            float barCompletionRate = (float)currExpForLevel / (float)diff;
            expBarHUD.localScale = new Vector3(barCompletionRate, 1, 1);
        } 
    }

    public void OnChangeHP()
    {
        float ratio = (float)GameManager.instance.player.healthPoint / (float)GameManager.instance.player.maxHealthPoint;
        healthBarHUD.localScale = new Vector3(ratio, 1, 1);
    }
}
