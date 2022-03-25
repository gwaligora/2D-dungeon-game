using UnityEngine;

public class projectile : Collidable
{
    public GameObject breakEffect;
    public bool myChoice;
    public int myDamageAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(10, 13, true);
        if (collision.collider.tag == "Blocking"
            || collision.collider.tag == "NPC" || collision.collider.tag == "environment")
            Instantiate(breakEffect, gameObject.transform);;
        Destroy(gameObject, 0.15f);
    }

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.tag == "Fighter" && collider.name != "Player")
        {
            if (myChoice)
            {
                Damage damageObj = new Damage()
                {
                damageAmount = myDamageAmount,
                origin = transform.position,
                knockBack = 1
                };
                
            collider.SendMessage("ReceiveDamage", damageObj);
            }
            else
            {
                Damage damageObj = new Damage()
                {
                    damageAmount = GameManager.instance.sword.damage[GameManager.instance.sword.swordLevel] + 1,
                    origin = transform.position,
                    knockBack = 1
                };
                collider.SendMessage("ReceiveDamage", damageObj);
            }
        }
    }
}
