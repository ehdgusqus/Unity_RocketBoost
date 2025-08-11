using UnityEngine;
using UnityEngine.InputSystem;

// Player Rocket Script
public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] private float thrustStrength = 100f;
    [SerializeField] private float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Input을 감지할 준비가 되었다.
        thrust.Enable();    
        rotation.Enable();
    }

    void OnDisable()
    {
        thrust.Disable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void StartThrusting()
    {
        // local 좌표계 기준으로 힘을 적용
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();  // 회전 입력값을 읽어오는 역할

        // Left (-1f)
        if (rotationInput < 0)
        {
            RotateLeft();
            // Debug.Log("move left");
        }
        // Right (1f)
        else if (rotationInput > 0)
        {
            RotateRight();
            // Debug.Log("move right");
        }
        // Nothing(0)
        else
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationStrength);

        if (!leftThrustParticles.isPlaying)
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationStrength);

        if (!rightThrustParticles.isPlaying)
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Play();
        }
    }

    // Object에 Rotation 적용하는 메서드
    private void ApplyRotation(float rotationThisFrame)
    {
        // 물리 엔진의 회전을 비활성화 하여 스크립트를 통한 회전만 이루어지게 설정 
        rb.freezeRotation = true;   // Rigidbody의 rotation을 비활성화 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;  // Rigidbody의 rotation을 활성화
    }
}
