using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    Boundry _offLimit;

    BulletTypes _type;

    Vector3 _direction;
    [SerializeField]
    float _speed = 5;
    // Start is called before the first frame update

    BulletManager _manager;
    void Start()
    {
        _manager = FindAnyObjectByType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBoundries();
    }

    protected void CheckBoundries()
    {
        if (transform.position.y > _offLimit.max || transform.position.y < _offLimit.min)
        {
            _manager.ReturnBullet(gameObject,_type);
        }
    }

    protected void Move()
    {
        transform.position += _direction * Time.deltaTime * _speed;
    }

    public void SetDirection(Vector3 dir)
    {
        _direction = dir;
    }

    void Damage()
    {

    }

    public void SetType(BulletTypes type)
    {
        _type = type;
    }
}
