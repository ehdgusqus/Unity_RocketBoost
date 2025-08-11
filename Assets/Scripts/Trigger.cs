using UnityEngine;

// Scene 2
// Trigger Object Script
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