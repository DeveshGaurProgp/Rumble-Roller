using UnityEngine;

public class HomingMissiles : MonoBehaviour
{
    private float pushStrength = 15f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 look = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(look * pushStrength, ForceMode.Impulse);
        }
    }
}