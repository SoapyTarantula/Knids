using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnidScript : MonoBehaviour
{
    Transform _targetPos;
    public int _currentHealth;
    Rigidbody2D _knidRB;
    float _knidSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _knidSpeed = Random.Range(100f, 500f);
        _currentHealth = Random.Range(1, 3);
        transform.localScale = new Vector3(_currentHealth, _currentHealth, 1);
        if(_knidRB == null){
            try
            {
                _knidRB = GetComponent<Rigidbody2D>();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        if(_targetPos == null){
            try
            {
                _targetPos = GameObject.FindGameObjectWithTag("KnidTarget").transform;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        KnidSwitching();
        if (Vector2.Distance(transform.position, _targetPos.position) < 25)
        {
            Debug.DrawLine(transform.position, _targetPos.position, Color.red);
        }
        else
        { Debug.DrawLine(transform.position, _targetPos.position, Color.blue); }
    }

    void FixedUpdate()
    {
        _knidRB.velocity = (_targetPos.position - transform.position).normalized * Time.fixedDeltaTime * _knidSpeed;
        transform.up = (_targetPos.position - transform.position).normalized;
    }

    int KnidHealth(){

        return _currentHealth;
    }

    void KnidSwitching(){
        if(Vector2.Distance(transform.position, _targetPos.position) < 0.8f && gameObject.activeSelf || KnidHealth() <= 0 && gameObject.activeSelf){
            gameObject.SetActive(false);
            GameObject.Find("KnidPooler").GetComponent<KnidSpawner>()._activeKnids.Remove(this.gameObject);
            GameObject.Find("KnidPooler").GetComponent<KnidSpawner>()._inactiveKnids.Add(this.gameObject);
            if (KnidHealth() > 0)
            {
                print("A Knid reached the end point");
            }
            else
            {
                print("A Knid has died");
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            _currentHealth -= 1;
            print(_currentHealth);
        }
        else return;
    }
}

