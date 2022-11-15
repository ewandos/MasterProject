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


    private void Start()
    {
        amunition = maxAmunition;
        SetUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) 
            && Time.time >= nexTimeToFire
            && amunition > 0)
        {
            nexTimeToFire = Time.time + firerate;
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
        SetUI();
    }
    void Reload()
    {
        amunitionCarried -= (maxAmunition - amunition);
        amunition = maxAmunition;
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
            
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        
    }

    void SetUI()
    {
        ammoUI.text = (amunition.ToString() + '/' + amunitionCarried);
    }
}
