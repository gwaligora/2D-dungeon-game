using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator.SetTrigger("go");
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
