using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour

{

    //[SerializeField] private GameObject player;
    public Transform respawn;
    public float freezeTime = 3.0f;
    bool moving;
    public Vector3 turnAround = new Vector3(179.0f, 0.0f, 0.0f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & !moving)
        {
            
            StartCoroutine(RespawnPlayer(other.transform, respawn));
            
            
        }
        
        //player.transform.position = respawn.transform.position;
        //player.transform.parent.position = respawn.transform.position;

    }

    IEnumerator RespawnPlayer(Transform avatar, Transform destination)
    {
        moving = true;

        avatar.GetComponentInChildren<CharacterController>().enabled = false;
        avatar.position = new Vector3(respawn.position.x, avatar.position.y, destination.position.z);
        
        
        Debug.Log("Player shouldn't be able to be respawned now");
        yield return new WaitForSeconds(0);
        moving = false;
        avatar.GetComponentInChildren<CharacterController>().enabled = true;
        Debug.Log("Player should be able to be moved again");

    }
    

}
