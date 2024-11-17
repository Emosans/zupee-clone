using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class DiceRoll : MonoBehaviour
{
    // variables
    [SerializeField] private GameObject player1;
    [SerializeField] private TextMeshProUGUI player1text;
    [SerializeField] private TextMeshProUGUI player2text;
    private int rollnum;
    private bool canSelectPiece = false;
    [SerializeField] private Button dicebutton;
    private int playerscore;
    private int aiscore;
    public List<GameObject> eligibleToMove;

    // pieces
    [SerializeField] public List<GameObject> redPieces;
    [SerializeField] public List<GameObject> yellowPieces;
    

    void Start()
    {
        //diceText = GetComponentInChildren<Text>();
        Debug.Log("Connected");
        dicebutton.interactable = true; 
    }

    void Update()
    {
    }

    public void RollDice()
    {
        rollnum = Random.Range(6, 7);
        canSelectPiece = true;
        if (player1.GetComponent<PlayerTurns>().player1turn)
        {
            //Debug.Log("player 1 turn");
            player1text.text = "Dice Rolled : "+rollnum;

        }
        //else if (player1.GetComponent<PlayerTurns>().player2turn)
        //{
            
        //    player2text.text = "Dice Rolled : " + rollnum;
        //}
        player1.GetComponent<PlayerTurns>().checkTurn();

        if (player1.GetComponent<PlayerTurns>().player2turn)
        {
            //Debug.Log("player 2 turn");
            disableButton();
            StartCoroutine(aimoves());
        }

        //if (AnyPieceSelected())
        //{
        //    MoveSelectedPiece();
        //}   
    }

    private GameObject randomPiece(List<GameObject> randomPieces)
    {
        return randomPieces[Random.Range(0,randomPieces.Count)];
    }

    private IEnumerator aimoves()
    {
        if (player1.GetComponent<PlayerTurns>().player2turn)
        {
            yield return new WaitForSeconds(2f);

            rollnum = Random.Range(1, 7);
            player2text.text = "Dice Rolled : " + rollnum;

            eligibleToMove = new List<GameObject>();

            // if its on path add it to list
            for(int index=0;index<yellowPieces.Count;index++)
            {
                if (yellowPieces[index].GetComponent<YellowPieceMovement>().IsOnPath())
                {
                    eligibleToMove.Add(yellowPieces[index]);
                }
            }

            // when 6 is rolled add the random piece to the list
            if (rollnum == 6)
            {
                GameObject randomNewPiece = randomPiece(yellowPieces);
                if (!randomNewPiece.GetComponent<YellowPieceMovement>().IsOnPath())
                {
                    eligibleToMove.Add(randomNewPiece);
                }

            }

            // check if list count>0 then choose any random piece to move at each roll
            if (eligibleToMove.Count > 0)
            {
                GameObject randomPieceToMove = eligibleToMove[Random.Range(0, eligibleToMove.Count)];
                randomPieceToMove.GetComponent<YellowPieceMovement>().movePiece(rollnum);

                aiscore += rollnum;
                Debug.Log("Player 2 score : "+aiscore);
                yield return new WaitForSeconds(1f);

                // move again
                if(rollnum == 6)
                {
                    yield return StartCoroutine(aimoves());
                }
            }
            player1.GetComponent<PlayerTurns>().checkTurn();
            enableButton();
        }
    }

    private void disableButton()
    {
        dicebutton.interactable = false;
    }

    private void enableButton()
    {
        dicebutton.interactable = true;
    }

    public void MoveSelectedPiece()
    {
        foreach (var piece in redPieces)
        {
            RedPieceMovement pieceMovement = piece.GetComponent<RedPieceMovement>();
            if (pieceMovement.IsSelected())
            {
                pieceMovement.movePiece(rollnum);
                canSelectPiece = false;
                break;
            }
        }


        
    }

    //private bool AnyPieceSelected()
    //{
    //    foreach (var piece in redPieces)
    //    {
    //        if (piece.GetComponent<PieceMovement>().IsSelected())
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    public int RolledNumber()
    {
        return rollnum;
    }

    public bool CanSelectPiece()
    {
        return canSelectPiece;
    }

    public void updatePlayerScore()
    {
        playerscore += rollnum;
        Debug.Log("Player 1 score : "+playerscore);
    }

}
