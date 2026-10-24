using Unity.VisualScripting;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [Header("Objects")]
    public GameObject pauseMenu;

    [Header("Scripts")]
    public Manager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {   
        if (Time.timeScale > 0f)
        {
            pauseMenu.SetActive(true);
            manager.PauseGame();
        }
        else if (Time.timeScale == 0f)
        {
            pauseMenu.SetActive(false);
            manager.ResumeGame();
        }
    }
}
