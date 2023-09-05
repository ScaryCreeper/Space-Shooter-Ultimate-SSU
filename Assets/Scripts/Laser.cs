using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private GameObject  _laser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement(); 
    }

    void calculateMovement()
    {
        transform.Translate(new Vector3(0, _speed, 0) * Time.deltaTime);

        if (transform.position.y > 8)
        {
            
            if (transform.parent !=  null)
            {
               Destroy(transform.parent.gameObject);
                Object.Destroy(_laser);
            }
            else
            {
                Object.Destroy(_laser);
            }
            
            
        
        }

    }

}
