using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject Explosion;

    private GameObject _player;
    private Rigidbody _rigidbody;

    private float _power;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _rigidbody.velocity = transform.forward * Random.Range(75, 85);

        _power = Random.Range(45, 55);

        _player = GameObject.FindWithTag("Player");

        
        Destroy(gameObject, 30);
    }

    void Update()
    {
        if (GameManager.GameMode == 2)
        {
            Vector3 Direction = _player.transform.position - transform.position;

            int angleMultiplier = (_player.transform.position.y < transform.position.y ? 1 : -1);

            _rigidbody.velocity = Direction.normalized * _power;
            transform.rotation = Quaternion.Euler(Vector3.Angle(Direction, Vector3.forward)*angleMultiplier, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth)
        {
            playerHealth.TakeDamage();
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
