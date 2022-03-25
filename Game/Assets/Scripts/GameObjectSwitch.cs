using UnityEngine;

public class GameObjectSwitch : Collidable
{
    public GameObject Object;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name == "Player")
            Object.SetActive(true);
    }
}
