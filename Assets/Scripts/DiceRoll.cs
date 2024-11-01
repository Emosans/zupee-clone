using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DiceRoll : MonoBehaviour
{
    // variables
    [SerializeField]
    private TextMeshProUGUI diceText;


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
        diceText.text = rollnum.ToString();
        Debug.Log(diceText.text);
    }
}
