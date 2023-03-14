using UnityEngine;

public class StandUp : MonoBehaviour
{
    private Rigidbody2D rigid;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        rigid = transform.root.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
    transform.up = Vector2.up;
        float xMotion = rigid.velocity.x;

        if (xMotion > 0.1f) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }
}
