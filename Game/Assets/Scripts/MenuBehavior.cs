using UnityEngine;
using UnityEngine.UI;


public class MenuBehavior : MonoBehaviour
{
    public bool isCharInfoOn;
    private Animator animator;
    public Button CharacterInfoOffButton;

    private void Start()
    {
        isCharInfoOn = false;
        animator = GetComponent<Animator>();
        CharacterInfoOffButton.onClick.AddListener(HideCharInfo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isCharInfoOn == false)
            ShowCharInfo();

        else if (Input.GetKeyDown(KeyCode.P) && isCharInfoOn == true) 
            HideCharInfo();
    }

    public void HideCharInfo()
    {
        animator.ResetTrigger("show");
        isCharInfoOn = false;
        animator.SetTrigger("hide");
    }

    public void ShowCharInfo()
    {
        CharacterInfo.instance.UpdateInfo();
        animator.ResetTrigger("hide");
        isCharInfoOn = true;
        animator.SetTrigger("show");
    }
}
