using UnityEngine;

public class MovingCactus : MonoBehaviour
{
    [Header("Properties")]
    public float speed = 5f;
    public float lifetime = 5f;

    [HideInInspector]
    private float timer = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
