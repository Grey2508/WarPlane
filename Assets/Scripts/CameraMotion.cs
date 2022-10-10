using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    public Transform PlayerTransform;
    public float SpeedMotion;

    void Update()
    {
        //Debug.Log($"{transform.localPosition}, {PlayerTransform.localPosition}");
        transform.position = PlayerTransform.position;// Vector3.Lerp(transform.position, PlayerTransform.position, Time.deltaTime * SpeedMotion);
    }
}
