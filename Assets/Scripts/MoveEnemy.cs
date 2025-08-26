using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MoveCar : MonoBehaviour
{
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float moveDistance = 150f;
    private Transform carTransform;
    private Vector3 startPosition;

    void Start()
    {
        carTransform = GetComponent<Transform>();
        startPosition = carTransform.position;
    }
    void FixedUpdate()
    {
        ProcessMove();
    }

    // x: 50~200
    private void ProcessMove()
    {
        float xOffset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);

        transform.position = startPosition + Vector3.right * xOffset;

    }
}
