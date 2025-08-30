using UnityEngine;

public class SphericalGravity : MonoBehaviour
{
    public Transform planetCenter;
    public float gravity = 30f;
    public float alignSpeed = 5f;
    Rigidbody rb;

    void Awake() => rb = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        Vector3 toCenter = (planetCenter.position - transform.position).normalized;
        rb.AddForce(toCenter * gravity, ForceMode.Acceleration);

        // Smoothly align character “up” to point away from center
        Quaternion targetRot = Quaternion.FromToRotation(transform.up, -toCenter) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, alignSpeed * Time.fixedDeltaTime);
    }
}