using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    [SerializeField]
    private int startmoney = 300;
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private TextMeshProUGUI LivesText;
    public static int lives;
    [SerializeField]
    private int StartLives = 10;


    private void Start()
    {
        Money = startmoney;
        lives = StartLives;
    }

    private void Update()
    {
        moneyText.text = "Crystals : " + Money.ToString();
        LivesText.text = "Lives : " + lives.ToString();
    }

}
