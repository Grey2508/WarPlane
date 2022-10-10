using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float Power;
    public float MaxPower;
    public float Acceleration;
    public float TorqueSpeed;
    public float Torque;

    public Slider PowerDial;
    public float TimeMaxPower;
    public Animator PowerDialAnimator;
    public Animator PowerDialCaption;

    private float _timerMaxPower;

    private GameManager _manager;

    public AudioSource Sound;

    private void Start()
    {
        _manager = FindObjectOfType<GameManager>();
    }
    private void FixedUpdate()
    {
        Power += Input.GetAxis("Vertical") * Time.deltaTime * Acceleration;
        Power = Mathf.Clamp(Power, 0, MaxPower);

        // Torque += Input.GetAxis("Horizontal") * Time.deltaTime * TorqueSpeed;
        Torque = Input.GetAxis("Horizontal") * TorqueSpeed * Time.deltaTime;

        Torque = Mathf.Clamp(Torque, -120, 120);
        //Vector3 inputVector = new Vector3(0, 0, 1);

        Rigidbody.AddRelativeForce(0, 0, Power);
        Rigidbody.AddRelativeTorque(Torque, 0, 0);

        float powerLevel = Power / MaxPower;

        PowerDial.value = powerLevel;

        Sound.pitch = 0.5f + powerLevel;

        //ColorBlock sliderColors = PowerDial.colors;
        //Color captionColor = PowerDialCaption.color;
        ////PowerDialCaption.
        //int fontSize = 0;

        if (Power == MaxPower)
        {
            //if (captionColor != Color.red)
            //{
            //    sliderColors.disabledColor = Color.red;
            //    captionColor = Color.red;
            //}

            PowerDialAnimator.SetBool("Alarm", true);
            PowerDialCaption.SetBool("Alarm", true);

            _timerMaxPower += Time.deltaTime;

            if (_timerMaxPower > TimeMaxPower)
                _manager.GameOver("Нельзя долго лететь на максимальной тяге!");
        }
        else
        {
            //if (captionColor != Color.green)
            //{
            //    sliderColors.disabledColor = Color.green;
            //    captionColor = Color.green;
            //}

            PowerDialAnimator.SetBool("Alarm", false);
            PowerDialCaption.SetBool("Alarm", false);

            _timerMaxPower = 0;
            //fontSize = 19;
        }

        //PowerDial.colors = sliderColors;
        //PowerDialCaption.color = captionColor;
        //PowerDialCaption.fontSize
    }

    private void OnEnable()
    {
        Rigidbody.isKinematic = false;
        Sound.enabled = true;
    }

    private void OnDisable()
    {
        //Rigidbody.isKinematic = true;
        Sound.enabled = false;
    }
}
