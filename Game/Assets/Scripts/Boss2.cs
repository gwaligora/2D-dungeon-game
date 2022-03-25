using UnityEngine;

public class Boss2 : Enemy
{
    public GameObject walls;
    public GameObject bossKidPrefab;
    public float spawnKidCD;
    private float lastSpawnKid;
    public float[] batSpeed;
    public float[] batDistance;
    public Transform[] bats;

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        if (Vector3.Distance(playerTransform.position, originPosition) < chaseDistance)
        {

            if (Vector3.Distance(playerTransform.position, originPosition) < triggerDistance)
                chasing = true;

            if (chasing)
            {
                if (Time.time - lastSpawnKid > spawnKidCD)
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

    private void Update()
    {
        for(int i = 0; i < bats.Length; i++)
        {
            bats[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * batSpeed[i])
                * batDistance[i], Mathf.Sin(Time.time * batSpeed[i]) * batDistance[i], 0);
        }


    }

    private void SpawnKid()
    {
        GameObject zombieKid = Instantiate(bossKidPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }

    protected override void Die()
    {
        if (walls != null)
            walls.SetActive(false);

        base.Die();
    }
}