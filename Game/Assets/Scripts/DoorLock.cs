using UnityEngine;

public class DoorLock : Collidable
{
    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name != "Player")
            return;

        if (GameManager.instance.player.hasKey)
        {
            Destroy(gameObject);
            GameManager.instance.player.hasKey = false;
        }

    }

}
