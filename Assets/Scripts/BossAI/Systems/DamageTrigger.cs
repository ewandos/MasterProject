using System.Collections;
using UnityEngine;

public class DamageTrigger: MonoBehaviour
{
    public float sec = 0.5f;
    public GameObject EnemyCube;
    public GameObject bulletPrefab;
    public float bulletFlySpeed = 0.5f;
    public bool running;
    
    public void CreateDamageThingForSeconds()
    {
        
        if (!running)
        {
            StartCoroutine(LateCall(sec));
            running = true;
        }
    }

    public void createRangedAttack()
    {
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = EnemyCube.transform.position;
        
        newBullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * bulletFlySpeed, ForceMode.VelocityChange);
        Destroy(newBullet, 2);
    }
  
    IEnumerator LateCall(float seconds)
    {
        if (!EnemyCube.activeInHierarchy)
            EnemyCube.SetActive(true);
         
        yield return new WaitForSeconds(seconds);

        running = false;
        EnemyCube.SetActive(false);
    }
    
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}