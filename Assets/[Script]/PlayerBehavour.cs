using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavour : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;

    [SerializeField]
    private Boundry _boundry;

    Vector3 _destination;

    Camera _camera;

    [SerializeField]
    bool _isUsingMobile = false;

    GameManager _gameManager;

    GameObject _bulletPrefab;
    int _count = 0;

    BulletManager _bulletManager;

    [SerializeField]
    Transform _bulletPoint;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;

        _isUsingMobile = Application.platform == RuntimePlatform.Android ||
                        Application.platform == RuntimePlatform.IPhonePlayer;

        _gameManager = FindAnyObjectByType<GameManager>();
        _bulletManager = FindAnyObjectByType<BulletManager>();

        _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if(_isUsingMobile)
            GetTouchInput();
        else
            GetConventionalInput();
        

        Move();

    }

    private void FixedUpdate()
    {
        _count++;
        if (_count > 5)
        {
             GameObject bullet = _bulletManager.GetBullet(BulletTypes.PLAYERBULLET);
            bullet.transform.position = _bulletPoint.position;

            _count = 0;
        }
    }

    void GetTouchInput()
    {

        foreach (Touch touch in Input.touches)
        {
            Vector3 pos = _camera.ScreenToWorldPoint(touch.position);
            pos = new Vector3(pos.x, pos.y, 0);

            _destination = Vector2.Lerp(transform.position, pos, Time.deltaTime * _speed);

            Debug.Log("Touch Input Pos= " + pos);
            Debug.Log("Destination Pos = " + _destination);
        }
    }

    void GetConventionalInput()
    {
        float xAxis = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float yAxis = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;

        _destination = new Vector3(transform.position.x + xAxis, transform.position.y, 0);
    }

    void Move()
    {
        transform.position = _destination;


        if (transform.position.x < _boundry.min)
        {
            transform.position = new Vector3(_boundry.min, transform.position.y, transform.position.z);
        }

        if (transform.position.x > _boundry.max)
        {
            transform.position = new Vector3(_boundry.max, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("I am colliding with Enemy");

            _gameManager.ChangeScore(5);

        }
    }
}
