using UnityEngine;

public class Chest : Collectable
{
    public Sprite collectedChest;
    public int silverAmount = 10;
    public bool grantKey = false;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = collectedChest;
            GameManager.instance.ShowText("+" +  silverAmount + " silver", 20, Color.white, transform.position, Vector3.up * 40, 1f); 
            GameManager.instance.silver += silverAmount;
            
            if (grantKey)
            {
                GameManager.instance.ShowText("+1 golden key", 20, Color.yellow, transform.position + Vector3.up * 0.16f, Vector3.up * 40, 1f);
                GameManager.instance.player.hasKey = true;
            }   
        }
    }
}
