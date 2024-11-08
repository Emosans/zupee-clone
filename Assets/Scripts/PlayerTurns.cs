using UnityEngine;
using UnityEngine.UI;

public class PlayerTurns : MonoBehaviour
{
    // variables
    public bool player1turn = true;
    public bool player2turn = false;
    [SerializeField] private Button dicebtn;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void checkTurn()
    {
        if (player1turn)
        {
            //Debug.Log("player1 turn done");
            if (dicebtn.GetComponent<DiceRoll>().RolledNumber() == 6)
            {
                Debug.Log("Repeat");
                player1turn = true;
                player2turn = false;
            } else
            {
                player1turn = false;
                player2turn = true;
            }
        }

        else if (player2turn)
        {
            //Debug.Log("player2 turn done");
            if (dicebtn.GetComponent<DiceRoll>().RolledNumber() == 6)
            {
                Debug.Log("Repeat");
                player1turn = false;
                player2turn = true;
            }
            else
            {
                player1turn = true;
                player2turn = false;
            }
        }
    }
}
