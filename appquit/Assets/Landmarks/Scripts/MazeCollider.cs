using Landmarks.Scripts.Progress;
using UnityEngine;

public class MazeCollider : MonoBehaviour
{
    [Tooltip("Unique identifier for this collider. Must be consistent across sessions.")]
    [SerializeField] private string colliderId;

    [Tooltip("The wall GameObjects to move when this collider is triggered.")]
    [SerializeField] private GameObject[] wallsToMove;

    [Tooltip("World-space target positions for each wall (must match wallsToMove order).")]
    [SerializeField] private Vector3[] targetPositions;

    [Tooltip("Tag the player must have to trigger this collider.")]
    [SerializeField] private string playerTag = "Player";

    private bool _triggered = false;

    private void Start()
    {
        // On scene load, restore wall state if this collider was
        // already triggered in a previous session
        if (LM_Progress.Instance.IsColliderTriggered(colliderId))
        {
            ApplyWallPositions();
            _triggered = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        if (!other.CompareTag(playerTag)) return;

        _triggered = true;
        ApplyWallPositions();
        LM_Progress.Instance.RecordColliderHit(colliderId);
    }

    private void ApplyWallPositions()
    {
        for (var i = 0; i < wallsToMove.Length; i++)
        {
            if (wallsToMove[i] == null) continue;
            if (i < targetPositions.Length)
            {
                wallsToMove[i].transform.position = targetPositions[i];
            }
        }
    }
}