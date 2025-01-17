using UnityEngine;

[CreateAssetMenu(fileName = "WeaponVariant", menuName = "ScriptableObjects/WeaponScriptable", order = 1)]
public class WeaponScriptable : ScriptableObject
{
    [Header("Weapon")]
    public GameObject bullet;
    public GameObject muzzleFlash;
    public GameObject impactEffect;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float spread = 5f;
    

    [Header("Ammo")]
    public int maxAmmo = 10;
    public float reloadTime = 1f;
    

    
    public bool isAutomatic = false;
    
    
    
}
