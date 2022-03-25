using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float rangeAttackCD;
    private float lastRangedAttack;
    public Image RangeAttackCDImage;
    public Text RangeAttackCDText;

    public float projectileForce = 20f;

    private void Start()
    {
        lastRangedAttack = 0;
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (y >= 0.1)
            firePoint.rotation = Quaternion.Euler(0, 0, 0);
        else if (y <= -0.1)
            firePoint.rotation = Quaternion.Euler(0, 0, -180);
        else if  (x >= 0.1)
            firePoint.rotation = Quaternion.Euler(0, 0, -90);
        else if (x <= -0.1)
            firePoint.rotation = Quaternion.Euler(0, 0, 90);


        int timeLeft = ((int)lastRangedAttack + (int)rangeAttackCD) - (int)Time.time;
        if (Time.time - lastRangedAttack < rangeAttackCD)
        {
            RangeAttackCDImage.gameObject.SetActive(true);
            RangeAttackCDText.text = timeLeft.ToString() + "s";
            RangeAttackCDText.gameObject.SetActive(true);
        }
        else
        {
            RangeAttackCDImage.gameObject.SetActive(false);
            RangeAttackCDText.gameObject.SetActive(false);

            if (Input.GetKeyDown(KeyCode.K))
            {
                Shoot();
                lastRangedAttack = Time.time;
            }                      
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);
    }
}
