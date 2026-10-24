using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dino : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip scoreSound;
    public AudioClip gameOver;

    [Header("Properties")]
    public float jumpForce;

    [Header("Objects")]
    public GameObject scoreObject;
    public GameObject highScoreObject;

    [Header("Scripts")]
    public Summary summary;

    private Rigidbody2D rb;

    private int score = 0;
    private int highScore = 0; 
    private int platformContacts = 0; 
    
    private bool isJumping = false;

    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreObject.GetComponent<TextMeshProUGUI>().text = "High Score: " + highScore.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) 
        || Input.GetKeyDown(KeyCode.UpArrow) 
        || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Jump();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platformContacts++;
            isJumping = false;
            animator.speed = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Score"))
        {
            score++;
            audioSource.clip = scoreSound;
            audioSource.Play();
            scoreObject.GetComponent<TextMeshProUGUI>().text = score.ToString();

            SaveAndUpdate();

            if (collision.transform.parent != null) 
            {
                GameObject parentObj = collision.transform.parent.gameObject;
                if (parentObj.CompareTag("Cactus"))
                {
                    summary.cactuses++;
                }
                else if (parentObj.CompareTag("Bat"))
                {
                    summary.bats++;
                }
            }
        }
        else if (collision.gameObject.CompareTag("Cactus") || collision.gameObject.CompareTag("Bat"))
        {
            PlayerPrefs.Save();

            audioSource.clip = gameOver;
            audioSource.Play();
            
            summary.score = score;
            summary.ShowSummary();
        }
        else if (collision.gameObject.CompareTag("Stooby"))
        {
            int totalDestroyed = 0;

            GameObject[] group1 = GameObject.FindGameObjectsWithTag("Cactus");
            totalDestroyed += group1.Length;
            foreach (GameObject obj in group1)
            {
                Destroy(obj);
            }

            GameObject[] group2 = GameObject.FindGameObjectsWithTag("Bat");
            totalDestroyed += group2.Length;
            foreach (GameObject obj in group2)
            {
                Destroy(obj);
            }

            GameObject[] group3 = GameObject.FindGameObjectsWithTag("Stooby");
            foreach (GameObject obj in group3)
            {
                Destroy(obj);
            }

            score += totalDestroyed;
            audioSource.clip = scoreSound;
            audioSource.Play();
            scoreObject.GetComponent<TextMeshProUGUI>().text = score.ToString();

            SaveAndUpdate();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platformContacts--;
            
            if (platformContacts <= 0)
            {
                platformContacts = 0;
                isJumping = true;
                animator.speed = 0f;
            }
        }
    }

    public void Jump()
    {
        if (!isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
        }
    }

    public void SaveAndUpdate()
    {
        if (score > highScore)
        {
            highScore = score; 
            PlayerPrefs.SetInt("HighScore", highScore); 
            highScoreObject.GetComponent<TextMeshProUGUI>().text = "High Score: " + highScore.ToString();
        }

        PlayerPrefs.Save();
    }
}
