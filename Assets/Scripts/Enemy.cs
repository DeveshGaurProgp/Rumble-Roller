using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;

    public float speed;
    public float forceStrength;
    private float zBound = 12f;
    private float xBound = 13f;

    private PlayerController playerControllerScript;
    private SpawnManager spawnManagerScript;


    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager> ();
    }

    void Update()
    {
        EnemyBehavior();

        if (transform.position.y < -10)
        {
            spawnManagerScript.inSceneEnemy.Remove(this.gameObject);
            Destroy(gameObject);
        }

        if (transform.position.x > xBound || transform.position.x < -xBound || transform.position.z > zBound || transform.position.z < -zBound)
        {
            spawnManagerScript.inSceneEnemy.Remove(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerControllerScript.hasPowerUp)
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 hitPlayer = collision.gameObject.transform.position - transform.position;
            playerRb.AddForce(hitPlayer * forceStrength * Time.timeScale);
        }
    }

    void EnemyBehavior()
    {
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(playerDirection * speed * Time.timeScale);
    }
}
