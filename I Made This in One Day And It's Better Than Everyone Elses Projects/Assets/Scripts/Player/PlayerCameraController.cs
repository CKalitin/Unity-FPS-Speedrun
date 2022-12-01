using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {
    [SerializeField] private FollowObject followObject;
    [SerializeField] private Transform mainMenuCameraPosition;
    [Space]
    [SerializeField] private float lerpTimeInSeconds = 0.1f;
    [Space]
    [SerializeField] private GameObject playerMesh;

    bool lerp = false;
    float lerpStep = 0f;
    float dist;

    public bool Lerp { get => lerp; set => lerp = value; }
    public float Dist { get => dist; set => dist = value; }

    private void Awake() {
        followObject.enabled = false;
        
        transform.position = mainMenuCameraPosition.position;
        transform.rotation = mainMenuCameraPosition.rotation;
    }

    private void Update() {
        if (lerp) {
            lerpStep += (1 / lerpTimeInSeconds) * Time.unscaledDeltaTime;
            transform.position = Vector3.Lerp(mainMenuCameraPosition.position, followObject.Target.position, lerpStep);
            transform.rotation = Quaternion.Lerp(mainMenuCameraPosition.rotation, followObject.Target.rotation, lerpStep);

            dist = Vector3.Distance(transform.position, followObject.Target.position);
            if (dist < 0.05f) {
                transform.position = followObject.transform.position;
                transform.rotation = followObject.Target.rotation;
                followObject.enabled = true;
                
                lerp = false;
                
                playerMesh.layer = 6; // 'Dont Render' Layer
                
                GameController.instance.ToggleCursor(false);
                GameController.instance.GameActive = true;

                FindObjectOfType<MainMenuController>().ActiveGameUI();

                Time.timeScale = 1f;
            }
        }
    }

    public void LerpToFollowObjectPosition() {
        lerp = true;
    }
}
