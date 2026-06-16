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

	void Start () {
		Debug.Log("Project Root Folder: " + System.IO.Directory.GetCurrentDirectory());
		cameraCon =player.transform as Transform;
		cameraRig =camerarig.transform as Transform;

		experiment = GameObject.FindWithTag ("Experiment");
		manager = experiment.GetComponent("Experiment") as Experiment;
		log = manager.dblog;
		avatar = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // Log the name of the tracked object, it's body position, body rotation, and camera (head) rotation
		if (true){
            //print("AVATAR_POS	" + "\t" +  avatar.position.ToString("f3") + "\t" + "AVATAR_Body " + "\t" +  cameraCon.localEulerAngles.ToString("f3") +"\t"+ "AVATAR_Head " + cameraRig.localEulerAngles.ToString("f3"));
			Debug.Log("📝 avatarLog is sending data to dbLog right now!");

            log.log("Avatar: \t" + avatar.name + "\t" +
                    "Position (xyz): \t" + cameraCon.position.x + "\t" + cameraCon.position.y + "\t" + cameraCon.position.z + "\t" +
                    "Rotation (xyz): \t" + cameraCon.eulerAngles.x + "\t" + cameraCon.eulerAngles.y + "\t" + cameraCon.eulerAngles.z + "\t" +
                    "Camera   (xyz): \t" + cameraRig.eulerAngles.x + "\t" + cameraRig.eulerAngles.y + "\t" + cameraRig.eulerAngles.z + "\t"
                    , 1);
            log.log("Avatar: \t" + avatar.name + "\t" +
                    "Position (xyz): \t" + cameraCon.position.x + "\t" + cameraCon.position.y + "\t" + cameraCon.position.z + "\t" +
                    "Rotation (xyz): \t" + cameraCon.eulerAngles.x + "\t" + cameraCon.eulerAngles.y + "\t" + cameraCon.eulerAngles.z + "\t" +
                    "Camera   (xyz): \t" + cameraRig.eulerAngles.x + "\t" + cameraRig.eulerAngles.y + "\t" + cameraRig.eulerAngles.z + "\t"
                    , 1);
        }
	}

	void OnApplicationQuit() {
		if (log != null) {
			log.close();
			Debug.Log("Log file closed safely.");
		}
	}
}
