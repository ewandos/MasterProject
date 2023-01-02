using UnityEngine;

public class PivotController : MonoBehaviour
{
    public Camera camera;
    public float speed = 0.5f;
    
    private void FixedUpdate()
    {
        Transform from = transform;
        Transform to = camera.transform;
        transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, speed);
    }
}
