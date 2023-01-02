
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject DirLight;
    public GameObject boss;
    //public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        DirLight.SetActive(true);
        boss.SetActive(true);
        //door.SetActive(true);
    }
}