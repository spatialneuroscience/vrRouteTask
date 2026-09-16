using UnityEngine;
using System.Collections;

public class avatarLog : MonoBehaviour {

    [HideInInspector] public bool navLog = true;
    private Transform avatar;
    private Transform cameraCon;
    private Transform cameraRig;

    private GameObject experiment;
    private dbLog log;
    private Experiment manager;

    public GameObject player;
    public GameObject camerarig;

    // ── Pause Detection Thresholds (tweak in Inspector or here) ──────────────
    public float pauseDurationThreshold  = 0.5f;   // seconds stationary to count as a pause
    public float positionChangeThreshold = 0.01f;  // units of movement considered "not moving"
    public float rotationChangeThreshold = 0.1f;   // degrees of rotation considered "not moving"
    // ─────────────────────────────────────────────────────────────────────────

    // Pause tracking state (private, no need to touch these)
    private Vector3 lastPosition;
    private Vector3 lastRotation;
    private float   pauseStartTime  = -1f;
    private bool    inPause         = false;
    private int     pauseCount      = 0;
    private float   totalPauseTime  = 0f;

    void Start () {
        Debug.Log("Project Root Folder: " + System.IO.Directory.GetCurrentDirectory());
        cameraCon = player.transform as Transform;
        cameraRig = camerarig.transform as Transform;

        experiment = GameObject.FindWithTag("Experiment");
        manager    = experiment.GetComponent("Experiment") as Experiment;
        log        = manager.dblog;
        avatar     = transform;

        // Seed last-known position/rotation so first frame has a valid comparison
        lastPosition = cameraCon.position;
        lastRotation = cameraCon.eulerAngles;

        LoadPauseTotals();
    }

    void FixedUpdate () {

        // ── Position & Rotation Logging (unchanged from original) ────────────
        log.log("Avatar: \t" + avatar.name + "\t" +
                "Position (xyz): \t" + cameraCon.position.x    + "\t" + cameraCon.position.y    + "\t" + cameraCon.position.z    + "\t" +
                "Rotation (xyz): \t" + cameraCon.eulerAngles.x  + "\t" + cameraCon.eulerAngles.y  + "\t" + cameraCon.eulerAngles.z  + "\t" +
                "Camera   (xyz): \t" + cameraRig.eulerAngles.x  + "\t" + cameraRig.eulerAngles.y  + "\t" + cameraRig.eulerAngles.z  + "\t"
                , 1);
        log.log("Avatar: \t" + avatar.name + "\t" +
                "Position (xyz): \t" + cameraCon.position.x    + "\t" + cameraCon.position.y    + "\t" + cameraCon.position.z    + "\t" +
                "Rotation (xyz): \t" + cameraCon.eulerAngles.x  + "\t" + cameraCon.eulerAngles.y  + "\t" + cameraCon.eulerAngles.z  + "\t" +
                "Camera   (xyz): \t" + cameraRig.eulerAngles.x  + "\t" + cameraRig.eulerAngles.y  + "\t" + cameraRig.eulerAngles.z  + "\t"
                , 1);

        // ── Pause Detection ──────────────────────────────────────────────────

        // Position delta — straight Euclidean distance from last frame
        float posDelta = Vector3.Distance(cameraCon.position, lastPosition);

        // Rotation delta — Mathf.DeltaAngle handles the 0/360 wraparound cleanly
        float rotDelta = Mathf.Abs(Mathf.DeltaAngle(lastRotation.x, cameraCon.eulerAngles.x))
                       + Mathf.Abs(Mathf.DeltaAngle(lastRotation.y, cameraCon.eulerAngles.y))
                       + Mathf.Abs(Mathf.DeltaAngle(lastRotation.z, cameraCon.eulerAngles.z));

        bool isMoving = posDelta > positionChangeThreshold || rotDelta > rotationChangeThreshold;

        if (!isMoving) {
            // Not moving — start timing a pause if we haven't already
            if (!inPause) {
                pauseStartTime = Time.fixedTime;
                inPause = true;
            }
        } else {
            // Moving — close out any pause that was in progress
            if (inPause) {
                float duration = Time.fixedTime - pauseStartTime;
                if (duration >= pauseDurationThreshold) {
                    pauseCount++;
                    totalPauseTime += duration;
                }
                inPause = false;
            }
        }

        // Always update last-known values for next frame comparison
        lastPosition = cameraCon.position;
        lastRotation = cameraCon.eulerAngles;
    }

    void OnApplicationQuit() {

        // Close out any pause still in progress at quit time
        if (inPause) {
            float duration = Time.fixedTime - pauseStartTime;
            if (duration >= pauseDurationThreshold) {
                pauseCount++;
                totalPauseTime += duration;
            }
        }

        // ── Append Pause Summary to Log ──────────────────────────────────────
        if (log != null) {
            float avgPause = pauseCount > 0 ? totalPauseTime / pauseCount : 0f;

            log.log("", 1);
            log.log("============================================================", 1);
            log.log("PAUSE ANALYSIS RESULTS", 1);
            log.log("============================================================", 1);
            log.log("Pause Count          : " + pauseCount, 1);
            log.log("Total Pause Time     : " + totalPauseTime.ToString("F2") + " s", 1);
            log.log("Avg Pause Duration   : " + avgPause.ToString("F2") + " s", 1);
            log.log("Thresholds Used      : duration >= " + pauseDurationThreshold + "s" +
                    "  |  pos <= " + positionChangeThreshold +
                    "  |  rot <= " + rotationChangeThreshold + " deg", 1);
            log.log("============================================================", 1);

            log.close();
            Debug.Log("Log file closed safely. Pauses detected: " + pauseCount);
        }
    }
    public void WritePauseSummary()
    {
        if (inPause)
        {
            float duration = Time.fixedTime - pauseStartTime;
            if (duration >= pauseDurationThreshold)
            {
                pauseCount++;
                totalPauseTime += duration;
            }
            inPause = false;
        }

        if (log == null) return;

        float avgPause = pauseCount > 0 ? totalPauseTime / pauseCount : 0f;
        log.log("", 1);
        log.log("============================================================", 1);
        log.log("PAUSE ANALYSIS RESULTS", 1);
        log.log("============================================================", 1);
        log.log("Pause Count          : " + pauseCount, 1);
        log.log("Total Pause Time     : " + totalPauseTime.ToString("F2") + " s", 1);
        log.log("Avg Pause Duration   : " + avgPause.ToString("F2") + " s", 1);
        log.log("Thresholds Used      : duration >= " + pauseDurationThreshold + "s" +
                "  |  pos <= " + positionChangeThreshold +
                "  |  rot <= " + rotationChangeThreshold + " deg", 1);
        log.log("============================================================", 1);

        SavePauseTotals();
    }

    private string GetPauseTotalsPath()
    {
        var exp = GameObject.FindWithTag("Experiment").GetComponent<Experiment>();
        return GlobalPaths.DataPath + "pause_totals.dat";
    }

    private void LoadPauseTotals()
    {
        var path = GetPauseTotalsPath();
        if (!System.IO.File.Exists(path)) return;

        var lines = System.IO.File.ReadAllLines(path);
        foreach (var line in lines)
        {
            var parts = line.Split('=');
            if (parts.Length != 2) continue;
            if (parts[0] == "pauseCount") int.TryParse(parts[1], out pauseCount);
            if (parts[0] == "totalPauseTime") float.TryParse(parts[1], out totalPauseTime);
        }
        Debug.Log($"Loaded previous pause totals: count={pauseCount}, total={totalPauseTime}");
    }

    private void SavePauseTotals()
    {
        var path = GetPauseTotalsPath();
        System.IO.File.WriteAllLines(path, new[]
        {
            $"pauseCount={pauseCount}",
            $"totalPauseTime={totalPauseTime}"
        });
    }
}