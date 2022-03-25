using UnityEngine;
using UnityEngine.UI;


public class InteractEnchanter : MonoBehaviour
{
    public Button exitEnchanting;
    public bool isEnchantingOn;
    public Animator animator;
    private GameObject triggeringNPC;
    public bool triggering;
    public GameObject interactButton;
    private string[] welcomeSentences = { "want some upgrades... huh? ", "Hello!", "Welcome... stranger", "What do you need?", "I've got something for you." };

    private void Start()
    {
        exitEnchanting.onClick.AddListener(HideEnchanting);
        isEnchantingOn = false;
    }

    private void Update()
    {
        
        if (triggering)
        {
            Vector3 position = Camera.main.WorldToScreenPoint(gameObject.transform.GetChild(0).transform.position);
            interactButton.transform.position = position;
            if (isEnchantingOn && Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.instance.TryUpgradeWeapon();
                EnchantingMenu.instance.UpdateMenu();
            }

            if (Input.GetKeyDown(KeyCode.E) && isEnchantingOn == false)
                ShowEnchanting();

            else if (Input.GetKeyDown(KeyCode.E) && isEnchantingOn == true)
                HideEnchanting();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            animator.ResetTrigger("hide");
            interactButton.SetActive(true);
            triggering = true;
            triggeringNPC = other.gameObject;

            int i = Random.Range(0, welcomeSentences.Length );
            GameManager.instance.ShowText(welcomeSentences[i], 20, Color.white,
                gameObject.transform.position + Vector3.up * 0.23f, Vector3.up * 5, 3f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactButton.SetActive(false);
            triggering = false;
            triggeringNPC = null;
            HideEnchanting();
        }
    }

    public void HideEnchanting()
    {
        animator.ResetTrigger("show");
        isEnchantingOn = false;
        animator.SetTrigger("hide");
    }

    public void ShowEnchanting()
    {
        animator.SetTrigger("show");
        EnchantingMenu.instance.UpdateMenu();
        interactButton.SetActive(false);
        CharacterInfo.instance.UpdateInfo();
        animator.ResetTrigger("hide");
        isEnchantingOn = true;
        animator.SetTrigger("show");
    }
}