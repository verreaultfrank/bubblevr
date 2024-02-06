using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;

public class Press_Space_To_Fire : MonoBehaviour {
    //Drag in the Bullet Emitter from the Component Inspector.
    private FireGun bulletEmitter;

    private GameObject World;

    protected JoyButton1 joyButton1;

    public InputActionReference inputActionReference = null;

    // Use this for initialization
    void Start()
    {
        bulletEmitter = gameObject.GetComponent("Winchesters") as FireGun;

        World = GameObject.FindGameObjectWithTag("World");

        joyButton1 = FindObjectOfType<JoyButton1>();

        inputActionReference.action.performed += FireBulletEmitterFromVrController;
    }

    // Update is called once per frame
    void Update()
    {
        FireBulletEmitter();
    }

    void FireBulletEmitter() {
        float fireTime = Time.time;
        if ((Input.GetKeyDown("space") || (joyButton1 != null && joyButton1.Pressed))) {
            bulletEmitter.CmdFire();
        }
    }

    void FireBulletEmitterFromVrController(InputAction.CallbackContext context) {
        float fireTime = Time.time;
            bulletEmitter.CmdFire();
    }
}