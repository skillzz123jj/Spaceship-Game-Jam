using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform player;          
    public Transform planetCenter;    
    public float distance = 5f;       
    public float height = 2f;         
    public float rotationSpeed = 5f; 
    public float followSpeed = 10f;   

    void LateUpdate()
    {
        Vector3 playerUp = (player.position - planetCenter.position).normalized;

        Vector3 offset = -player.forward * distance + playerUp * height;
        Vector3 desiredPos = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        Quaternion targetRot = Quaternion.LookRotation(player.position - transform.position, playerUp);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }
}
