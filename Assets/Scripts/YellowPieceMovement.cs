using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YellowPieceMovement : MonoBehaviour, IPointerClickHandler
{
    public Transform yellowPath;
    private Transform[] yellowpathnodes;
    private int currentPosition = -1;
    private bool isOnPath = false;
    private bool isSelected = false;
    [SerializeField] private Button diceBtn;
    //[SerializeField] private DiceRoll diceroll;

    private void Start()
    {
        yellowpathnodes = new Transform[yellowPath.childCount];
        for (int i = 0; i < yellowPath.childCount; i++)
        {
            yellowpathnodes[i] = yellowPath.GetChild(i);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.transform == transform)
    //            {
    //                isSelected = true;
    //                Debug.Log(name + " is selected.");
    //            }
    //        }
    //    }
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (diceBtn.GetComponent<DiceRoll>().CanSelectPiece() && diceBtn.GetComponent<DiceRoll>().RolledNumber() > 0)
        {
            isSelected = true;
            Debug.Log(name + "is selected");
            diceBtn.GetComponent<DiceRoll>().MoveSelectedPiece();
        }
    }

    private void DeselectPiece() { isSelected = false; }

    public void movePiece(int diceValue)
    {
        if (!isSelected)
        {
            Debug.Log("Select the piece first to move.");
            return;
        }

        if (!isOnPath && diceValue == 6)
        {
            isOnPath = true;
            currentPosition = 0;
            StartCoroutine(MoveToPosition(yellowpathnodes[currentPosition].position));
            DeselectPiece();
        }
        else if (isOnPath)
        {
            StartCoroutine(MoveAlongPath(diceValue));
            DeselectPiece();
        }
        else
        {
            Debug.Log("Roll a six to start moving!");
        }
    }

    public bool IsOnPath()
    {
        return isOnPath;
    }
    public bool IsSelected()
    {
        return isSelected;
    }
    private IEnumerator MoveAlongPath(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            if (currentPosition < yellowpathnodes.Length - 1)
            {
                currentPosition++;
                yield return MoveToPosition(yellowpathnodes[currentPosition].position);
            }
            else
            {
                Debug.Log("End of path reached");
                break;
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 50f * Time.deltaTime);
            yield return null;
        }
    }
}