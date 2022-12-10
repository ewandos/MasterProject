using System;
using Drawing;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float range = 100f;
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

    [SerializeField] private TextMeshProUGUI ammoUI;

    float speed = 0.5f;

    private void Start()
    {
        amunition = maxAmunition;
        SetUI();
    }

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

    private void FixedUpdate()
    {
        Transform from = transform;
        Transform to = fpsCam.transform;
        transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, speed);
    }

    public void AddAmmo(int amount)
    {
        amunitionCarried += amount;
        SetUI();
    }
    void Reload()
    {
        if (amunitionCarried <= 0) return;
        int requiredReloadAmount = maxAmunition - amunition;
        int possibleReload = Math.Max(0, amunitionCarried - (amunitionCarried - requiredReloadAmount));
        
        amunitionCarried -= possibleReload;
        amunition += possibleReload;
        
        SetUI();
    }
    
    void Shoot()
    {
        StatTracker.Instance.RangeAttackPerformed();
        amunition--;
        SetUI();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            HealthSystem target = hit.transform.GetComponent<HealthSystem>();

            if (target != null)
                target.TakeDamage(damage);
            
            GameObject impactEffectInstantiate = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            impactEffectInstantiate.transform.parent = hit.transform;
        }
        
    }

    void SetUI()
    {
        ammoUI.text = (amunition.ToString() + '/' + amunitionCarried);
    }
}
