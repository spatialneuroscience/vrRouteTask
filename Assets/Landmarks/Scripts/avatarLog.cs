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

    // ── Pause Detection Thresholds ────────────────────────────────────────────
    public float pauseDurationThreshold  = 2f;
    public float positionChangeThreshold = 1f;
    public float rotationChangeThreshold = 360f;
    // ─────────────────────────────────────────────────────────────────────────

    private Vector3 lastPosition;
    private Vector3 lastRotation;
    private float   pauseStartTime = -1f;
    private bool    inPause        = false;
    private int     pauseCount     = 0;
    private float   totalPauseTime = 0f;
    private bool    summaryWritten = false;

    // Cached file path — grabbed at Start() so we never depend on log being alive at shutdown
    private string logFilePath = "";

    void Start () {
        Debug.Log("Project Root Folder: " + System.IO.Directory.GetCurrentDirectory());
        cameraCon = player.transform as Transform;
        cameraRig = camerarig.transform as Transform;

        experiment = GameObject.FindWithTag("Experiment");
        manager    = experiment.GetComponent("Experiment") as Experiment;
        log        = manager.dblog;
        avatar     = transform;

        // Cache the path right now while log is guaranteed alive
        if (log != null) {
            logFilePath = log.FilePath;
            Debug.Log("avatarLog: Log file path cached at: " + logFilePath);
        }

        lastPosition = cameraCon.position;
        lastRotation = cameraCon.eulerAngles;
    }

    void FixedUpdate () {

        // ── Position & Rotation Logging ──────────────────────────────────────
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
        float posDelta = Vector3.Distance(cameraCon.position, lastPosition);

        float rotDelta = Mathf.Abs(Mathf.DeltaAngle(lastRotation.x, cameraCon.eulerAngles.x))
                       + Mathf.Abs(Mathf.DeltaAngle(lastRotation.y, cameraCon.eulerAngles.y))
                       + Mathf.Abs(Mathf.DeltaAngle(lastRotation.z, cameraCon.eulerAngles.z));

        bool isMoving = posDelta > positionChangeThreshold || rotDelta > rotationChangeThreshold;

        if (!isMoving) {
            if (!inPause) {
                pauseStartTime = Time.fixedTime;
                inPause = true;
            }
        } else {
            if (inPause) {
                float duration = Time.fixedTime - pauseStartTime;
                if (duration >= pauseDurationThreshold) {
                    pauseCount++;
                    totalPauseTime += duration;
                }
                inPause = false;
            }
        }

        lastPosition = cameraCon.position;
        lastRotation = cameraCon.eulerAngles;
    }

    public void WritePauseSummary() {
        if (summaryWritten) return;
        summaryWritten = true;

        if (inPause) {
            float duration = Time.fixedTime - pauseStartTime;
            if (duration >= pauseDurationThreshold) {
                pauseCount++;
                totalPauseTime += duration;
            }
            inPause = false;
        }

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

        // NOTE: do NOT call log.close() here — Experiment.EndScene() does that
        Debug.Log("avatarLog: Pause summary written. Pauses detected: " + pauseCount);
    }

    void OnApplicationQuit() { WritePauseSummary(); }
    void OnDestroy()          { WritePauseSummary(); }
}