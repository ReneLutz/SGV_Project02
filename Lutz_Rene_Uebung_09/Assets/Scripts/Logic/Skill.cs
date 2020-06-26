using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Skill", order = 1)]
public class Skill : ScriptableObject
{
    public int Damage;

    public float Range;

    public int ProjectileSpeed;

    public Projectile ProjectilePrefab;

    public Sprite Icon;
}
