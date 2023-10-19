using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory 
{
    /***********************SINGLETON SECTION*****************************/
    //Step 1 private static instance
    private static BulletFactory instance;

    //Step 2 Make the constructor private

    private BulletFactory()
    {
        //Add the construction stuff
        SetupBulletFactory();


    }

    //Step 3 public static creational method
    public static BulletFactory Instance()
    {
        return instance ??= new BulletFactory();
    }

    /***********************SINGLETON SECTION****************************/

    GameObject _playerBulletPrefab, _enemyBulletPrefab;

    private  void SetupBulletFactory()
    {
        _playerBulletPrefab = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        _enemyBulletPrefab = Resources.Load<GameObject>("Prefabs/EnemyBullet");
    }

    [SerializeField]
    private Sprite _playerBulletSprite, _enemyBulletSprite;
    // Start is called before the first frame update

    public GameObject CreateBullet(BulletTypes type)
    {


        //bullet.transform.parent = GetComponentInChildren<Transform>();
        GameObject bullet;



        switch (type)
        {
            case BulletTypes.PLAYERBULLET:
                bullet = MonoBehaviour.Instantiate(_playerBulletPrefab);

                bullet.name = "PlayerBullet";

               // bullet.GetComponent<SpriteRenderer>().sprite = _playerBulletSprite;
                bullet.GetComponent<BulletBehavior>().SetDirection(Vector3.up);

                bullet.GetComponent<BulletBehavior>().SetType(BulletTypes.PLAYERBULLET);

                bullet.AddComponent<PlayerBullet>();
                break;
            case BulletTypes.ENEMYBULLET:
                bullet = MonoBehaviour.Instantiate(_enemyBulletPrefab);

                bullet.name = "EnemyBullet";

               // bullet.GetComponent<SpriteRenderer>().sprite = _enemyBulletSprite;
                bullet.GetComponent<BulletBehavior>().SetDirection(Vector3.down);

                bullet.GetComponent<BulletBehavior>().SetType(BulletTypes.ENEMYBULLET);

                bullet.transform.RotateAround(Vector3.forward, Mathf.Deg2Rad * 180);

                break;

            default:
                Debug.LogError("Incorrect type of bullet");
                return null;
                break;
        }

        return bullet;

    }

    
}
