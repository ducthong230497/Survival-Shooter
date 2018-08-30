using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelHelper : MonoBehaviour
{
    public void RestartLevel()
    {
        PlayerAnimationSystem.isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
