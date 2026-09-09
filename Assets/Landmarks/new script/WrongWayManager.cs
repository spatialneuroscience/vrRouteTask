using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Landmarks.Scripts.Progress;

public class WrongWayManager : MonoBehaviour
{
    void Start()
    {
        var events = LM_Progress.Instance.GetTriggeredColliders()
            .Where(id => id.StartsWith("WW|"))
            .ToList();

        if (events.Count == 0) return;

        // Build final state per UIObject path — last recorded event wins
        var finalStates = new Dictionary<string, bool>();
        foreach (var evt in events)
        {
            var parts = evt.Split('|');
            if (parts.Length < 3) continue;
            string path = parts[1];
            bool state = parts[2] == "1";
            finalStates[path] = state;
        }

        // Apply final states to the actual GameObjects
        foreach (var kvp in finalStates)
        {
            var go = FindGameObjectByPath(kvp.Key);
            if (go != null) go.SetActive(kvp.Value);
            else Debug.LogWarning("WrongWayResumeHandler: could not find " + kvp.Key);
        }
    }

    private GameObject FindGameObjectByPath(string path)
    {
        var parts = path.Split('/');
        var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        GameObject current = roots.FirstOrDefault(r => r.name == parts[0]);
        if (current == null) return null;
        for (int i = 1; i < parts.Length; i++)
        {
            var child = current.transform.Find(parts[i]);
            if (child == null) return null;
            current = child.gameObject;
        }
        return current;
    }
}