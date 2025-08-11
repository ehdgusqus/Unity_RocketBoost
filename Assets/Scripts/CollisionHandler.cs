using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Player Rocket 스크립트
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f; // 다음 레벨로 넘어갈 때 delay 시간 설정
    [SerializeField] AudioClip successSFX;  // Sound Effects
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    private bool isControllable = true;     // 충돌 처리 중복 방지 변수
    private bool isColliderbool = true;     // 충돌 처리 여부 설정 변수

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }


    private void RespondToDebugKeys()
    {
        // wasPressedThisFrame은 키가 현재 프레임에서 여러 번 눌렸을 때 한번만 true를 반환. 다음 프레임부터는 false

        // 다음 씬으로 넘어가기
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Debug.Log("L key is pressed");
            LoadNextLevel();
        }
        // 충돌 무시하기
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            Debug.Log("C key is pressed");
            isColliderbool = !isColliderbool;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // OnCollisionEnter()가 한 번만 호출되게 하기 위한 장치 
        if (!isControllable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":  // Launch Pad의 tag    
                // Debug.Log("Everything is looking good!");
                break;

            case "Finish":   // Landing Pad의 tag
                StartSuccessSequence();
                break;

            default:
                // 충돌이 일어나지 않게 하는 키보드(c key)를 눌렀을 경우
                if (!isColliderbool) { return; }

                StartCrashSequence();
                break;
        }
    }

    // 게임 실패 했을 때를 처리하는 메서드
    private void StartCrashSequence()
    {
        isControllable = false;    // 충돌을 한 번만 처리하기 위해 false로 변경

        // audio
        audioSource.Stop();        // 기존에 재생되는 오디오 정지
        audioSource.PlayOneShot(crashSFX);

        // particle
        successParticles.Play();

        GetComponent<Movement>().enabled = false;   // Movement 스크립트 비활성화
        Invoke("ReloadLevel", levelLoadDelay);      // Invoke(함수이름, 지연 시간)
    }

    // Landing Pad에 도착했을 경우를 처리하는 메서드
    void StartSuccessSequence()
    {
        isControllable = false;

        // audio
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);

        // particle
        crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    // 현재 씬을 다시 로드하는 메서드
    void ReloadLevel()
    {
        /*
        SceneManager.GetActiveScene(): 현재 활성화 되어 있는(플레이어가 보고 있는) 씬을 가져온다.
        .buildIndex: 현재 씬의 빌드 인덱스 번호를 가져온다.
        File -> Build Profiles에서 전체 인덱스 확인
        */
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    // 다음 씬으로 넘어가도록 하는 메서드
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        currentScene++;

        // sceneCountInBuildSettings은 빌드에 포함된 씬의 총 개수
        if (currentScene == SceneManager.sceneCountInBuildSettings)
        {
            currentScene = 0;
        }

        SceneManager.LoadScene(currentScene);
    }
}