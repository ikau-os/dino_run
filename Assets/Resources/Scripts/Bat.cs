using UnityEngine;

public class Bat : MonoBehaviour
{
    [Header("Properties")]
    public float speed;
    public float lifetime = 5f;

    [HideInInspector]
    private float timer = 0f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (audioSource.isPlaying && Time.timeScale == 0f)
        {
            audioSource.Stop();
        }

        transform.position += Vector3.left * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
