using UnityEngine;

public class Boss3 : Enemy
{
    public GameObject walls;
    public GameObject bossKidPrefab;
    public Transform KidSpawnPoint;

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
        GameObject bossKid = Instantiate(bossKidPrefab, KidSpawnPoint.position, KidSpawnPoint.rotation);
    }

    protected override void Die()
    {
        if (walls != null)
            walls.SetActive(false);

        SpawnKid();
        KidSpawnPoint.position += new Vector3(0.16f, 0, 0);
        SpawnKid();
        base.Die();
    }

}