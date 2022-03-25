using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Sprite> swordsSprites;
    public List<int> swordEnchantPrices;
    public List<int> expForLevels;
    public Player player;
    public Sword sword;
    public FloatingTextManager floatingTextManager;

    public int silver;
    public int exp;

    public GameObject moveableHUD;
    public GameObject deathMenu;
    public Animator deathMenuAnim;


    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(moveableHUD.gameObject);
            return;
        }
        PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(moveableHUD.gameObject);
        DontDestroyOnLoad(deathMenu.gameObject);
        DontDestroyOnLoad(deathMenuAnim);
    }

    public void ShowText(string text, int fontSize, Color fontColor, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(text, fontSize, fontColor, position, motion, duration);
    }

    public void SaveState()
    {
        string save = "";
        save += silver.ToString() + "|";
        save += exp.ToString() + "|";
        save += sword.swordLevel.ToString() + "|";
        PlayerPrefs.SetString("SaveState", save);
        Debug.Log("SaveState");
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] playersData = PlayerPrefs.GetString("SaveState").Split('|');
        Debug.Log("LoadState");

        silver = int.Parse(playersData[0]);
        exp = int.Parse(playersData[1]);
        sword.swordLevel = int.Parse(playersData[2]);
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    public void RestartGame()
    {
        silver = 0;
        exp = 0;
        sword.swordLevel = 0;
        CharacterInfo.instance.UpdateInfo();
        sword.spriteRenderer.sprite = GameManager.instance.swordsSprites[0];
        GameManager.instance.deathMenuAnim.SetTrigger("hide");
        SaveState();
        SceneManager.LoadScene(1);
        player.Respawn();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public int CurrentLevel()
    {
        int returnLevel = 0;
        int addExp = 0;

        while (exp >= addExp)
        {
            addExp += expForLevels[returnLevel];
            returnLevel++;

            if(returnLevel == expForLevels.Count)
            {
                return returnLevel;
            }

        }
        return returnLevel;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int exp = 0;

        while (r < level)
        {
            exp += expForLevels[r];
            r++;
        }

        return exp; 
    }

    public bool TryUpgradeWeapon()
    {
        if (swordEnchantPrices.Count <= sword.swordLevel)
            return false;

        if (silver >= swordEnchantPrices[sword.swordLevel])
        {
            silver -= swordEnchantPrices[sword.swordLevel];
            sword.UpgradeSword();
            return true;
        }
        return false;
    }

    public void GrantExp(int experience)
    {
        int currentLevel = CurrentLevel();
        exp += experience;

        if (currentLevel < CurrentLevel())
            LevelUp();
    }
    
    public void LevelUp()
    {
        GameManager.instance.ShowText("Leveled UP to " + CurrentLevel().ToString() + "!",
        40, new Color(56 / 255f, 199 / 255f, 1), GameObject.Find("Player").transform.position + Vector3.up * 0.64f, Vector3.up * 10, 4);
        player.maxHealthPoint++;
        player.healthPoint = player.maxHealthPoint;
    }
}
