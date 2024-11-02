using System.Collections;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    public Transform redPath;
    private Transform[] redpathnodes;
    private int currentPosition = -1;
    private bool isOnPath = false;

    private void Start()
    {
        redpathnodes = new Transform[redPath.childCount];
        for (int i = 0; i < redPath.childCount; i++)
        {
            redpathnodes[i] = redPath.GetChild(i);
        }
        transform.position = transform.position;
    }

    public void movePiece(int diceValue)
    {
        if (!isOnPath)
        {
            if (diceValue == 6)
            {
                isOnPath = true;
                currentPosition = 0;
                StartCoroutine(MoveToPosition(redpathnodes[currentPosition].position));
            }
            else
            {
                Debug.Log("Roll a six to start moving!");
            }
        }
        else
        {
            StartCoroutine(MoveAlongPath(diceValue));
        }
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
