using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyJumpscare : MonoBehaviour
{
    public float detectRange = 1.5f;
    public float jumpscareDuration = 2.0f;
    public string loseSceneName = "LoseScene";

    public GameObject jumpscareUI; 
    public AudioSource jumpscareAudio; 
    public Transform player;

    private bool isScaring = false;

    void Update()
    {
        if (isScaring || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectRange)
        {
            StartCoroutine(ShowImageJumpscare());
        }
    }

    IEnumerator ShowImageJumpscare()
    {
        isScaring = true;
        PlayerController move = player.GetComponent<PlayerController>();
        if (move != null) move.enabled = false;

        if (jumpscareUI != null)
        {
            jumpscareUI.SetActive(true);
        }

        if (jumpscareAudio != null)
        {
            jumpscareAudio.Play();
        }

        yield return new WaitForSeconds(jumpscareDuration);

        SceneManager.LoadScene(loseSceneName);
    }
}