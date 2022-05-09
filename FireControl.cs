using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    private Controls _controls;

    public float shotSpeed = 1000f;
    float _timeToFire = 0;
    [SerializeField] Transform firePos;
    [SerializeField] GameObject bulletPrefab;
    public List<GameObject> _bulletArchive = new List<GameObject>();
    public List<GameObject> _activeBullets = new List<GameObject>();

    void Awake()
    {
        _controls = new Controls();
    }

    void Start()
    {
        Transform _poolBox = GameObject.Find("Pooler").transform;
        if (_bulletArchive.Count <= 0)
        {
            for (int i = 0; i < 100; i++)
            {
                GameObject _temp = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, _poolBox) as GameObject;
                _bulletArchive.Add(_temp);
                _temp.SetActive(false);
            }
        }
    }

    void OnEnable()
    {
        _controls.Enable();
    }

    void OnDisable()
    {
        _controls.Disable();
    }
    void Update()
    {
        Shooting();
    }

    
    bool Shooting(){
        bool _isShooting = _controls.PlayerControl.Fire.ReadValue<float>() > 0;
        float _rateOfFire = 0.5f;


        if(_isShooting && _timeToFire <= Time.time){


            GameObject _bullet = _bulletArchive[0];
            _bulletArchive.Remove(_bullet);
            _activeBullets.Add(_bullet);
            _bullet.transform.position = firePos.transform.position;
            _bullet.SetActive(true);
            _bullet.GetComponent<Rigidbody2D>().velocity = shotSpeed * Time.fixedDeltaTime * firePos.transform.up.normalized;

            StartCoroutine(Pooler(_bullet));

            _timeToFire = Time.time + _rateOfFire;
            //print("The next shot can happen at " + Mathf.Floor(_timeToFire) + ", the time is " + Mathf.Floor(Time.time));
            _isShooting = false;
        }

        return _isShooting;
    }

    IEnumerator Pooler(GameObject _obj){
        yield return new WaitForSeconds(5f);
        if (_obj.activeSelf)
        {
            _obj.SetActive(false);
            _activeBullets.Remove(_obj);
            _bulletArchive.Add(_obj);
        }
    }
}