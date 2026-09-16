using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Landmarks.Scripts.Progress;
using UnityEngine;
using TMPro;

public enum HideTargetOnStart
{
    Off,
    SetInactive,
    SetInvisible,
    Mask,
    SetProbeTrial
}

public class NavigationTask : ExperimentTask
{
    [Header("Task-specific Properties")]
    public ObjectList destinations;
    private GameObject current;

    public TextAsset NavigationInstruction;

    [Tooltip("in meters")]
    public float distanceAllotted = Mathf.Infinity;
    [Tooltip("in seconds")]
    public float timeAllotted = Mathf.Infinity;

    [HideInInspector] private int score = 0;
    [HideInInspector] public int scoreIncrement = 50;
    [HideInInspector] public int penaltyRate = 2000;
    [HideInInspector] private float penaltyTimer = 0;
    [HideInInspector] public bool showScoring;

    public HideTargetOnStart hideTargetOnStart;
    [Tooltip("negative values denote time before targets are hidden; 0 is always on; set very high for no targets")]
    public float showTargetAfterSeconds;

    public bool hideNonTargets;

    public LM_Compass assistCompass;
    [Tooltip("negative values denote time before compass is hidden; 0 is always on; set very high for no compass")]
    public float SecondsUntilAssist = Mathf.Infinity;
    public Vector3 compassPosOffset;
    public Vector3 compassRotOffset;

    private float startTime;
    private Vector3 playerLastPosition;
    private float playerDistance = 0;
    private Vector3 scaledPlayerLastPosition;
    private float scaledPlayerDistance = 0;
    private float optimalDistance;

    public override void startTask()
    {
        TASK_START();
        avatarLog.navLog = true;
        if (isScaled) scaledAvatarLog.navLog = true;
    }

    public override void TASK_START()
    {
        WrongWay.SetArmed(true);

        if (!manager) Start();

        base.startTask();

        if (skip)
        {
            log.log("INFO    skip task    " + name, 1);
            return;
        }

        if (!destinations)
        {
            Debug.LogWarning("No target objects specified; task will run as" +
                " free exploration with specified time Alloted or distance alloted" +
                " (whichever is less)");

            var tmp = new List<GameObject>();
            tmp.Add(gameObject);
            gameObject.AddComponent<ObjectList>();
            gameObject.GetComponent<ObjectList>().objects = tmp;
            destinations = gameObject.GetComponent<ObjectList>();
        }

        hud.showEverything();
        hud.showScore = showScoring;

        current = destinations.currentObject();

        Debug.Log("Find " + destinations.currentObject().name);

        if (current.GetComponentInChildren<LM_TargetStore>() != null)
        {
            current.GetComponentInChildren<LM_TargetStore>().OpenDoor();
        }

        if (NavigationInstruction)
        {
            string msg = NavigationInstruction.text;
            if (destinations != null) msg = string.Format(msg, current.name);
            hud.setMessage(msg);
        }
        else
        {
            hud.SecondsToShow = 0;
        }

        if (hideNonTargets)
        {
            foreach (GameObject item in destinations.objects)
            {
                if (item.name != destinations.currentObject().name)
                    item.SetActive(false);
                else item.SetActive(true);
            }
        }

        if (hideTargetOnStart != HideTargetOnStart.Off)
        {
            if (hideTargetOnStart == HideTargetOnStart.SetInactive)
            {
                destinations.currentObject().SetActive(false);
            }
            else if (hideTargetOnStart == HideTargetOnStart.SetInvisible)
            {
                destinations.currentObject().GetComponent<MeshRenderer>().enabled = false;
            }
            else if (hideTargetOnStart == HideTargetOnStart.SetProbeTrial)
            {
                destinations.currentObject().SetActive(false);
                destinations.currentObject().GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            destinations.currentObject().SetActive(true);
            try
            {
                destinations.currentObject().GetComponent<MeshRenderer>().enabled = true;
            }
            catch (System.Exception ex) { }
        }

        startTime = Time.time;

        playerDistance = 0.0f;
        playerLastPosition = avatar.transform.position;
        if (isScaled)
        {
            scaledPlayerDistance = 0.0f;
            scaledPlayerLastPosition = scaledAvatar.transform.position;
        }

        if (isScaled)
            optimalDistance = Vector3.Distance(scaledAvatar.transform.position, current.transform.position);
        else
            optimalDistance = Vector3.Distance(avatar.transform.position, current.transform.position);

        if (assistCompass != null)
        {
            assistCompass.transform.parent = avatar.GetComponentInChildren<LM_SnapPoint>().transform;
            assistCompass.transform.localPosition = compassPosOffset;
            assistCompass.transform.localEulerAngles = compassRotOffset;
            assistCompass.gameObject.SetActive(false);
        }

        StartCoroutine(RestoreStatesNextFrame());
    }

    private IEnumerator RestoreStatesNextFrame()
    {
        LM_Progress.SetRestoring(true);
        yield return new WaitForEndOfFrame();

        if (LM_Progress.Instance.lastSaveStack == null || LM_Progress.Instance.lastSaveStack.Count == 0)
        {
            LM_Progress.SetRestoring(false);
            yield break;
        }

        var allEvents = LM_Progress.Instance.GetTriggeredColliders()
            .Where(id => id.StartsWith("WW|") || id.StartsWith("MW|"))
            .OrderBy(id =>
            {
                var parts = id.Split('|');
                return long.TryParse(parts[parts.Length - 1], out var t) ? t : 0;
            })
            .ToList();

        if (allEvents.Count == 0)
        {
            LM_Progress.SetRestoring(false);
            yield break;
        }

        foreach (var evt in allEvents)
        {
            var parts = evt.Split('|');
            if (parts[0] == "WW" && parts.Length >= 3)
            {
                var go = FindByPath(parts[1]);
                if (go != null) go.SetActive(parts[2] == "1");
            }
            else if (parts[0] == "MW" && parts.Length >= 2)
            {
                var go = FindByPath(parts[1]);
                if (go != null)
                {
                    var allMW = go.GetComponents<MoveWalls>();
                    MoveWalls mw = null;

                    if (parts.Length >= 5)
                    {
                        // New format: MW|path|localWallsName|replacementWallsName|ticks
                        string localName = parts[2];
                        string replaceName = parts[3];
                        mw = allMW.FirstOrDefault(m =>
                            (m.localWalls ? m.localWalls.name : "null") == localName &&
                            (m.replacementWalls ? m.replacementWalls.name : "null") == replaceName);
                    }

                    if (mw == null) mw = allMW.FirstOrDefault(); // fallback for old saves
                    if (mw != null) mw.ApplyWallSwapImmediate();
                }
            }
        }

        yield return new WaitForEndOfFrame();
        LM_Progress.SetRestoring(false);
    }

    private GameObject FindByPath(string path)
    {
        var parts = path.Split('/');
        GameObject current = null;

        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
            var root = scene.GetRootGameObjects().FirstOrDefault(r => r.name == parts[0]);
            if (root != null) { current = root; break; }
        }

        if (current == null)
        {
            current = Resources.FindObjectsOfTypeAll<GameObject>()
                .FirstOrDefault(g => g.name == parts[0] && g.transform.parent == null);
        }

        if (current == null) return null;

        for (int i = 1; i < parts.Length; i++)
        {
            var child = current.transform.Find(parts[i]);
            if (child == null) return null;
            current = child.gameObject;
        }
        return current;
    }

    public override bool updateTask()
    {
        return false;
    }

    public override void endTask()
    {
        TASK_END();
    }

    public override void TASK_PAUSE()
    {
        avatarLog.navLog = false;
        if (isScaled) scaledAvatarLog.navLog = false;
        log.log("TASK_PAUSE\t" + name + "\t" + this.GetType().Name + "\t", 1);
        hud.setMessage("");
        hud.showScore = false;
    }

    public override void TASK_END()
    {
        WrongWay.SetArmed(false);
        base.endTask();

        var navTime = Time.time - startTime;

        avatarLog.navLog = false;
        if (isScaled) scaledAvatarLog.navLog = false;

        if (current.GetComponentInChildren<LM_TargetStore>() != null)
        {
            current.GetComponentInChildren<LM_TargetStore>().CloseDoor();
        }

        if (canIncrementLists)
        {
            destinations.incrementCurrent();
        }

        current = destinations.currentObject();

        hud.setMessage("");
        hud.showScore = false;
        hud.SecondsToShow = hud.GeneralDuration;

        if (assistCompass != null)
        {
            assistCompass.gameObject.SetActive(false);
        }

        hud.hudPanel.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        hud.hudPanel.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);

        float perfDistance;
        if (isScaled)
            perfDistance = scaledPlayerDistance;
        else
            perfDistance = playerDistance;

        var parent = this.parentTask;
        var masterTask = parent;
        while (!masterTask.gameObject.CompareTag("Task")) masterTask = masterTask.parentTask;

        var excessPath = perfDistance - optimalDistance;

        if (skip)
        {
            navTime = float.NaN;
            perfDistance = float.NaN;
            optimalDistance = float.NaN;
            excessPath = float.NaN;
        }

        log.log("LM_OUTPUT\tNavigationTask.cs\t" + masterTask + "\t" + this.name + "\n" +
            "Task\tBlock\tTrial\tTargetName\tOptimalPath\tActualPath\tExcessPath\tRouteDuration\n" +
            masterTask.name + "\t" + masterTask.repeatCount + "\t" + parent.repeatCount + "\t" +
            destinations.currentObject().name + "\t" + optimalDistance + "\t" + perfDistance + "\t" +
            excessPath + "\t" + navTime, 1);

        if (trialLog != null && trialLog.active)
        {
            trialLog.AddData(transform.name + "_target", destinations.currentObject().name);
            trialLog.AddData(transform.name + "_actualPath", perfDistance.ToString());
            trialLog.AddData(transform.name + "_optimalPath", optimalDistance.ToString());
            trialLog.AddData(transform.name + "_excessPath", excessPath.ToString());
            trialLog.AddData(transform.name + "_duration", navTime.ToString());
        }

        Destroy(GetComponent<ObjectList>());
    }

    public override bool OnControllerColliderHit(GameObject hit)
    {
        if (hit == current)
        {
            if (showScoring)
            {
                score = score + scoreIncrement;
                hud.setScore(score);
            }
            return true;
        }

        if (hit.transform.parent == current.transform)
        {
            if (showScoring)
            {
                score = score + scoreIncrement;
                hud.setScore(score);
            }
            return true;
        }
        return false;
    }
}