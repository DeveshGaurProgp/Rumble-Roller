using UnityEngine;
using System.Collections.Generic;

public class MissilesManager : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private SpawnManager spawnManagerScript;

    private float repeatRate = 1f;

    public GameObject homingMissilePrefab;
    public GameObject player;

    public List<GameObject> missiles = new List<GameObject>();

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        InvokeRepeating("LaunchHomingMissile", 0, repeatRate);
    }

    private void LaunchHomingMissile()
    {
        if (spawnManagerScript.inSceneEnemy.Count > 0 && playerControllerScript.hasHomingPowerUp)
        {
            float missileSpeed = 50f;
            for (int i = 0; i < spawnManagerScript.inSceneEnemy.Count; i++)
            {
                if (spawnManagerScript.inSceneEnemy[i] != null)
                {
                    Vector3 missileInstanceLoc = (spawnManagerScript.inSceneEnemy[i].transform.position - player.transform.position).normalized;
                    GameObject newMissile = Instantiate(homingMissilePrefab, player.transform.position + (missileInstanceLoc * 1.5f), homingMissilePrefab.transform.rotation);
                    missiles.Add(newMissile);
                    Vector3 move = (spawnManagerScript.inSceneEnemy[i].transform.position - newMissile.transform.position).normalized;
                    Rigidbody missileRb = missiles[i].GetComponent<Rigidbody>();
                    missileRb.AddForce(move * missileSpeed, ForceMode.Impulse);
                }
            }
        }
    }
}
