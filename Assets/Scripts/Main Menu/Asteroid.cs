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
    private SpawnManager _spawnManager;
    [SerializeField]
    private AudioSource _audioSourceExplosion;
    [SerializeField]
    private AudioClip explosionSFX;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        _audioSourceExplosion.GetComponent<AudioSource>();
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
            _audioSourceExplosion.clip = explosionSFX;
            _audioSourceExplosion.Play();   
            Instantiate(explosion, asteroid.transform);
            Destroy(other.gameObject);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 2f);
            
        }
    }

}
