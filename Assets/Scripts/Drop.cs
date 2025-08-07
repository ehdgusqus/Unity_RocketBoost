using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private float customGravity = -30f;
    MeshRenderer mr;
    Rigidbody rb;

    void Awake()
    {
        if (gameObject != null)
        {
            // Debug.Log("gameObject.SetActive(false)");
            gameObject.SetActive(false);    
        }
    }

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;  // 기본 중력 비활성화
        mr.enabled = true;      // MeshRenderer 활성화
    }

    void FixedUpdate()
    {
        // ForceMode.Acceleration -> 질량(Mass)을 무시하고, Rigidbody에 지속적인 가속도를 적용
        rb.AddForce(Vector3.up * customGravity, ForceMode.Acceleration);
    }
}
