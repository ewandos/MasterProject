﻿using System;
using UnityEngine;


public class Melee : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float knockback = 30f;
    [SerializeField] private float firerate = 1.5f;
    
    [SerializeField] private GameObject impactEffect;
    //[SerializeField] private AudioClip MeleeAudio;

    [SerializeField] private float nexTimeToFire = 0f;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)
            && Time.time >= nexTimeToFire)
        {
            nexTimeToFire = Time.time + firerate;
            Attack();
        }
    }

    public void Attack()
    {
        Animator anim = GetComponent<Animator>();
        //AudioSource audio = GetComponent<AudioSource>();
        StatTracker.Instance.MeleeAttackPerformed();
        //audio.PlayOneShot(MeleeAudio);
        anim.SetTrigger("Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target" && Time.time >= nexTimeToFire)
        {
            //other.GetComponent<Animator>().SetTrigger("Hit");
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Instantiate(impactEffect,
                new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z),
                other.transform.rotation);
        }
    }
}