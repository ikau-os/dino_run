using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Private Variables:
    // floats
    float _memoryTimeScale = 0f;

    // bools
    bool isPaused = false;
    bool canMusicPlay = true;

    private AudioSource audioSource;

    // Public Variables
    [Header("Audio")]
    public AudioClip backgroundMusic;

    [Header("Properties")]
    [SerializeField] private float reduceVolumeBy = 4f;

    [Header("Scripts")]
    public Dino dino;

    [HideInInspector]
    // bools
    public bool isGameFinished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && audioSource.clip == backgroundMusic && canMusicPlay)
        {
            audioSource.Play();
        }
    }

    public void PauseGame()
    {
        if (isGameFinished == false)
        {
            _memoryTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            isPaused = true;

            audioSource.volume /= reduceVolumeBy;
        }
    }

    public void ResumeGame()
    {
        if (!isGameFinished)
        {
            Time.timeScale = _memoryTimeScale;
            audioSource.volume *= reduceVolumeBy;
            isPaused = false;
        }
    }

    public void ToggleGameState()
    {
        if (isPaused == true)
        {
            ResumeGame();
        }
        else if (isPaused == false)
        {
            PauseGame();
        }
    }

    public void ResetScene()
    {
        int buildNum = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SceneManager.LoadScene(buildNum);
    }

    public void GoToScene(int sceneNumber)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneNumber);
    }

    public void CloseGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            dino.SaveAndUpdate();
        }
    }

    void OnApplicationQuit()
    {
        dino.SaveAndUpdate();
    }

    public void StopBackgroundMusic()
    {
        if (audioSource.isPlaying && audioSource.clip == backgroundMusic && canMusicPlay)
        {
            canMusicPlay = false;
            audioSource.Stop();
        }
    }
}
