using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelloaddelay = 5f;
    AudioSource audioSource;
    bool isTransitioning = false;
    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning)
        {
            return; //don't execute the following codes
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                DelaybeforeNextLevel();
                Debug.Log("the thing is friendly");
                break;
            case "Finish":
                
                Debug.Log("congrats!");
                break;
            //case "Fuel":
               // Debug.Log("you picked up fuel");
                //break;
            default:
                Debug.Log("you blew up!");
                StartCrashSequence();
                break;
        }
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }
    void DelaybeforeNextLevel()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelloaddelay);
    }
    void LoadNextLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = CurrentSceneIndex + 1;
        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex =0;
        }
        SceneManager.LoadScene(NextSceneIndex);
    }
    void ReloadLevel()
   {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }
}
