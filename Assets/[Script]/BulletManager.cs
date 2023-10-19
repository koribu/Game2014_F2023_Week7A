using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    /****************************SINGLETON SECTION******************************/
    //Step 1 private Static Instance
    private static BulletManager instance;

    //Step 2 Make the Constructor function private
    private BulletManager()
    {
        //Our constructing functions
        SetupBulletManager();
      
    }

    //Step 3 Public Static Creational Method -- Instance(Gateway to the class)
    public static BulletManager Instance()
    {
        //Check if we already created that instance before. if not, create and instance of that object
        if(instance == null)
        {
            instance = new BulletManager();
        }
        //return instance of that object
        return instance;
    }

    /*****************************SINGLETON SECTION*****************************/


    List<Queue<GameObject>> _bulletPools = new List<Queue<GameObject>>();

    [SerializeField]
    int _playerBulletTotal = 50;
    [SerializeField]
    int _enemyBulletTotal = 50;

    //Instantiate a bullet pool

    private void SetupBulletManager()
    {
        for(int count = 0; count < (int)BulletTypes.NUMBEROFBULLETTYPES; count++)
        {
            _bulletPools.Add(new Queue<GameObject>());
        }

        BuildBulletPool();
    }

    void BuildBulletPool()
    {
        //create bullets
        //add them to a list


        for(int i = 0; i < _bulletPools.Count; i++)
        {
            _bulletPools[i].Enqueue(BulletFactory.Instance().CreateBullet((BulletTypes)i));

        }
/*
        for(int i = 0; i < _playerBulletTotal; i++)
        {
            GameObject bullet = BulletFactory.Instance().CreateBullet(BulletTypes.PLAYERBULLET); //_factory.CreateBullet(BulletTypes.PLAYERBULLET);
            _playerBulletPool.Enqueue(bullet);
        }
        for (int i = 0; i < _enemyBulletTotal; i++)
        {
            GameObject bullet = BulletFactory.Instance().CreateBullet(BulletTypes.ENEMYBULLET);
            _enemyBulletPool.Enqueue(bullet);
        }*/
    }

    
    public GameObject GetBullet(BulletTypes type)
    {    

        if (_bulletPools[(int)type].Count < 1)
            _bulletPools[(int)type].Enqueue(BulletFactory.Instance().CreateBullet(type));

        GameObject bullet = _bulletPools[(int)type].Dequeue();

        bullet.SetActive(true);

        return bullet;

       

      /*  switch (type)
        {
            case BulletTypes.PLAYERBULLET:
                if (_playerBulletPool.Count < 1)
                    _playerBulletPool.Enqueue(BulletFactory.Instance().CreateBullet(BulletTypes.PLAYERBULLET));

                bullet = _playerBulletPool.Dequeue();

                break;
            case BulletTypes.ENEMYBULLET:
                if (_enemyBulletPool.Count < 1)
                    _enemyBulletPool.Enqueue(BulletFactory.Instance().CreateBullet(BulletTypes.ENEMYBULLET));

                bullet = _enemyBulletPool.Dequeue();

                break;
            default:
                Debug.LogError("There is no bullet at that type in the pool");
                return null;
            break;
        }*/

        bullet.SetActive(true);
        return bullet;

        
    }

    public void ReturnBullet(GameObject bullet, BulletTypes type)
    {
        bullet.SetActive(false);

        _bulletPools[(int)type].Enqueue(bullet);


 /*       switch(type)
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
        }*/
    }
}
