using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [Header("Cactus Settings")]
    public float minCactusTimeOfSpawn = 0.3f;
    public float maxCactusTimeOfSpawn = 10f;

    [Space(15)]
    public GameObject[] cactus;
    
    private float nextCactusSpawnTime;

    [Header("Bat Settings")]
    public float minBatTimeOfSpawn = 0.3f;
    public float maxBatTimeOfSpawn = 10f;

    [Space(15)]
    public GameObject bat;
    
    private float nextBatSpawnTime;

    [Space(15)]
    [SerializeField] private float minBatY = 1.0f;
    [SerializeField] private float maxBatY = 4.0f;

    [Header("Stooby Settings")]
    public float minStoobyTimeOfSpawn = 0.3f;
    public float maxStoobyTimeOfSpawn = 10f;

    [Space(15)]
    public float stoobyProbabilty = 0.5f;

    [Space(15)]
    public AudioClip spawnSound1;
    public AudioClip spawnSound2;

    [Space(15)]
    public GameObject stooby;
    
    private float nextStoobySpawnTime;

    [Space(15)]
    [SerializeField] private float minStoobyY = 1.0f;
    [SerializeField] private float maxStoobyY = 4.0f;
    private AudioSource audioSource;

    void Start()
    {
        nextCactusSpawnTime = Time.timeSinceLevelLoad + Random.Range(minCactusTimeOfSpawn, maxCactusTimeOfSpawn);
        nextBatSpawnTime = Time.timeSinceLevelLoad + Random.Range(minBatTimeOfSpawn, maxBatTimeOfSpawn);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > nextCactusSpawnTime)
        {
            SpawnCactus();
            nextCactusSpawnTime = Time.timeSinceLevelLoad + Random.Range(minCactusTimeOfSpawn, maxCactusTimeOfSpawn);
        }

        if (Time.timeSinceLevelLoad > nextBatSpawnTime)
        {
            SpawnBat();
            nextBatSpawnTime = Time.timeSinceLevelLoad + Random.Range(minBatTimeOfSpawn, maxBatTimeOfSpawn);
        }

        if (Time.timeSinceLevelLoad > nextStoobySpawnTime)
        {
            if (Random.Range(0f, 100f) <= stoobyProbabilty)
            {
                SpawnStooby();
                nextStoobySpawnTime = Time.timeSinceLevelLoad + Random.Range(minStoobyTimeOfSpawn, maxStoobyTimeOfSpawn);
            }
        }
    }

    private void SpawnCactus()
    {
        GameObject randomCactus = cactus[Random.Range(0, cactus.Length)];
        Instantiate(randomCactus, randomCactus.transform.position, Quaternion.identity);
    }

    private void SpawnBat()
    {
        float randomY = Random.Range(minBatY, maxBatY);
        Vector3 batPosition = new Vector3(bat.transform.position.x, randomY, bat.transform.position.z);
        Instantiate(bat, batPosition, Quaternion.identity);
    }

    private void SpawnStooby()
    {
        float randomY = Random.Range(minStoobyY, maxStoobyY);
        Vector3 stoobyPosition = new Vector3(stooby.transform.position.x, randomY, stooby.transform.position.z);
        Instantiate(stooby, stoobyPosition, Quaternion.identity);

        if (Random.Range(0f, 100f) <= 0.5f)
        {
            audioSource.clip = spawnSound1;
        }
        else
        {
            audioSource.clip = spawnSound2;
        }

        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
