using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int GameMode;
    public Transform PlayerTransform;
    public MonoBehaviour[] ScriptsForOff;

    public GameObject CoinPrefab;
    public int CoinsCount;

    public GameObject MinePrefab;
    public int MinesCount;

    public GameObject LoseScreen;
    public Text TipText;

    public GameObject ReturnGameZoneScreen;
    public Text TimeReportText;

    public float OutsideTime = 10f;

    private float _timerOut;

    public float HalfWidthGameZone;
    public float MinHeightGameZone;
    public float MaxHeightGameZone;

    public Slider HeightDial;
    public Animator HeightDialAnimator;
    public Animator HeightDialCaption;

    public Text Score;
    public int CollectCoins
    {
        get;
        private set;
    }

    public GameObject Menu;

    public GameObject WinScreen;
    // Start is called before the first frame update
    void Start()
    {

        HeightDial.maxValue = MaxHeightGameZone;
        HeightDial.minValue = MinHeightGameZone;

        SpawnObject(CoinPrefab, CoinsCount);
        SpawnObject(MinePrefab, MinesCount);

        Score.text = $"Собрано: 0 из {CoinsCount}";
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mathf.Abs(PlayerTransform.position.z) > HalfWidthGameZone)|| (PlayerTransform.position.y > MaxHeightGameZone))
        {
            ReturnGameZoneScreen.SetActive(true);

            _timerOut += Time.deltaTime;
            TimeReportText.text = (OutsideTime - Mathf.Floor(_timerOut)).ToString();

            if (_timerOut >= OutsideTime)
            {
                GameOver("Не покидайте игровую зону!");
            }
        }
        else
        {
            ReturnGameZoneScreen.SetActive(false);

            _timerOut = 0;
        }

        HeightDial.value = PlayerTransform.position.y;

        if (PlayerTransform.position.y < MinHeightGameZone + 50)
        {
            HeightDialAnimator.SetBool("Alarm", true);
            HeightDialCaption.SetBool("Alarm", true);
        }
        else
        {
            HeightDialAnimator.SetBool("Alarm", false);
            HeightDialCaption.SetBool("Alarm", false);
        }

        if (PlayerTransform.position.y < MinHeightGameZone)
        {
            GameOver("Следите за высотой!");
        }
    }

    public void GameOver(string Tip)
    {
        ReturnGameZoneScreen.SetActive(false);

        LoseScreen.SetActive(true);
        TipText.text = Tip;

        FindObjectOfType<CameraMotion>().enabled = false;

        gameObject.SetActive(false);

        foreach(var script in ScriptsForOff)
        {
            script.enabled = false;
        }
    }

    public void CollectCoin()
    {
        Score.text = $"Собрано: {++CollectCoins} из {CoinsCount}";
        GetComponent<AudioSource>().Play();

        if (CollectCoins == CoinsCount)
            GameComplete();
    }

    public void StartGame()
    {
        foreach (var script in ScriptsForOff)
            script.enabled = true;

        Menu.SetActive(false);
    }

    public void EasyStart()
    {
        StartGame();

        GameMode = 0;
    }
    public void NormalStart()
    {
        StartGame();

        GameMode = 1;
    }
    public void HardStart()
    {
        StartGame();

        GameMode = 2;
    }
    void GameComplete()
    {
        WinScreen.SetActive(true);
        gameObject.SetActive(false);

        foreach(var script in ScriptsForOff)
        {
            script.enabled = false;
        }
    }

    void SpawnObject(GameObject targetObject, int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            bool freePoint = false;

            Vector3 SpawnPoint = new Vector3();

            while (!freePoint)
            {
                SpawnPoint = new Vector3(0, Random.Range(MinHeightGameZone + 20, MaxHeightGameZone - 20), Random.Range(-HalfWidthGameZone + 20, HalfWidthGameZone - 20));

                freePoint = Physics.CheckSphere(SpawnPoint.normalized, 2f);
            }

            Instantiate(targetObject, SpawnPoint, Quaternion.identity);
        }
    }
}
