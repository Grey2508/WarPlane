using UnityEngine;

public class Coin : MonoBehaviour
{
    public float yRotation;
    public float RotationSpeed;

    private GameManager _manager;

    // Start is called before the first frame update
    void Start()
    {
        _manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, yRotation * RotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _manager.CollectCoin();
            Destroy(gameObject);
        }
    }
}
