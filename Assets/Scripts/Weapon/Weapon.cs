using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Weapon : MonoBehaviour
{   
    [SerializeField] private WeaponScriptable weapon;
    [SerializeField] private Transform shootSlot;
    
    public bool isReload = false;
    public bool isShooting = false;

    public int currentAmmo;
    public bool shoot = false;
    private PlayerInputHandler InputHandler;

    private GetMousePosition getMouse;

    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        getMouse = transform.root.GetComponent<GetMousePosition>();
        InputHandler = PlayerInputHandler.Instance;
        currentAmmo = weapon.maxAmmo;
    }

    private void Update()
    {
        ReloadInput();
        ShootInput();
    }

    public void Reload()
    {
        if(isReload)
        {
            return;
        }
        if(isShooting)
        {
            return;
        }
        StartCoroutine(ReloadCorutine());
    }

    private void ReloadInput()
    {
        if(InputHandler.ReloadInput)
        {
            Reload();
        }
    }

    private void ShootInput()
    {
        //shoot = weapon.isAutomatic ? InputHandler.FireInput : InputHandler.FireInputPressed;
        
        if(getMouse.targetFound)
        {
           Shoot();
        }
        else
        {
            if(shoot)
            {
                Shoot();
            }
        }
        
       
    }

    private void Shoot()
    {
        if(isReload)
        {
            return;
        }
        if(isShooting)
        {
            return;
        }
        if(currentAmmo <= 0)
        {
            Reload();
            return;
        }
        StartCoroutine(ShootCorutine());
    }

    protected virtual IEnumerator ShootCorutine()
    {
        isShooting = true;
        currentAmmo--;
        GameObject bullet = Instantiate(weapon.bullet, shootSlot.position, shootSlot.rotation);
        bullet.GetComponent<Bullet>().damage = weapon.damage;
        bullet.transform.Rotate(0, Random.Range(-weapon.spread, weapon.spread), 0);
        yield return new WaitForSeconds(weapon.fireRate);
        isShooting = false;
    }

    private IEnumerator ReloadCorutine()
    {
        isReload = true;
        yield return new WaitForSeconds(weapon.reloadTime);
        isReload = false;
        currentAmmo = weapon.maxAmmo;
    }
}
