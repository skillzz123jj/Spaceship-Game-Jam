using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Animator animator;
    public GameObject model;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip landSound;

    private bool wasGrounded;
    private bool canPlayLandSound = false;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(EnableLandSoundAfterDelay(0.5f));
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("isWalking", true);
            transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

            if (Input.GetAxis("Horizontal") > 0) model.transform.rotation = Quaternion.Euler(0, 90, 0);
            else model.transform.rotation = Quaternion.Euler(0, -90, 0);

        }
        else animator.SetBool("isWalking", false);


        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Debug.Log("Jump");
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            animator.SetTrigger("Jump");
        }

        if (isGrounded()) animator.SetBool("isGrounded", true);
        else animator.SetBool("isGrounded", false);

        bool grounded = isGrounded();

        if (grounded && !wasGrounded)
        {
            if (landSound != null && audioSource != null && canPlayLandSound)
            {
                audioSource.PlayOneShot(landSound);
            }
        }

        wasGrounded = grounded;
    }

    private bool isGrounded()
    {
        bool grounded = Physics.Raycast(transform.position, -transform.up, .5f);
        Debug.DrawRay(transform.position, -transform.up * .5f, grounded ? Color.green : Color.red);
        return grounded;
    }

    private IEnumerator EnableLandSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canPlayLandSound = true;
    }
}
