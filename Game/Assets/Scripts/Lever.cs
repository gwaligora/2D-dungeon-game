using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool triggering = false;
    private bool isClosed = true;
    public GameObject interactButton;
    public GameObject leverOff;
    public GameObject leverOn;
    public GameObject Wall;

    private void Start()
    {
        leverOff.SetActive(true);
        leverOn.SetActive(false);
    }

    private void Update()
    {
        if (triggering)
        {
            Vector3 position = Camera.main.WorldToScreenPoint(GameObject.Find("leverPlaceholder").transform.position);
            interactButton.transform.position = position;
            if (Input.GetKeyDown(KeyCode.E))
                SwitchLever();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            triggering = true;
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactButton.SetActive(false);
            triggering = false;
        }
    }

    private void SwitchLever()
    {
        if (isClosed)
        {
            isClosed = false;
            leverOn.SetActive(true);
            leverOff.SetActive(false);
            Wall.SetActive(false);
        }

        else if (!isClosed)
        {
            isClosed = true;
            leverOn.SetActive(false);
            leverOff.SetActive(true);
            Wall.SetActive(true);
        }
    }
}
