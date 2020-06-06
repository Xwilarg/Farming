using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/WeaponInfo", fileName = "WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    [Header("General info")]
    [Tooltip("The type of the weapon (also define it damage type)")]
    public WeaponType weaponType;

    [Tooltip("Damages the weapon do")]
    public int damage;

    [Tooltip("How the weapon will shoot")]
    public WeaponShootType shootType;

    [Tooltip("Time between the player can shoot")]
    public float intervalBetweenShoot;

    [Tooltip("Time before the projectile/laser disappear.")]
    public float lifetime;

    [Tooltip("Number of projectiles/lasers shoot")]
    public int nbShoot;

    [Tooltip("Deviation angle of the shoot")]
    public float deviationAngle;

    [Header("Projectile info")]
    [Header("ONLY FOR MECHANICAL WEAPONS")]
    [Tooltip("Mass of the projectile (The heavier, the faster it'll drop to the ground)")]
    public float projectileMass;

    [Tooltip("Speed of the projectile")]
    public int projectileSpeed;
}
