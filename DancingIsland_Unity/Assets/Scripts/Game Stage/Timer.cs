using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerTime;
    private float remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = timerTime;

        timerText.GetComponent<Timer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            timerText.color = (remainingTime > 60) ? Color.green : (remainingTime > 10 ?  Color.yellow : Color.red);

            int minutes = Mathf.FloorToInt (remainingTime / 60);
            int seconds = Mathf.FloorToInt (remainingTime % 60);

            timerText.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
        } 
    }
}
