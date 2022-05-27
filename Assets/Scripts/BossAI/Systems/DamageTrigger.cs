using System.Collections;
using UnityEngine;

public class DamageTrigger: MonoBehaviour
{
    public float sec = 1f;
    public GameObject EnemyCube;
    public GameObject bulletPrefab;
    public float bulletFlySpeed = 0.5f;
    
    public void CreateDamageThingForSeconds()
    {
        StartCoroutine(LateCall(sec));
    }

    public void createRangedAttack()
    {
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = EnemyCube.transform.position;
        
        newBullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * bulletFlySpeed, ForceMode.VelocityChange);
        Destroy(newBullet, 2);
        //Debug.Log("bullet should be kaputt");
    }
  
    IEnumerator LateCall(float seconds)
    {
        if (!EnemyCube.activeInHierarchy)
            EnemyCube.SetActive(true);
         
        yield return new WaitForSeconds(seconds);
  
        EnemyCube.SetActive(false);
    }
}