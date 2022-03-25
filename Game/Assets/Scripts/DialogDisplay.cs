using UnityEngine;

public class DialogDisplay : MonoBehaviour
{   
    public Conversation conversation;
    public GameObject speakerLeft;
    public GameObject speakerRight;
    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;
    private int activeLineIndex = 0;
    public bool isTriggering;
    public GameObject interactButton;
    public bool isConversationOn;
    public Animator animator;
    public GameObject nextSentenceText;

    void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();
        speakerUILeft.Speaker = conversation.leftSpeaker;
        speakerUIRight.Speaker = conversation.rightSpeaker;
    }
    
    void Update()
    {
        if (isTriggering)
        {
            Vector3 position = Camera.main.WorldToScreenPoint(gameObject.transform.GetChild(0).transform.position);
            interactButton.transform.position = position;

            if (Input.GetKeyDown(KeyCode.E) && isConversationOn == false)
            {
                isConversationOn = true;
                AdvanceConversation();
                animator.ResetTrigger("hide");
                animator.SetTrigger("show");
            }

            if (isConversationOn)
                ConversationOn();
        }
    }

    void AdvanceConversation()
    {
        if(activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
            activeLineIndex += 1;
            nextSentenceText.SetActive(true);
        }
        else
        {
            SetDialog(speakerUILeft, speakerUIRight, "...");
            nextSentenceText.SetActive(false);
        }
    }
    void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            SetDialog(speakerUILeft, speakerUIRight, line.text);
        }
        else if(speakerUIRight.SpeakerIs(character))
        {
            SetDialog(speakerUIRight, speakerUILeft, line.text);
        }
    }
    void SetDialog(SpeakerUI activeSpeakerUI, SpeakerUI inactiveSpeakerUI, string text)
    {
        activeSpeakerUI.Dialog = text;
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activeLineIndex = 0;
            interactButton.SetActive(true);
            isTriggering = true;
            animator.ResetTrigger("hide");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activeLineIndex = 0;
            interactButton.SetActive(false);
            isTriggering = false;
            isConversationOn = false;
            speakerUIRight.Hide();
            animator.ResetTrigger("show");
            animator.SetTrigger("hide");
        }
    }

    private void ConversationOn()
    {
        interactButton.SetActive(false);

        if (Input.GetKeyDown(KeyCode.F))
        {
            AdvanceConversation();
        }
    }

}
