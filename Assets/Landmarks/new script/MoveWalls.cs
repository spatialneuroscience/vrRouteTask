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
    void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        var exp = GameObject.FindObjectOfType<Experiment>();
        if (exp != null && !exp.goToEnded)
        {
            return;
        }
        if (other.tag == "Player")
        {
            Debug.Log("tRIGGERING THE SWITCH!");
            StartCoroutine(moveWalls());
        }
        if (other == GameObject.FindGameObjectWithTag("Player").GetComponent<LM_PlayerController>().collisionObject)
        {
            Debug.Log("tRIGGERING THE SWITCH!");
            StartCoroutine(moveWalls());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var exp = GameObject.FindObjectOfType<Experiment>();
        if (exp != null && !exp.goToEnded)
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("tRIGGERING THE SWITCH!");
            StartCoroutine(moveWalls());
        }
        
    }

    IEnumerator moveWalls()
    {
        if (localWalls)
        {
            localWalls.transform.position = localWalls.transform.position + localTransform;
        }
        yield return new WaitForSeconds(waitTime);
        if (replacementWalls)
        {
            replacementWalls.transform.position = replacementWalls.transform.position + replacementTransform;
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}