using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    static public DebugText instance;
    public Text debugText;

    private List<TextTimer> highPriority;
    private List<TextTimer> lowPriority;
    private List<int> deletedTimers; 

    // Start is called before the first frame update
    private void Awake()
    {
        if (!debugText)
        {
            Text text = GameObject.Find("Debug Text").GetComponent<Text>();
            debugText = (text) ? text : GetComponent<Text>();
        }
        highPriority = new List<TextTimer>();
        lowPriority = new List<TextTimer>();
        deletedTimers = new List<int>();
        if (!instance)
            instance = this;
    }
    void Start()
    {
        if (!instance)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = "";
        foreach (TextTimer message in highPriority)
            debugText.text += message.Text;
        foreach(TextTimer message in lowPriority)
        {
            if (message.TimePassed > message.TimeAlive)
            {
                deletedTimers.Add(lowPriority.IndexOf(message));
                continue;
            }
            debugText.text += message.Text;
            message.TimePassed += Time.deltaTime;
        }

        for (int i =0; i < deletedTimers.Count; i++)
        {
            lowPriority.RemoveAt(deletedTimers[i] - i);
        }

        highPriority.Clear();
        deletedTimers.Clear();
    }

    //
    // Summary:
    //      Message is the debug message, time is how long it'll display for, use a time <0 for continuously updated messages
    static public void AddMessage(string message, float time)
    {
        TextTimer DebugMessage = new TextTimer();
        DebugMessage.Text = message + "\n";
        DebugMessage.TimeAlive = time;

        if (time < 0)
        {
            instance.highPriority.Add(DebugMessage);
        }
        else
            instance.lowPriority.Insert(0, DebugMessage);
    }
   

    private class TextTimer
    {
        public string Text;
        public float TimeAlive;
        public float TimePassed = 0.0f;

    }
}
