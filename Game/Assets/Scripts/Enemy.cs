using UnityEngine;

public class Enemy : Mover
{
    public int expValue = 1;
    public float speed;
    public float triggerDistance = 0.16f;
    public float chaseDistance = 1f;
    protected bool chasing;
    protected bool collidingPlayer;
    protected Transform playerTransform;
    protected Vector3 originPosition;
    public ContactFilter2D filter2D;
    protected BoxCollider2D hitbox;
    protected Collider2D[] hitsArray = new Collider2D[10];
    public GameObject bloodEffect;

    protected override void Start()
    {
        base.Start();
        xSpeed = speed;
        ySpeed = speed * 0.75f;
        playerTransform = GameObject.Find("Player").transform;
        originPosition = transform.position;
        hitbox = gameObject.GetComponent<BoxCollider2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (Vector3.Distance(playerTransform.position, originPosition) < chaseDistance)
        {

            if (Vector3.Distance(playerTransform.position, originPosition) < triggerDistance) 
            chasing = true;

            if (chasing)
            {

                if (!collidingPlayer)
                {
                    UpdateMovement((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMovement(originPosition - transform.position);
            }
        }
        
        else
        {
            UpdateMovement(originPosition - transform.position);
            chasing = false;
        }

        collidingPlayer = false;
        hitbox.OverlapCollider(filter2D, hitsArray);

        for (int i = 0; i < hitsArray.Length; i++)
        {
            if (hitsArray[i] == null)
                continue;

            if(hitsArray[i].tag == "Fighter" && hitsArray[i].name == "Player")
            {
                collidingPlayer = true;
            }

            hitsArray[i] = null;
        }       
    }

    protected override void Die()
    {
        Destroy(gameObject);
        GameManager.instance.GrantExp(expValue);
        GameManager.instance.ShowText("+" + expValue + " xp", 
            30, new Color(56/255f, 199/255f, 1), GameObject.Find("Player").transform.position + Vector3.up * 0.16f, Vector3.up * 40, 1);
    }
    protected override void ReceiveDamage(Damage damageObj)
    {
        if (Time.time - lastImune > imuneTime)
        {
            lastImune = Time.time;
            healthPoint -= damageObj.damageAmount;
            pushDirection = (transform.position - damageObj.origin).normalized * damageObj.knockBack;
            Instantiate(bloodEffect, gameObject.transform);
            GameManager.instance.ShowText("-" + damageObj.damageAmount.ToString() + " hp", 20, Color.red, 
                transform.position + new Vector3(0, 0.1f, 0), Vector3.up * 60, 0.5f);
           
            if (healthPoint <= 0)
            {
                healthPoint = 0;
                Die();
            }
        }
    }

}
