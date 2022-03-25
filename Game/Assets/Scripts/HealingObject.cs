using UnityEngine;

public class HealingObject : Collidable
{
    private int healingAmount = 1;
    public float healingCooldown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            if (Time.time - lastHeal > healingCooldown)
            {
                lastHeal = Time.time;
                GameManager.instance.player.Heal(healingAmount);
            }
        }
    }


}
