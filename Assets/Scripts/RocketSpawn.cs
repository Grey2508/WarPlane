using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawn : MonoBehaviour
{
    public GameObject RocketPrefab;
    public float TimerStart;
    
    private float _timer;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _timer = TimerStart;
    }

    void Update()
    {
        if (GameManager.GameMode == 0)
        {
            return;
        }

        _timer += Time.deltaTime;

        if (_timer > TimerStart)
        {
            Vector3 Direction = _player.transform.position - transform.position;
            
            Instantiate(RocketPrefab, transform.position, Quaternion.Euler(Vector3.Angle(Direction, Vector3.forward) * -1, 0, 0));

            _timer = 0;
        }
    }
}
