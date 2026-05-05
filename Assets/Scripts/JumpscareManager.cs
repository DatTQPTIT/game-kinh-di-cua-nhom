using UnityEngine;

public class JumpscareManager : MonoBehaviour
{
    public static JumpscareManager Instance; 
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        this.gameObject.SetActive(false);
    }

    public void ShowJumpscare()
    {
        this.gameObject.SetActive(true);
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play(); 
        }
    }
}