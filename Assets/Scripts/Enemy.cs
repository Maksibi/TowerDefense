using SpaceShooter;
using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(AIController))]
public class Enemy : MonoBehaviour
{
    public void Use(EnemyAsset asset)
    {
        SpriteRenderer spriteRenderer = transform.Find("View").GetComponent<SpriteRenderer>();
        spriteRenderer.color = asset.color;
        spriteRenderer.transform.localScale = asset.spriteScale;

        spriteRenderer.GetComponent<Animator>().runtimeAnimatorController = asset.animations;


        GetComponent<SpaceShip>().Use(asset);
    }
}
