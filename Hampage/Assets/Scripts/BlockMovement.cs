using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float openHeight = 4.5f;
    public float duration = 1;
    bool doorOpen;
    Vector3 closePosition;
    void Start()
    {
        // Sets the first position of the door as it's closed position.
        closePosition = transform.position;
    }
    public void OperateDoor()
    {
        StopAllCoroutines();
        if (!doorOpen)
        {
            Vector3 openPosition = closePosition + Vector3.up * openHeight;
            StartCoroutine(MoveDoor(openPosition));
        }
        else
        {
            StartCoroutine(MoveDoor(closePosition));
        }
        doorOpen = !doorOpen;
    }
    IEnumerator MoveDoor(Vector3 targetPosition)
    {
        float timeElapsed = 0;
        Vector3 startPosition = transform.position;
        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
