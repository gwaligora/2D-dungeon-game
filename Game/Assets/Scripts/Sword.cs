using UnityEngine;

public class Sword : Collidable
{
    public int[] damage = {1, 3, 5, 7, 10, 12, 14, 17, 20 ,23 };
    public float[] knockBack = { 2.0f, 2.4f, 2.8f, 3.2f, 3.5f, 3.8f, 4f, 4.3f, 4.6f, 4f };

    public int swordLevel = 0;
    public SpriteRenderer spriteRenderer;
 
    private Animator animator;
    private float attackCooldown = 0.2f;
    private float lastAttack;

    protected override void Start()
    {
        base.Start();
        Physics2D.IgnoreLayerCollision(8, 12, true);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.sprite = GameManager.instance.swordsSprites[0]; 
    }

    protected override void Update()
    {
        base.Update();
        float y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Vertical", y);

        if (Input.GetKeyDown(KeyCode.J))
        {
            if(Time.time - lastAttack > attackCooldown)
            {
                lastAttack = Time.time;
                Attack();
            }
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
    }

    protected override void OnCollide(Collider2D collider)
    {
        if(collider.tag =="Fighter" && collider.name != "Player")
        {
            Damage damageObj = new Damage()
            {
                damageAmount = damage[swordLevel],
                origin = transform.position,
                knockBack = knockBack[swordLevel]
            };
            collider.SendMessage("ReceiveDamage", damageObj);
        }
    }

    public void UpgradeSword()
    {
        swordLevel++;
        spriteRenderer.sprite = GameManager.instance.swordsSprites[swordLevel];
    }
}
