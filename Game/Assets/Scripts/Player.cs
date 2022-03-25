using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : Mover
{
    public bool imunity;
    public float playerImmuneTime;
    private new Rigidbody2D rigidbody;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    private float lastDash;
    public float dashCD;
    public Image dashCDImage;
    public Text dashCDText;
    public Animator animator;
    public GameObject bloodEffect;
    public GameObject dashEffect;
    public bool canMove = true;
    public bool hasKey;
    public Image keyImage;
    Vector2 movement;

    protected override void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(9, 10, true);
        hasKey = false;

        dashCDImage.gameObject.SetActive(false);
        dashCDText.gameObject.SetActive(false);
        lastDash = Time.time - dashCD;
        direction = 1;
        
        imuneTime = playerImmuneTime;

        base.Start();      
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(canMove)
        UpdateMovement(new Vector3(movement.x, movement.y, 0));
    }

    public void Heal(int healingAmount)
    {
        if (healthPoint < maxHealthPoint)
        {
            healthPoint += healingAmount;
            GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position + Vector3.up * 0.16f, Vector3.up * 30, 0.5f);
        }
    }

    protected override void ReceiveDamage(Damage damageObj)
    {  
        if (canMove == false)
            return;
        if (GameManager.instance.player.imunity)
            return;
        if (Time.time - lastImune > imuneTime)
        {
            lastImune = Time.time;
            healthPoint -= damageObj.damageAmount;
            pushDirection = (transform.position - damageObj.origin).normalized * damageObj.knockBack;
            Instantiate(bloodEffect, gameObject.transform);
            GameManager.instance.ShowText("-" + damageObj.damageAmount.ToString() + " hp", 20, Color.red, transform.position + new Vector3(0, 0.1f, 0), Vector3.up * 60, 0.5f);
            CharacterInfo.instance.UpdateInfo();

            if (healthPoint <= 0)
            {
                healthPoint = 0;
                Die();
            }
        }
    }
        
    private void Update()
    {
        if (hasKey == true)
            keyImage.gameObject.SetActive(true);
        else 
            keyImage.gameObject.SetActive(false);

        if (canMove == false)
            GameManager.instance.moveableHUD.SetActive(false);
        else
            GameManager.instance.moveableHUD.SetActive(true);

        int timeLeft = ((int)lastDash + (int)dashCD) -(int)Time.time ;

        if(Time.time - lastDash < dashCD)
        {
            dashCDImage.gameObject.SetActive(true);
            dashCDText.text = timeLeft.ToString() + "s";
            dashCDText.gameObject.SetActive(true);
        }
        else
        {
            dashCDImage.gameObject.SetActive(false);
            dashCDText.gameObject.SetActive(false);
        }

        if (direction == 0)
        {
            if (Input.GetKey(KeyCode.A))
                if (Input.GetKeyDown(KeyCode.Space))
                    direction = 1;
            if (Input.GetKey(KeyCode.W))
                if (Input.GetKeyDown(KeyCode.Space))
                    direction = 2;
            if (Input.GetKey(KeyCode.D))
                if (Input.GetKeyDown(KeyCode.Space))
                    direction = 3;
            if (Input.GetKey(KeyCode.S))
                if (Input.GetKeyDown(KeyCode.Space))
                    direction = 4;

        }
        else 
        {
            if (Time.time - lastDash >= dashCD)
            {
                lastDash = Time.time;
                Dash(direction);

            }

            else if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rigidbody.velocity = Vector2.zero;
                Physics2D.IgnoreLayerCollision(9, 10, false);
                imunity = false;
            }

            else {
                
                dashTime -= Time.deltaTime; 
            }
        }
    }

    private void Dash(int direction)
    {
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        imunity = true;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        if (direction == 1)
            rigidbody.velocity = Vector2.left * dashSpeed;
        else if (direction == 2)
            rigidbody.velocity = Vector2.up * dashSpeed * 0.75f;
        else if (direction == 3)
            rigidbody.velocity = Vector2.right * dashSpeed;
        else if (direction == 4)
            rigidbody.velocity = Vector2.down * dashSpeed * 0.75f;
    }

    protected override void Die()
    {
        canMove = false;
        GameManager.instance.deathMenuAnim.SetTrigger("show");
    }

    public void Respawn()
    {
        maxHealthPoint = 10;
        healthPoint = maxHealthPoint;
        lastImune = Time.time;
        canMove = true;
        pushDirection = Vector3.zero;
    }
}
