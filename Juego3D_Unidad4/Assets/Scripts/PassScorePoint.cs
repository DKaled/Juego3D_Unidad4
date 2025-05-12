using UnityEngine;

public class PassScorePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pass Score Point");
        GameManager.singleton.AddScore(1);
    }
}
