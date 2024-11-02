using UnityEngine;
using UnityEngine.UI;

public class PlayerTurns : MonoBehaviour
{
    // variables
    public bool player1turn = true;
    public bool player2turn = false;
    
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
            player1turn = false;
            player2turn = true;
        }

        else if (player2turn)
        {
            //Debug.Log("player2 turn done");
            player2turn = false;
            player1turn = true;
        }
    }
}
