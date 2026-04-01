using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWalls : MonoBehaviour
{
    public GameObject localWalls; //these are the walls that need to get moved out of the way
    public GameObject replacementWalls; //these are the walls that replace the first section
    public Vector3 localTransform; //how much to move the localWalls
    public Vector3 replacementTransform; //how much to move the replacementWalls
    public float waitTime; //when replacing the objects that move the walls, a short wait time is used so the walls don't get switched multiple times per walk through
    // Start is called before the first frame update
    private Quaternion _quaternion;
    private float scaleFactor = 1f;
    void Start()
    {
        _quaternion = GameObject.FindGameObjectWithTag("maze").transform.rotation;
        //Debug.Log("rotation: " + rotation);
        //_quaternion = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        // _quaternion = 
        // Auto-calculate scale relative to base size of 7
        Vector3 mazeScale = GameObject.FindGameObjectWithTag("maze").transform.localScale;
        scaleFactor = mazeScale.x; // assumes uniform XZ scale
        Debug.Log("Calculated scaleFactor: " + scaleFactor);
        Debug.Log("floor size: " + GameObject.FindGameObjectWithTag("floor").GetComponent<Renderer>().bounds.size);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter fired by: " + other.gameObject.name + " | tag: " + other.tag);

        var exp = GameObject.FindObjectOfType<Experiment>();
        Debug.Log("Experiment found: " + (exp != null) + (exp != null ? " | goToEnded: " + exp.goToEnded : ""));
        if (exp != null && !exp.goToEnded)
        {
            Debug.Log("STOPPED: experiment has ended");
            return;
        }

        var playerController = GameObject.FindGameObjectWithTag("Player")
                                ?.GetComponent<LM_PlayerController>();

        bool isPlayer = other.CompareTag("Player");
        bool isCollisionObj = playerController != null
                            && other == playerController.collisionObject;

        Debug.Log("isPlayer: " + isPlayer + " | isCollisionObj: " + isCollisionObj + " | playerController found: " + (playerController != null));

        if (isPlayer || isCollisionObj)
        {
            Debug.Log("TRIGGERING wall move!");
            StartCoroutine(moveWalls());
        }
        else
        {
            Debug.Log("STOPPED: neither isPlayer nor isCollisionObj was true");
        }
    }
    IEnumerator moveWalls()
    {

        if (localWalls)
        {
            localWalls.transform.position = localWalls.transform.position + _quaternion * localTransform * scaleFactor;
        }
        yield return new WaitForSeconds(waitTime);
        if (replacementWalls)
        {
            replacementWalls.transform.position = replacementWalls.transform.position + _quaternion * replacementTransform * scaleFactor;
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}