using UnityEngine;
using System.Collections;

public enum ButtonType {
    GREEN,
    BLUE
}

public struct ButtonDecideResult {
    public ButtonType type;
    public Main.Events eventId;
}

public class Button : MonoBehaviour {
    private Animator anim;
    private AudioSource clip;
    

    public KeyCode key;
    public bool locked = true;
    public ButtonType buttonType;

    private Main.Events eventId;


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
                ButtonDecideResult result = new ButtonDecideResult();
                result.eventId = eventId;
                result.type = buttonType;
                (Main.Instance as Main).SendMessage("OnDecide", result);
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
