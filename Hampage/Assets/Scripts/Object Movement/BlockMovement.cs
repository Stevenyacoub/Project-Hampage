using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour, IDataPersistence 
{
    public float openHeight = 4.5f;
    public float duration = 1;
    bool doorOpen = false;
    Vector3 closePosition;
    Vector3 setclosePosition;
    Vector3 setopenPosition;
    public Vector3 doorPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the first position of the door as it's closed position.
        closePosition = transform.position;
        setclosePosition = transform.position; 
        Vector3 openPosition = closePosition + Vector3.up * openHeight;
        doorPosition = closePosition;
      
        if (doorOpen != null)
        {
            //Debug.Log("Door Open Position is" + doorOpen);
            if (doorOpen == true)
            {
                doorPosition = openPosition;
            }

           else if (doorOpen == false)
            {
                doorPosition = closePosition;
            }
            transform.position = doorPosition;
        }
    }

    public void OperateDoor()
    {
        
        StopAllCoroutines();
        if (!doorOpen)
        {
            Vector3 openPosition = closePosition + Vector3.up * openHeight;
            StartCoroutine(MoveDoor(openPosition));
            doorPosition = openPosition;
        }
        else
        {
            StartCoroutine(MoveDoor(closePosition));
            doorPosition = closePosition;
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



    public void LoadData(GameData data)
    {
        this.doorOpen = data.doorOpen;
        Debug.Log("Door Load data called door state is: " + doorOpen);
    }

    public void SaveData(ref GameData data)
    {
        data.doorOpen = this.doorOpen;
    }
}
