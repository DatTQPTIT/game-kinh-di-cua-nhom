using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class JumpscareTrigger : MonoBehaviour
{
    public float jumpscareTime = 2.0f;
    public string loseSceneName = "LoseScene";
    public AudioSource jumpscareSound;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {

        if (!hasTriggered && other.CompareTag("Player"))
        {
            StartCoroutine(PlayJumpscare(other.gameObject));
        }
    }

    IEnumerator PlayJumpscare(GameObject playerObj)
    {
        hasTriggered = true;

        var playerMovement = playerObj.GetComponent<PlayerController>();
        if (playerMovement != null) playerMovement.enabled = false;

        if (JumpscareManager.Instance != null)
        {
            JumpscareManager.Instance.ShowJumpscare();
        }

        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(loseSceneName);
    }
}