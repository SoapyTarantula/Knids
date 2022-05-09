using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnidSpawner : MonoBehaviour
{
    [SerializeField] GameObject _knid;
    [SerializeField] Transform _knidTarget;
    public List<GameObject> _activeKnids = new List<GameObject>();
    public List<GameObject> _inactiveKnids = new List<GameObject>();
    [SerializeField] Transform _knidPooler;
    // Start is called before the first frame update
    void Start()
    {
        if(_knidTarget == null){try
        {
                _knidTarget = GameObject.FindGameObjectWithTag("KnidTarget").transform;
            }
        catch (System.Exception)
        {
            
            throw;
        }}
        if(_knidPooler == null){try
        {
                _knidPooler = GameObject.Find("KnidPooler").transform;
            }
        catch (System.Exception)
        {
            
            throw;
        }}
        if(_inactiveKnids.Count == 0){
            for (int i = 0; i < 10; i++)
            {
                GameObject _newKnid = Instantiate(_knid, Vector3.zero, Quaternion.identity, _knidPooler) as GameObject;
                _newKnid.SetActive(false);
                _inactiveKnids.Add(_newKnid);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_activeKnids.Count == 0){
            GameObject _newKnid = _inactiveKnids[0];
            _newKnid.GetComponent<KnidScript>()._currentHealth = Random.Range(1, 3);
            _inactiveKnids.Remove(_newKnid);
            _activeKnids.Add(_newKnid);
            Vector2 _spawnPos = GenerateNewSpawnPosition();

            if(Vector2.Distance(_spawnPos, _knidTarget.transform.position) < 50f){
                GenerateNewSpawnPosition();
            }
            _newKnid.transform.position = _spawnPos;
            _newKnid.SetActive(true);
        }
    }
    
    Vector2 GenerateNewSpawnPosition(){
        Vector2 _spawnPosition = new Vector2(Random.Range(-30, 30), Random.Range(-30, 30));

        return _spawnPosition;
    }
}