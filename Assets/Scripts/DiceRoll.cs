using System.Collections;
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

    // pieces
    [SerializeField] private GameObject[] redPieces;
    [SerializeField] private GameObject[] yellowPieces;

    void Start()
    {
        //diceText = GetComponentInChildren<Text>();
        Debug.Log("Connected");
    }

    void Update()
    {
        dicebutton.interactable = true;
    }

    public void RollDice()
    {
        rollnum = Random.Range(1, 7);
        canSelectPiece = true;
        if (player1.GetComponent<PlayerTurns>().player1turn)
        {
            Debug.Log("player 1 turn");
            player1text.text = "Dice Rolled : "+rollnum;

        }
        //else if (player1.GetComponent<PlayerTurns>().player2turn)
        //{
            
        //    player2text.text = "Dice Rolled : " + rollnum;
        //}
        player1.GetComponent<PlayerTurns>().checkTurn();

        if (player1.GetComponent<PlayerTurns>().player2turn)
        {
            Debug.Log("player 2 turn");
            disableButton();
            StartCoroutine(aimoves());
        }

        //if (AnyPieceSelected())
        //{
        //    MoveSelectedPiece();
        //}   
    }

    private GameObject randomPiece(GameObject[] randomPieces)
    {
        return randomPieces[Random.Range(0,randomPieces.Length)];
    }

    private IEnumerator aimoves()
    {
        yield return new WaitForSeconds(2f);

        rollnum = Random.Range(1, 7);
        player2text.text = "Dice Rolled : " + rollnum;
        bool movedpiece = false;

        foreach (var piece in yellowPieces) {
            YellowPieceMovement pieceMovement = piece.GetComponent<YellowPieceMovement>();
            if (pieceMovement.IsOnPath() && rollnum>0)
            {
                pieceMovement.movePiece(rollnum);
                //canSelectPiece = false;
                movedpiece = true;
                break;
            }
            if(!movedpiece && rollnum == 6)
            {
                GameObject currentObj = randomPiece(yellowPieces);
                Debug.Log(currentObj.name);
                currentObj.GetComponent<YellowPieceMovement>().movePiece(rollnum);
                break;
            }
            yield return new WaitForSeconds(1f);

            player1.GetComponent<PlayerTurns>().checkTurn(); // check for turn again

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


        //foreach (var piece in yellowPieces)
        //{
        //    YellowPieceMovement pieceMovement = piece.GetComponent<YellowPieceMovement>();
        //    if (pieceMovement.IsSelected())
        //    {
        //        pieceMovement.movePiece(rollnum);
        //        canSelectPiece = false;
        //        break;
        //    }
        //}
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

}
