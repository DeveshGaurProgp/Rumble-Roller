using UnityEngine;
using System.Collections.Generic;

public class DestroyOutOfBound : MonoBehaviour
{
    private float zBound = 12f;
    private float xBound = 13f;

    private MissilesManager MissilesManagerScript;
    private SpawnManager spawnManagerScript;


    void Start()
    {
        MissilesManagerScript = GameObject.Find("MissilesManager").GetComponent<MissilesManager>();
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        if (transform.position.x > xBound || transform.position.x < -xBound || transform.position.z > zBound || transform.position.z < -zBound)
        {
            MissilesManagerScript.missiles.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        if(spawnManagerScript.enemyCount == 0)
        {
            MissilesManagerScript.missiles.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
