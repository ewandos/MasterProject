using System.Collections;
using UnityEngine;

public class DamageTrigger: MonoBehaviour
{
    public float sec = 0.5f;
    public GameObject EnemyCube;
    public GameObject EnemyBulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject Player;
    public float bulletFlySpeed = 2f;
    public bool running;
    
    public void CreateDamageThingForSeconds()
    {
        
        if (!running)
        {
            StartCoroutine(LateCall(sec));
            running = true;
        }
    }
    
    public Vector3 getPositionToChargeTo(float chargeDistance)
    {
        return (Player.transform.position - transform.position).normalized * chargeDistance;
    }
    
    public Vector3 getPositionToChargeAwayFrom(float chargeDistance)
    {
        return (transform.position - Player.transform.position).normalized * chargeDistance;
    }

    public void createRangedAttack()
    {
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = EnemyBulletSpawnPoint.transform.position;
        
        //angle bullet to player
        newBullet.GetComponent<Rigidbody>().AddForce(
            (Player.transform.position - newBullet.transform.position) 
            * bulletFlySpeed, ForceMode.VelocityChange);
        
        
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