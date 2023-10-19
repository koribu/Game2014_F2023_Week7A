using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    float _speedVertical, _speedHorizontal;

    [SerializeField]
    Boundry _verticalBoundry;

    [SerializeField]
    Boundry _horizontalBoundry;

    Material _enemyMaterial;

    GameObject _bulletPrefab;

    BulletManager _bulletManager;

    [SerializeField]
    Transform _bulletPoint;
    int _count = 0;
    // Start is called before the first frame update
    void Start()
    {
       _enemyMaterial = GetComponent<SpriteRenderer>().material;
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        _bulletManager = FindAnyObjectByType<BulletManager>();

        ResetEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (Mathf.PingPong(Time.time * _speedHorizontal, _horizontalBoundry.max - _horizontalBoundry.min)  
            + _horizontalBoundry.min , transform.position.y - _speedVertical * Time.deltaTime, 0);



        if(_verticalBoundry.min > transform.position.y)
        {
            ResetEnemy();


        }

        _count++;
        if (_count > 5)
        {
            GameObject bullet = _bulletManager.GetBullet(BulletTypes.ENEMYBULLET);
            bullet.transform.position = _bulletPoint.position;
            
            _count = 0;
        }
    }


    void ResetEnemy()
    {
        _speedVertical = Random.Range(1, 4);
        _speedHorizontal = Random.Range(3, 7);

        _enemyMaterial.color = new Color(Random.Range(0,150), Random.Range(0, 150), Random.Range(0, 150));

        GetComponent<SpriteRenderer>().color = _enemyMaterial.color;

        transform.position = new Vector3(Random.Range(_horizontalBoundry.min, _horizontalBoundry.max), _verticalBoundry.max, transform.position.z);
    }
}
