using UnityEngine;

public class EnemyWeapon : Collidable
{
    public int damage;
    public int knockBack;

    protected override void OnCollide(Collider2D collider)
    {
        if(collider.name == "Player")
        {
            Damage damageObj = new Damage()
            {
                damageAmount = damage,
                origin = transform.position,
                knockBack = knockBack
            };
            collider.SendMessage("ReceiveDamage", damageObj);
        }
    }
}
