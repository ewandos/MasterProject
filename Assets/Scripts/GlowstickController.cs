using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GlowstickController : MonoBehaviour
{
    public int glowstickCount = 10;
    public GameObject glowstickGO;
    private PlayerMovement playerMovement;
    public event Action<int> updatedGlowstickCount;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void AddGlowstick(int amount)
    {
        glowstickCount += amount;
        updatedGlowstickCount?.Invoke(glowstickCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && glowstickCount > 0)
        {
            glowstickCount--;
            GameObject go = Instantiate(glowstickGO);
            go.transform.position = playerMovement.transform.position + new Vector3(0, 0.5f);
            go.transform.rotation = new Quaternion(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), 0);
            go.GetComponent<Rigidbody>().AddForce(playerMovement.velocity * 1.5f + transform.forward * 3, ForceMode.Impulse);
            updatedGlowstickCount?.Invoke(glowstickCount);
        }
    }
}