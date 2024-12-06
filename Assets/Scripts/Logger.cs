using System;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public static Logger Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private TMPro.TMP_Text message;
    private int totalErrorCount;
    private string fullError;
    private bool limitErrorCharectorLength;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    public void Log(string message)
    {
        if (this.message != null)
        {
            this.message.text = message;
            Debug.Log(message);
        }
    }


    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            totalErrorCount++;
            fullError +=
                $"< error {totalErrorCount} > => ( [time] : {DateTime.Now.ToString()} [logString] : {logString} [stackTrace] : {stackTrace} ) < /error {totalErrorCount}>                        ";
            //GameManager.Instance.gameSaveManager.SaveLogText(fullError);
            if (logString.Length > 200 && limitErrorCharectorLength)
                logString = logString.Substring(logString.Length - 200);
            message.text += $"{logString}: ";
            if (stackTrace.Length > 200 && limitErrorCharectorLength)
                stackTrace = stackTrace.Substring(stackTrace.Length - 200);
            message.text += $"{stackTrace}\n-\n";
            if (message.text.Length > 2000)
                message.text = message.text.Substring(message.text.Length - 2000);
        }
    }
}

