using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    GameObject _bulletPrefab;
    Queue<GameObject> _playerBulletPool = new Queue<GameObject>();
    Queue<GameObject> _enemyBulletPool = new Queue<GameObject>();

    [SerializeField]
    int _playerBulletTotal = 50;
    [SerializeField]
    int _enemyBulletTotal = 50;

    BulletFactory _factory;


    // Start is called before the first frame update
    void Start()
    {
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        _factory = FindAnyObjectByType<BulletFactory>();

        BuildBulletPool();
    }

    //Instantiate a bullet pool

    void BuildBulletPool()
    {
        //create bullets
        //add them to a list

        for(int i = 0; i < _playerBulletTotal; i++)
        {
            GameObject bullet = _factory.CreateBullet(BulletTypes.PLAYERBULLET);
            _playerBulletPool.Enqueue(bullet);
        }
        for (int i = 0; i < _enemyBulletTotal; i++)
        {
            GameObject bullet = _factory.CreateBullet(BulletTypes.ENEMYBULLET);
            _enemyBulletPool.Enqueue(bullet);
        }
    }

    
    public GameObject GetBullet(BulletTypes type)
    {
     
        GameObject bullet;

        switch(type)
        {
            case BulletTypes.PLAYERBULLET:
                if (_playerBulletPool.Count < 1)
                    _playerBulletPool.Enqueue(_factory.CreateBullet(BulletTypes.PLAYERBULLET));

                bullet = _playerBulletPool.Dequeue();

                break;
            case BulletTypes.ENEMYBULLET:
                if (_enemyBulletPool.Count < 1)
                    _enemyBulletPool.Enqueue(_factory.CreateBullet(BulletTypes.ENEMYBULLET));

                bullet = _enemyBulletPool.Dequeue();

                break;
            default:
                Debug.LogError("There is no bullet at that type in the pool");
                return null;
            break;
        }

        bullet.SetActive(true);
        return bullet;

        
    }

    public void ReturnBullet(GameObject bullet, BulletTypes type)
    {
        bullet.SetActive(false);

        switch(type)
        {
            case BulletTypes.PLAYERBULLET:
                _playerBulletPool.Enqueue(bullet);
                break;
            case BulletTypes.ENEMYBULLET:
                _enemyBulletPool.Enqueue(bullet);
                break;
            default:
                Debug.LogError("Return bullet object type doesnt exist");
                break;
        }
    }
}
