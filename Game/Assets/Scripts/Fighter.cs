using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int healthPoint = 10;
    public int maxHealthPoint = 10;
    public float knockbackResistance = 0.2f;
    protected float imuneTime = 0.2f;
    protected float lastImune;
    protected Vector3 pushDirection;

    protected virtual void ReceiveDamage(Damage damageObj)
    {
        if (GameManager.instance.player.imunity)
            return;

        if(Time.time - lastImune > imuneTime)
        {
            lastImune = Time.time;
            healthPoint -= damageObj.damageAmount;
            pushDirection = (transform.position - damageObj.origin).normalized * damageObj.knockBack;

            GameManager.instance.ShowText("-" + damageObj.damageAmount.ToString() + " hp", 20, Color.red, 
                transform.position + new Vector3(0, 0.1f, 0), Vector3.up * 60, 0.5f);


            if(healthPoint <= 0)
            {
                healthPoint = 0;
                Die();
            }
        }
    }

    protected virtual void Die()
    {

    }
}
