using UnityEngine;
public static class LogTool
{
    static LogDataSO logData = Resources.Load<LogDataSO>("LogData/LogDataSO");

    public static void Log(string text)
    {
        Log(LogDataType.Default , text);
    }

    public static void Log(string text, object verifiedObject, System.Type targetType)
    {
        Log(LogDataType.Default, text, verifiedObject, targetType);
    }
    public static void Log(LogDataType logDataType, string text, object verifiedObject, System.Type targetType)
    {
        if (verifiedObject.GetType() == targetType)
            Log(logDataType, text);
    }
    public static void Log(LogDataType logDataType, string text)
    {
        if (!Application.isEditor) // log everything in build
        {
            Debug.Log($"{logDataType}: {text}");
            return;
        }

        if (logData == null) logData = Resources.Load<LogDataSO>("LogData/LogDataSO");

        for (int i = 0; i < logData.logDataList.Count; i++)
        {
            LogData data = logData.logDataList[i];
            if (logDataType == data.logDataType && data.IsActive)
            {
                Debug.Log(data.logDataType.ToString().Colored(data.logColor) + ": " + text);
                return;
            }    
        }
    }
}
