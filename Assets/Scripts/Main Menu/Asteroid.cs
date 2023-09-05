using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private GameObject asteroid;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject explosionPrefab;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        asteroid.transform.Rotate(0f, 0f, 0.1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

         if (other.tag == "Laser")
        {
            Instantiate(explosion, asteroid.transform);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 1.19f);
            
        }
    }

}
