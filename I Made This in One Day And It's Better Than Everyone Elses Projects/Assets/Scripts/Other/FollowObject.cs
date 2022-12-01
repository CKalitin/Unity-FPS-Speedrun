using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
    [SerializeField] private Transform target;

    public Transform Target { get => target; set => target = value; }

    private void Update() {
        transform.rotation = target.rotation;
        transform.position = target.position;
    }
}
