using System;
using Drawing;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float range = 100f;
    [SerializeField] 
    private float knockback = 30f;
    [SerializeField] 
    private float firerate = 1.5f;

    [SerializeField] private int maxAmunition = 10;
    [SerializeField] private int amunition = 10;
    [SerializeField] private int amunitionCarried = 100;
    
    
    
    [SerializeField]
    private Camera fpsCam;

    [SerializeField] 
    private GameObject impactEffect;

    [SerializeField]
    private MuzzleFlashController muzzleFlash;
    
    [SerializeField] private float nexTimeToFire = 0f;
    
    
    void Update()
    {
        if (Input.GetButton("Fire1") 
            && Time.time >= nexTimeToFire
            && amunition > 0)
        {
            nexTimeToFire = Time.time + 1f / firerate;
            Shoot();
            muzzleFlash.Effect();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void AddAmmo(int amount)
    {
        amunitionCarried += amount;
    }
    void Reload()
    {
        amunitionCarried -= (maxAmunition - amunition);
        amunition = maxAmunition;
    }
    
    void Shoot()
    {
        StatTracker.Instance.RangeAttackPerformed();
        amunition--;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            
            Debug.Log(hit.transform.name);

            HealthSystem target = hit.transform.GetComponent<HealthSystem>();

            if (target != null)
            {
                //emit event
                target.TakeDamage(damage);
            }
            
            if (hit.rigidbody != null)
            {
                //das geht gerade nicht weil raycast den rigidbody des targets auf is kinematic braucht
                hit.rigidbody.AddForce(-hit.normal * knockback);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        
    }
}
