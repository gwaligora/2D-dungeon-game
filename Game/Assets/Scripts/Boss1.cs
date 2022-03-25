using UnityEngine;

public class Boss1 : Enemy
{
    public GameObject portal;
    public GameObject zombieKidPrefab;
    public GameObject portalParticles;
    public GameObject portalVisuals;
    public float spawnKidCD;
    private float lastSpawnKid;

    protected override void Start()
    {
        base.Start();
        portal.GetComponent<BoxCollider2D>().enabled = false;
    }

    protected override void FixedUpdate()
    {
        if (Vector3.Distance(playerTransform.position, originPosition) < chaseDistance)
        {
            if (Vector3.Distance(playerTransform.position, originPosition) < triggerDistance)
                chasing = true;

            if (chasing)
            {
                if(Time.time - lastSpawnKid > spawnKidCD)
                {
                    lastSpawnKid = Time.time;
                    SpawnKid();
                }
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

            if (hitsArray[i].tag == "Fighter" && hitsArray[i].name == "Player")
            {
                collidingPlayer = true;
            }

            hitsArray[i] = null;
        }
    }

    private void SpawnKid()
    {
        GameObject zombieKid = Instantiate(zombieKidPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }

    protected override void Die()
    {
        if (portal != null)
        {
            portal.GetComponent<BoxCollider2D>().enabled = true;
            portalParticles.SetActive(true);
            portalVisuals.SetActive(true);
        }
        base.Die();
    }
}
