using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour
{
    public bool isPauseMenuOn;
    private Animator animator;
    public Button PauseMenuOffButton;
    public GameObject HUD;

    private void Start()
    {
        isPauseMenuOn = false;
        animator = GetComponent<Animator>();
        PauseMenuOffButton.onClick.AddListener(HidePauseMenu);
    }
    private void ShowPauseMenu()
    {
        animator.ResetTrigger("hide");
        isPauseMenuOn = true;
        animator.SetTrigger("show");
        HUD.SetActive(false);
        GameManager.instance.player.canMove = false;
    }

    private void HidePauseMenu()
    {
        HUD.SetActive(true);
        animator.ResetTrigger("show");
        isPauseMenuOn = false;
        animator.SetTrigger("hide");
        GameManager.instance.player.canMove = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPauseMenuOn == false)
            ShowPauseMenu();

        else if (Input.GetKeyDown(KeyCode.Escape) && isPauseMenuOn == true)
            HidePauseMenu();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
        SceneManager.LoadScene(1);
    }
}
