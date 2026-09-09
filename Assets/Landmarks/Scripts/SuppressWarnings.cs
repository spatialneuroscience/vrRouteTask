using System;
using UnityEngine;

public class SuppressWarnings : MonoBehaviour
{
    private ILogHandler _originalHandler;

    void Awake()
    {
        _originalHandler = Debug.unityLogger.logHandler;
        Debug.unityLogger.logHandler = new FilteredLogHandler(_originalHandler);
    }

    void OnDestroy()
    {
        Debug.unityLogger.logHandler = _originalHandler;
    }

    private class FilteredLogHandler : ILogHandler
    {
        private readonly ILogHandler _original;

        public FilteredLogHandler(ILogHandler original)
        {
            _original = original;
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            if (logType == LogType.Warning && format.Contains("audio listener"))
                return;
            _original.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            _original.LogException(exception, context);
        }
    }
}