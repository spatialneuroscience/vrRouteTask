using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionalTeleport : MonoBehaviour

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

    IEnumerator RespawnPlayer(Transform avatar, Transform distance)
    {
        moving = true;

        avatar.GetComponentInChildren<CharacterController>().enabled = false;
        Debug.Log(avatar.position);
        Debug.Log(respawn.position);
        Debug.Log(avatar.position.x + respawn.position.x);
        avatar.position = new Vector3(avatar.position.x + respawn.position.x, avatar.position.y, avatar.position.z + respawn.position.z);
        
        Debug.Log(avatar.position);
        Debug.Log("Player shouldn't be able to be respawned now");
        yield return new WaitForSeconds(0.1f);
        moving = false;
        avatar.GetComponentInChildren<CharacterController>().enabled = true;
        Debug.Log("Player should be able to be moved again");

    }
    

}