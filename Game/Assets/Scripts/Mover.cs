using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;
    private Vector3 originalSize;

    protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMovement(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);

        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, knockbackResistance);
        
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Creature", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
        Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Creature", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
