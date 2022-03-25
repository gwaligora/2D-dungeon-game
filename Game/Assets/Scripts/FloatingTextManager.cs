using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    private void Update()
    {
        foreach (FloatingText text in floatingTexts)
        {
            text.UpdateText();
        }
    }

    public void Show(string text, int fontSize, Color fontColor, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.text.text = text;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = fontColor;
        floatingText.game_object.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);
        if(txt == null)
        {
            txt = new FloatingText();
            txt.game_object = Instantiate(textPrefab);
            txt.game_object.transform.SetParent(textContainer.transform);
            txt.text = txt.game_object.GetComponent<Text>();

            floatingTexts.Add(txt);
        }
        return txt;
    }
}
