using System.Collections;
using UnityEngine;

public class SpeedRun : MonoBehaviour, IObjective
{
    // Created by Giovanni Quevedo
    // Objective stipulating a player must reach the exit within a time frame to escape
    [field: SerializeField]
    float waitTime = 5;
    public bool complete { get; set; }
    GameManager gameMan;

    // Awake is called on setup
    private void Awake() {
        // We start complete, and become uncomplete if the timer runs out
        complete = true;
        var timer = Timer(waitTime);
        StartCoroutine(timer);
        gameMan = transform.parent.GetComponent<GameManager>();
    }

    // Called when a condition a
    public void UpdateStatus(){
        complete = true;
    }

    private IEnumerator Timer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime - 0.5f);
        Debug.Log("time's up!");
        gameMan.TimesUp();
    }
}