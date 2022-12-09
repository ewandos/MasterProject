using UnityEngine;

public class GlowstickController : MonoBehaviour
{
    
    public GameObject glowstickGO;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject go = Instantiate(glowstickGO);
            go.transform.position = transform.position + new Vector3(0, 0.5f);
            go.transform.rotation = new Quaternion(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), 0);
            go.GetComponent<Rigidbody>().AddForce(transform.forward * 3, ForceMode.Impulse);
        }
    }
}