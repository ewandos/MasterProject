using System;
using Menu;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public LayerMask layerMask;
    [SerializeField] private int damage = 10;
    [SerializeField] private float range = 100f;
    [SerializeField] private float firerate = 1.5f;
    [SerializeField] private int maxAmunition = 10;
    public int amunition = 10;

    public int amunitionCarried = 100;

    [SerializeField] private Camera fpsCam;

    [SerializeField] private GameObject impactEffect;
    
    [SerializeField] private GameObject creepImpactEffect;

    [SerializeField] private GunFeedback shootFeedback;
    [SerializeField] private MMF_Player reloadFeedback;

    private float nexTimeToFire = 0f;

    [SerializeField] private GameObject model;

    public event Action<int> updatedAmmo; 
    public event Action<int> updatedCarriedAmmo;
    public event Action<bool> toggledGun;

    private void Start()
    {
        amunition = maxAmunition;
    }

    private void OnDisable()
    {
        model.SetActive(enabled);
        toggledGun?.Invoke(enabled);
    }

    private void OnEnable()
    {
        model.SetActive(enabled);
        toggledGun?.Invoke(enabled);
    }

    void Update()
    {
        if (Input.GetButton("Fire1") 
            && Time.time >= nexTimeToFire
            && !PauseMenu.GameIsPaused)
        {
            if (amunition > 0)
            {
                nexTimeToFire = Time.time + 1f / firerate;
                Shoot();
                shootFeedback.PlayShootEffect();
            }
            else
            {
                shootFeedback.PlayDryShootEffect();
                nexTimeToFire = Time.time + 1f / (firerate * 0.65f);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void AddAmmo(int amount)
    {
        amunitionCarried += amount;
        updatedCarriedAmmo?.Invoke(amunitionCarried);
    }
    void Reload()
    {
        if (amunitionCarried <= 0) return;
        int requiredReloadAmount = maxAmunition - amunition;
        int difference = amunitionCarried - requiredReloadAmount;
        
        amunitionCarried = Mathf.Max(0, difference);
        amunition += requiredReloadAmount + Mathf.Min(0, difference);
        updatedAmmo?.Invoke(amunition);
        updatedCarriedAmmo?.Invoke(amunitionCarried);
        reloadFeedback.PlayFeedbacks();
    }
    
    void Shoot()
    {
        StatTracker.Instance.RangeAttackPerformed();
        amunition--;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
        {
            IHealth target = hit.transform.GetComponent<IHealth>();

            if (target != null)
            {
                GameObject impactEffectInstantiate = Instantiate(creepImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impactEffectInstantiate.transform.parent = hit.transform;
                target.TakeDamage(damage);
            }
            else
            {
                GameObject impactEffectInstantiate = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impactEffectInstantiate.transform.parent = hit.transform;
            }
        }

        updatedAmmo?.Invoke(amunition);
    }
}
