using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public AudioClip[] grassClips;
    public AudioClip[] woodClips;

    [Range(0f, 1f)] public float grassVolume = 0.4f; 
    [Range(0f, 1f)] public float woodVolume = 1.0f;

    public float stepInterval = 0.5f;
    private float stepTimer;

    private AudioSource audioSource;
    private Rigidbody rb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        audioSource.loop = false;
    }

    void Update()
    {
        bool isInputting = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        Vector3 vel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        bool isActuallyMoving = vel.magnitude > 0.1f;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (isInputting && isActuallyMoving && isGrounded)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0;
            if (audioSource.isPlaying) audioSource.Stop();
        }
    }

    void PlayFootstep()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, 1.5f))
        {
            AudioClip selectedClip = null;
            float currentVolume = 1f; 

            if (hit.collider.CompareTag("Grass"))
            {
                if (grassClips.Length > 0)
                {
                    selectedClip = grassClips[Random.Range(0, grassClips.Length)];
                    currentVolume = grassVolume; 
                }
            }
            else if (hit.collider.CompareTag("Wood"))
            {
                if (woodClips.Length > 0)
                {
                    selectedClip = woodClips[Random.Range(0, woodClips.Length)];
                    currentVolume = woodVolume; 
                }
            }

            if (selectedClip != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(selectedClip, currentVolume);
            }
        }
    }
}