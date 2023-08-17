using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text text;
    private float seconds, minutes;
    private void Start()
    {
        text = GetComponent<Text>() as Text;
    }
    private void Update()
    {
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        text.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        
    }
}
