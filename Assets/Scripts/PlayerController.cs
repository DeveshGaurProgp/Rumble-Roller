using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;

    private float speed = 200f;
    private float powerUpStrength = 15f;
    private float rotationSpeed = 100f;
    private float smashStrength = 30f;

    private GameObject focalPoint;
    public GameObject powerUpIndicator;

    private Rigidbody playerRb;
    private Collider playerCollider;
    public PhysicsMaterial bouncyMaterial;

    private bool hasPushPowerUp = false;
    public bool hasHomingPowerUp = false;
    public bool hasSmashPowerUp = false;
    public bool hasPowerUp = false;
    public bool gameOver = false;
    private bool isOnGround = true;
    private bool isSmashing = false;

    private Vector3 offset  = new Vector3(0, -0.5f, 0);

    private LayerMask enemyLayer;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        focalPoint = GameObject.Find("Focal Point");

        enemyLayer = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = (focalPoint.transform.forward).normalized;

        playerRb.AddForce(moveDirection * speed * verticalInput * Time.deltaTime);

        powerUpIndicator.transform.position = transform.position + offset;
        powerUpIndicator.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if(isOnGround && hasSmashPowerUp && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SmashAttack());
        }

        if (transform.position.y < -10)
        {
            gameOver = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasPowerUp)
        {
            if (other.gameObject.CompareTag("Push"))
            {
                hasPushPowerUp = true;
                Destroy(other.gameObject);
                PowerUpManager();
            }

            if (other.gameObject.CompareTag("Homing"))
            {
                hasHomingPowerUp = true;
                Destroy(other.gameObject);
                PowerUpManager();
            }

            if (other.gameObject.CompareTag("Smash"))
            {
                hasSmashPowerUp = true;
                Destroy(other.gameObject);
                PowerUpManager();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPushPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 hitEnemy = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(hitEnemy * powerUpStrength, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            playerRb.constraints = RigidbodyConstraints.None;
            playerCollider.sharedMaterial = bouncyMaterial;
        }
        if(collision.gameObject.CompareTag("Ground") && isSmashing)
        {
            isSmashing = false;
            Collider[] enemyInRange = Physics.OverlapSphere(transform.position, 20, enemyLayer);

            foreach (Collider enemy in enemyInRange)
            {
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                Vector3 hitEnemy = enemy.transform.position - transform.position;
                enemyRb.AddForce(hitEnemy * smashStrength * 10);
            }
        }
    }

    IEnumerator PowerUpTimer()
    {
        if(hasPushPowerUp)
        {
            yield return new WaitForSeconds(10);
            hasPushPowerUp = false;
            hasPowerUp = false;
        }
        else if(hasHomingPowerUp)
        {
            yield return new WaitForSeconds(7);
            hasHomingPowerUp = false;
            hasPowerUp = false;
        }
        else if(hasSmashPowerUp)
        {
            yield return new WaitForSeconds(3);
            hasSmashPowerUp = false;
            hasPowerUp = false;
        }
        powerUpIndicator.SetActive(false);
    }

    private void PowerUpManager()
    {
        hasPowerUp = true;
        powerUpIndicator.SetActive(true);
        StartCoroutine(PowerUpTimer());
    }

    IEnumerator SmashAttack()
    {
        playerRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        playerRb.AddForce(Vector3.up * smashStrength, ForceMode.Impulse);
        playerCollider.sharedMaterial = null;
        isOnGround = false;
        yield return new WaitForSeconds(0.5f);

        playerRb.AddForce(Vector3.down * (smashStrength * 3), ForceMode.Impulse);
        isSmashing = true;
    }
}