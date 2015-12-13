using UnityEngine;
using System.Collections;


public class Button : MonoBehaviour {
    private Animator anim;
    private AudioSource clip;
    

    public KeyCode key;
    public bool locked = true;
    public Main.Events eventId;

    void Start() {
        anim = GetComponent<Animator>();
        clip = GetComponent<AudioSource>();
    }

    void Update() {
        if (!locked)
        {
            if (Input.GetKeyDown(key))
            {
                anim.SetTrigger("pressed");
                clip.Play();

                (Main.Instance as Main).SendMessage("OnEvent", (int)eventId);
            }
        }
    }

    public void UnLock(Main.Events eventToCall) {
        eventId = eventToCall;
        locked = false;
    }

    public void Lock() {
        locked = true;        
    }
}
