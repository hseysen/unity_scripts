using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int scene_count;
    public static GameManager instance;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LevelFinished()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next != scene_count) SceneManager.LoadScene(next);
        else Application.Quit();
    }
}
