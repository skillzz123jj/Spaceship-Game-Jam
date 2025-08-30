using UnityEngine;

public class RocketUI : MonoBehaviour
{
    public GameObject rocketCanvas;

    private void Start()
    {
        rocketCanvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rocketCanvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rocketCanvas.SetActive(false);
        }
    }
}
