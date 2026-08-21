using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    // ── Pause Detection Thresholds ────────────────────────────────────────────
    public float pauseDurationThreshold  = 2f;
    public float positionChangeThreshold = 1f; // Bounding radius
    // ─────────────────────────────────────────────────────────────────────────

    private struct PositionDataPoint {
        public Vector3 position;
        public float timestamp;
    }
    private List<PositionDataPoint> positionHistory = new List<PositionDataPoint>();
    
    // New list to keep track of each individual pause's duration
    private List<float> individualPauseDurations = new List<float>();

    private int     pauseCount     = 0;
    private float   totalPauseTime = 0f;
    private bool    summaryWritten = false;
    private string logFilePath = "";

    void Start () {
        Debug.Log("Project Root Folder: " + System.IO.Directory.GetCurrentDirectory());
        cameraCon = player.transform as Transform;
        cameraRig = camerarig.transform as Transform;

        experiment = GameObject.FindWithTag("Experiment");
        manager    = experiment.GetComponent("Experiment") as Experiment;
        log        = manager.dblog;
        avatar     = transform;

        if (log != null) {
            logFilePath = log.FilePath;
            Debug.Log("avatarLog: Log file path cached at: " + logFilePath);
        }
        
        // Pre-allocate memory for ~15 minutes of data at 50fps to eliminate runtime resizing
        positionHistory = new List<PositionDataPoint>(45000);
    }

    void FixedUpdate () {
        log.log("Avatar: \t" + avatar.name + "\t" +
                "Position (xyz): \t" + cameraCon.position.x    + "\t" + cameraCon.position.y    + "\t" + cameraCon.position.z    + "\t" +
                "Rotation (xyz): \t" + cameraCon.eulerAngles.x  + "\t" + cameraCon.eulerAngles.y  + "\t" + cameraCon.eulerAngles.z  + "\t" +
                "Camera   (xyz): \t" + cameraRig.eulerAngles.x  + "\t" + cameraRig.eulerAngles.y  + "\t" + cameraRig.eulerAngles.z  + "\t"
                , 1);

        PositionDataPoint currentPoint;
        currentPoint.position = cameraCon.position;
        currentPoint.timestamp = Time.fixedTime;
        positionHistory.Add(currentPoint);
    }

    public void WritePauseSummary() {
        if (summaryWritten) return;
        summaryWritten = true;

        CalculatePausesRetroactively();

        float avgPause = pauseCount > 0 ? totalPauseTime / pauseCount : 0f;

        log.log("", 1);
        log.log("============================================================", 1);
        log.log("RETROACTIVE PAUSE ANALYSIS RESULTS", 1);
        log.log("============================================================", 1);
        log.log("Pause Count          : " + pauseCount, 1);
        log.log("Total Pause Time     : " + totalPauseTime.ToString("F2") + " s", 1);
        log.log("Avg Pause Duration   : " + avgPause.ToString("F2") + " s", 1);
        log.log("Thresholds Used      : duration >= " + pauseDurationThreshold + "s" +
                "  |  bounding diameter <= " + positionChangeThreshold + "m", 1);
        log.log("------------------------------------------------------------", 1);
        log.log("INDIVIDUAL PAUSE BREAKDOWN:", 1);
        
        // Loop through and print each pause dynamically
        if (individualPauseDurations.Count == 0) {
            log.log("No valid pauses detected.", 1);
        } else {
            for (int p = 0; p < individualPauseDurations.Count; p++) {
                log.log($"Pause {p + 1}: {individualPauseDurations[p].ToString("F2")} s", 1);
            }
        }
        
        log.log("============================================================", 1);

        Debug.Log("avatarLog: Retroactive pause summary written with individual breakdowns.");
    }

    private void CalculatePausesRetroactively() {
        if (positionHistory.Count < 2) return;

        int i = 0;
        while (i < positionHistory.Count) {
            float pauseStart = positionHistory[i].timestamp;
            Vector3 anchorPosition = positionHistory[i].position;
            
            int j = i + 1;
            bool isStillPausing = true;
            float validPauseEnd = pauseStart;

            while (j < positionHistory.Count && isStillPausing) {
                if (Vector3.Distance(positionHistory[j].position, anchorPosition) <= positionChangeThreshold) {
                    validPauseEnd = positionHistory[j].timestamp;
                    j++;
                } else {
                    isStillPausing = false;
                }
            }

            float finalDuration = validPauseEnd - pauseStart;

            if (finalDuration >= pauseDurationThreshold) {
                pauseCount++;
                totalPauseTime += finalDuration;
                
                // Track this specific duration to break down in the final summary
                individualPauseDurations.Add(finalDuration);
                
                i = j; 
            } else {
                i++;
            }
        }
    }

    void OnApplicationQuit() { WritePauseSummary(); }
    void OnDestroy()          { WritePauseSummary(); }
}