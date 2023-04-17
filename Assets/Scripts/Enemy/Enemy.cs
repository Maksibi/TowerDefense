using TowerDefense;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AIController))]
public class Enemy : TowerDefense.Destructible
{
    [SerializeField] private int damage = 1;

    private void Awake()
    {
        AIController controller = GetComponent<AIController>();

        controller.OnEndPath.AddListener(DamagePlayer);
    }
    public override void Use(EnemyAsset asset)
    {
        base.Use(asset);

        SpriteRenderer spriteRenderer = transform.Find("View").GetComponent<SpriteRenderer>();
        spriteRenderer.color = asset.color;
        spriteRenderer.transform.localScale = asset.spriteScale;

        spriteRenderer.GetComponent<Animator>().runtimeAnimatorController = asset.animations;


        //GetComponent<SpaceShip>().Use(asset);

        damage = asset.damage;

        Score = asset.score;
    }

    private void DamagePlayer()
    {
        Player.Instance.TakeDamage(damage);
    }
}
