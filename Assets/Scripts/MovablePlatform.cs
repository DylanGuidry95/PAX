using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovablePlatform : MonoBehaviour
{
	void Start ()
    {
        if(_pathFlow.Count == 0)
        {
            AddPathNode();
        }
        gameObject.transform.position = _pathFlow[0];
        StartCoroutine(StartItAll());
        //Messenger.AddListener<GameObject>("Chilled", OnCoolToggle);
    }
    
    IEnumerator StartItAll()
    {
        yield return new WaitForSeconds(initDelay);
        StartCoroutine(FollowPath());
    }

    public void AddPathNode()
    {
        _pathFlow.Add(gameObject.transform.position);
    }

    IEnumerator FollowPath()
    {
        if (_pathFlow.Count == _count)
        {
            _count = 0;
        }

        if(!lerpMotion)
        while(Vector3.Distance(transform.position, _pathFlow[_count]) > 0.003f)
        {
            transform.position += ((_pathFlow[_count] - transform.position)) * _speed * Time.deltaTime; 
            yield return null;
        }

        if(lerpMotion)
        {
            while (Vector3.Distance(transform.position, _pathFlow[_count]) > 0.003f)
            {
                transform.position = Vector3.Lerp(transform.position, _pathFlow[_count], _speed * Time.deltaTime);
            }
        }

        yield return new WaitForSeconds(_platformDelay);

        StopCoroutine(FollowPath());
        _count++;
        StartCoroutine(FollowPath());
    }

    [SerializeField]
    private List<Vector3> _pathFlow;

    private int _count = 0;

    [SerializeField]
    private float _platformDelay;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private bool lerpMotion = false;

    [SerializeField]
    private float initDelay = 0;

    //private bool _chilled = false; //*
}

///
/// Zac King
///