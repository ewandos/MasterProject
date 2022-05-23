using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float damage = 10f;
    [SerializeField]
    private float range = 100f;
    [SerializeField] 
    private float knockback = 30f;
    [SerializeField] 
    private float firerate = 15f;

    [SerializeField] private float maxAmunition = 10f;
    [SerializeField] private float amunition = 10f;
    [SerializeField] private float amunitionCarried = 100f;
    
    
    
    [SerializeField]
    private Camera fpsCam;
    //[SerializeField]
    //private ParticleSystem muzzleFlash;
    //[SerializeField] private GameObject impactEffect;

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
            muzzleFlash.Flash();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void AddAmmo(float amount)
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
        Debug.Log("shooting");
        //muzzleFlash.Play();
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
                //emit event
                hit.rigidbody.AddForce(-hit.normal * knockback);
            }

            //GameObject impactGo =  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGo, 1);
        }
        
    }
}
