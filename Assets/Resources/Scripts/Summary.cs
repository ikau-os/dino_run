using TMPro;
using UnityEngine;

public class Summary : MonoBehaviour
{
    // Note To Self: A variable without 'public' or 'private' is automatically declared 'private'
    [Header("Components")]
    public TextMeshProUGUI cactusCounter;
    public TextMeshProUGUI scoreObject;
    public TextMeshProUGUI batsCounter;

    [Header("Scripts")]
    public Manager manager;

    [HideInInspector] public float score;
    [HideInInspector] public float cactuses;
    [HideInInspector] public float bats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShowSummary()
    {
        // Summary popup
        manager.StopBackgroundMusic();
        gameObject.SetActive(true);
        scoreObject.text = "Score: " + score.ToString();
        cactusCounter.text = "Cactuses Jumped: " + cactuses.ToString();
        batsCounter.text = "Bats Avoided: " + bats.ToString();

        manager.PauseGame();
        manager.isGameFinished = true;
    }
}
