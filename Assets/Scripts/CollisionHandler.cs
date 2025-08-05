using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything is looking good!");
                break;
            case "Finish":
                LoadNextLevel();
                break;
            default:
                ReloadLevel();
                break;
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
}
