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
        
    }

    public void RollDice()
    {
        rollnum = Random.Range(1, 7);
        canSelectPiece = true;
        if (player1.GetComponent<PlayerTurns>().player1turn)
        {
            player1text.text = "Dice Rolled : "+rollnum;

        }
        else if (player1.GetComponent<PlayerTurns>().player2turn)
        {
            player2text.text = "Dice Rolled : " + rollnum;
        }
        player1.GetComponent<PlayerTurns>().checkTurn();

        //if (AnyPieceSelected())
        //{
        //    MoveSelectedPiece();
        //}   
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


        foreach (var piece in yellowPieces)
        {
            YellowPieceMovement pieceMovement = piece.GetComponent<YellowPieceMovement>();
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

}
