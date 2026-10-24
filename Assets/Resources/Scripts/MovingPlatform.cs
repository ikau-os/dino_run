using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Properties")]
    public float speed = 5f;
    public float endPointPositionX;

    private Transform Ground;
    private Vector3 endPoint;

    private Vector3 spawnPoint;

    void Awake()
    {
        spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Start()
    {
        endPoint = new Vector3(endPointPositionX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);

        if (transform.position.x < endPoint.x)
        {
            transform.position = new Vector3(spawnPoint.x, transform.position.y, transform.position.z);
        }
    }
}
