using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Portal : Collidable
{
    public bool isMyChoice;
    public int myBuildIndex;
    public Animator transition;
    private float trasitionTime = 1f;

    protected override void OnCollide(Collider2D collider)
    {
        if (isMyChoice && collider.name == "Player")
        {            
            GameManager.instance.SaveState();
            StartCoroutine(LoadLevel(myBuildIndex));           
        }
        else if(isMyChoice == false && collider.name == "Player")
        {
                GameManager.instance.SaveState();
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));         
        }

        IEnumerator LoadLevel(int levelIndex)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(trasitionTime);

            SceneManager.LoadScene(levelIndex);
        }
    }
}
