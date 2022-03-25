using UnityEngine;


public class Destroyable : Fighter
{
    public bool grantsMonay;
    public int silverAmount = 1;

    protected override void Die()
    {
        if (grantsMonay)
        {
            int i = Random.Range(0, 100);

            if (i <= 25)
            {
                GameManager.instance.ShowText("+" + silverAmount + " silver", 20, Color.white, transform.position, Vector3.up * 40, 1f);
                GameManager.instance.silver += silverAmount;
            }
        }
        Destroy(gameObject);
    }



}
