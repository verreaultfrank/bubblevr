using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

abstract public class Bubble : MonoBehaviour {
    public float currentSpotlightRange = 0;

    protected AudioSource audioSource;
    protected Rigidbody rb;

    public AudioClip explosionSound;

    
    protected void RpcSyncSpotlightRangeWithClients(float spotlightRangeToSync) {
        currentSpotlightRange = spotlightRangeToSync;
    }

    protected void Start() {
        audioSource = GameObject.FindObjectsOfType(typeof(AudioSource))[0] as AudioSource;

        rb = this.GetComponent<Rigidbody>();

        //Todo les enfants doivent se disperser en fonction de l'endroit de l'impact entre la munition et la boule parente
        if (this.tag == "Bubble") {
            rb.AddForce(new Vector3(150, 0, 150));
        } else if (this.tag == "Child1") {
            rb.AddForce(new Vector3(-150, 100, 150));
        } else {
            rb.AddForce(new Vector3(150, 100, -150));
        }
    }

    protected void Start(Vector3 push) {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(push);
    }

    // Update is called once per frame
    protected void Update() {

    }

    protected void LateUpdate() {
        //this.GetComponentInChildren<Light>().range = currentSpotlightRange;
    }


    protected int findEmptyLayer() {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<int> layerList = new List<int>();
        for (int i = 0; i < goArray.Length; i++) {
            if (goArray[i].layer != 0) {
                layerList.Add(i);
            }
        }

        layerList.Sort();

        return layerList[layerList.Count - 1] + 1;
    }
}