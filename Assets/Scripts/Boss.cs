using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject minion;
    public float repeatRate = 5f;

    private SpawnManager spawnManagerScript;

    void Start()
    {
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        InvokeRepeating("SpawnMinion", 3f, repeatRate);
    }

    void SpawnMinion()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject newMinion = Instantiate(minion, transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);
            spawnManagerScript.inSceneEnemy.Add(newMinion);
        }
    }
}
