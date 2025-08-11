using UnityEngine;

// Scene 2
// Drop_Rock Variant Script
public class Drop : MonoBehaviour
{
    [SerializeField] private float customGravity = -30f;
    MeshRenderer mr;
    Rigidbody rb;

    void Awake()
    {
        // 만약 바위가 활성화 되어 있다면 비활성화
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
        mr.enabled = true;      // MeshRenderer 활성화 -> Inspector에서 Drop_Rock MeshRenderer가 비활성화 되어 있음
    }

    /*
    FixedUpdate()를 쓰는 이유
    FixedUpdate()와 Update() 비교
    */
    void FixedUpdate()
    {
        // AddFore(Vector3 force, ForceMode mode) -> Rigidbody component에 힘을 가하는 데 사용
        // ForceMode.Acceleration -> 오브젝트의 질량(Mass)을 무시하고, Rigidbody에 지속적인 가속도를 적용
        rb.AddForce(Vector3.up * customGravity, ForceMode.Acceleration);
    }
}
