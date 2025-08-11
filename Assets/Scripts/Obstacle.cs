using UnityEngine;

// Scene 3
// Obstacle Object Script
public class Obstacle : MonoBehaviour
{
    [SerializeField] private bool isMovingUpwards;      // true면 위로, false면 아래로 이동 
    [SerializeField] private float moveDistance = 90f;  // 이동 거리
    [SerializeField] private float moveSpeed = 20f;     // 이동 속도

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        MoveUpDown();
    }

    // Object를 y축 기준으로 왕복해서 움직이게 하는 메서드
    void MoveUpDown()
    {
        // Mathf.PingPong(t, length): 0에서 length, length에서 0으로 왕복으로 t 시간만큼 움직인다.

        float yOffset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);

        Vector3 direction = (isMovingUpwards) ? Vector3.up : Vector3.down;

        transform.position = startPosition + direction * yOffset;
    }
}