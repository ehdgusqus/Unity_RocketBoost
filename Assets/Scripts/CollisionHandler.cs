using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    private bool isControllable = true;
    private bool isColliderbool = true;

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
            case "Friendly":
                Debug.Log("Everything is looking good!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                // 충돌이 일어나지 않게 하는 키보드(c key)를 눌렀을 경우
                if (!isColliderbool) { return; }

                StartCrashSequence();
                break;
        }
    }
    private void StartCrashSequence()
    {
        isControllable = false;

        // audio
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);

        // particle
        successParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);    // Invoke(함수이름, 지연 시간)
    }

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
    void ReloadLevel()
    {
        /*
        SceneManager.GetActiveScene(): 현재 활성화 되어 있는(플레이어가 보고 있는) 씬을 가져온다.
        .bulidIndex: 현재 씬의 빌드 인덱스 번호를 가져온다.
        */
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        currentScene++;

        if (currentScene == SceneManager.sceneCountInBuildSettings)
        {
            currentScene = 0;
        }

        SceneManager.LoadScene(currentScene);
    }
}