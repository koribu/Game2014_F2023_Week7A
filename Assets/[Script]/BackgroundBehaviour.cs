using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private Boundry _boundry;

/*    [SerializeField]
    private Vector3 _spawnPosition;*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - _speed * Time.deltaTime, transform.position.z);

        if(transform.position.y < _boundry.min)
        {
            //transform.position = _spawnPosition;
            transform.position = new Vector3(transform.position.x, _boundry.max, transform.position.z);

        }
    }
}
