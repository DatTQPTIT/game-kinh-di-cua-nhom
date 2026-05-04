using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyJumpscare : MonoBehaviour
{
    [Header("Cài đặt")]
    public float detectRange = 1.1f;         // Sát mức 1.0 bạn muốn
    public float animationDuration = 2.0f;
    public string jumpscareTrigger = "Jumpscare";

    [Header("Tham chiếu")]
    public Transform player;
    public Transform playerCamera;

    private Animator anim;
    private bool isScaring = false;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerCamera == null && player != null) playerCamera = player.GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        if (isScaring || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Debug để bạn theo dõi khoảng cách hiện tại trong Console
        // Debug.Log("Khoảng cách: " + distance);

        if (distance <= detectRange)
        {
            StartCoroutine(TriggerJumpscare());
        }
    }

    IEnumerator TriggerJumpscare()
    {
        isScaring = true;

        // 1. ĐÓNG BĂNG TỨC THÌ
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        PlayerController move = player.GetComponent<PlayerController>();
        if (move != null) move.enabled = false;

        // Khóa vị trí tuyệt đối (không cho trượt vật lý)
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // 2. XOAY QUÁI ĐỐI DIỆN PLAYER
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        // 3. KÍCH HOẠT ANIMATION
        anim.SetTrigger(jumpscareTrigger);

        // 4. KHÓA GÓC NHÌN CAMERA SÁT MẶT
        float elapsed = 0;
        while (elapsed < animationDuration)
        {
            // Nhắm vào đầu quái (1.6m là tầm mắt)
            Vector3 targetHead = transform.position + Vector3.up * 1.6f;
            Vector3 dir = targetHead - playerCamera.position;

            if (dir != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                // Xoay Camera cực nhanh và khóa chặt không cho quay chuột
                playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, targetRot, Time.deltaTime * 20f);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("LoseScene");
    }
}