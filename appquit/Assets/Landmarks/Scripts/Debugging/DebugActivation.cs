using UnityEngine;

public class DebugActivation : MonoBehaviour
{
    private bool _lastState;

    void Update()
    {
        var wall = transform.Find("Maze Wall (218)");
        if (wall == null) return;
        if (wall.gameObject.activeSelf != _lastState)
        {
            _lastState = wall.gameObject.activeSelf;
            Debug.Log("218 changed to " + _lastState + " on frame " + Time.frameCount + "\n" + UnityEngine.StackTraceUtility.ExtractStackTrace());
        }
    }
}