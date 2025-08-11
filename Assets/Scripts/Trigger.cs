using UnityEngine;

// Trigger 오브젝트의 스크립트
public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject player;     // player -> Drop_Rock

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("Drop_Rock SetActive");
            player.SetActive(true);   
        }
    }
}