using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Instance.GetComponent<AudioSource>().Pause();
        }
    }
}
