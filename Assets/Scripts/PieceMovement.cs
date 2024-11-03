using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceMovement : MonoBehaviour, IPointerClickHandler
{
    public Transform redPath;
    private Transform[] redpathnodes;
    private int currentPosition = -1;
    private bool isOnPath = false;
    private bool isSelected = false;

    private void Start()
    {
        redpathnodes = new Transform[redPath.childCount];
        for (int i = 0; i < redPath.childCount; i++)
        {
            redpathnodes[i] = redPath.GetChild(i);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    isSelected = true;
                    Debug.Log(name + " is selected.");
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = true;
        Debug.Log(name + "is selected");
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
            StartCoroutine(MoveToPosition(redpathnodes[currentPosition].position));
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
            if (currentPosition < redpathnodes.Length - 1)
            {
                currentPosition++;
                yield return MoveToPosition(redpathnodes[currentPosition].position);
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
