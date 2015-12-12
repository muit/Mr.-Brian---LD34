using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Crab.Controllers;
using Crab.Utils;
using Crab;


public class Main : SceneScript {

    private enum Events {
        PRESENTATION,
        GREETINGS,
        BUTTONS_SETUP,
        BUTTONS_SETUP_DONE,
        GREETINGS_NO_ANSWER,
        GREETINGS_ANSWER
    }

    protected EventsMap events;

    public FloodLight playerLight;
    public Light consoleLight;
    public FloodLight greenLight;
    public FloodLight blueLight;

    [Space()]
    public Console console;
    public Crab.Event buttons;
    public Crab.Event greetingsText;


    protected override void BeforeGameStart()
    {
        events = new EventsMap(this);
        consoleLight.enabled = false;
    }

    protected override void OnGameStart(PlayerController player) {
        events.RegistryEvent((int)Events.PRESENTATION, 2000);
    }

    //GameStats Handling
    //

    void Update() {
        events.Update();
    }

    void OnEvent(int id) {
        switch ((Events)id) {
            case Events.PRESENTATION:
                playerLight.TurnOn();
                consoleLight.enabled = true;
                events.RegistryEvent((int)Events.GREETINGS, 1000);
                break;

            case Events.GREETINGS:
                console.Write("Good Morning, Mr. Brian");
                events.RegistryEvent((int)Events.BUTTONS_SETUP, 1000);
                break;

            case Events.BUTTONS_SETUP:
                buttons.StartEvent();
                events.RegistryEvent((int)Events.BUTTONS_SETUP_DONE, 4000);
                break;

            case Events.BUTTONS_SETUP_DONE:
                greetingsText.StartEvent();
                events.RegistryEvent((int)Events.GREETINGS_NO_ANSWER, 6000);
                break;
                

            case Events.GREETINGS_NO_ANSWER:
                greetingsText.FinishEvent();
                console.Write("You are not very talkative today, Brian", 100);
                break;
            case Events.GREETINGS_ANSWER:
                events.CancelEvent((int)Events.GREETINGS_NO_ANSWER);
                greetingsText.FinishEvent();
                console.Write("Why are you using the buttons to answer me?", 100);
                break;

            default:
                Debug.LogWarning("Unknow event (id: " + id + ")");
                break;
        }
    }
}
