using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject game_object;
    public Text text;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        game_object.SetActive(true);
    }

    public void Hide()
    {
        active = false;
        game_object.SetActive(false);
    }

    public void UpdateText()
    {
        if (!active)
            return;

        if (Time.time - lastShown > duration)
            Hide();

        game_object.transform.position += motion * Time.deltaTime;
    }
}
