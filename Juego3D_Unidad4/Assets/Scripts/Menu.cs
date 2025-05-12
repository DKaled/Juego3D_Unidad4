using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame1() {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame1() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
