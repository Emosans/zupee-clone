using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DiceRoll : MonoBehaviour
{
    // variables
    [SerializeField] private GameObject player1;
    [SerializeField] private TextMeshProUGUI player1text;
    [SerializeField] private TextMeshProUGUI player2text;


    void Start()
    {
        //diceText = GetComponentInChildren<Text>();
        Debug.Log("Connected");
    }

    void Update()
    {
        
    }

    public void RollDice()
    {
        int rollnum = Random.Range(1, 7);
        if (player1.GetComponent<PlayerTurns>().player1turn)
        {
            player1text.text = "Dice Rolled : "+rollnum;
        }
        else if (player1.GetComponent<PlayerTurns>().player2turn)
        {
            player2text.text = "Dice Rolled : " + rollnum;
        }
        player1.GetComponent<PlayerTurns>().checkTurn();
    }
}
