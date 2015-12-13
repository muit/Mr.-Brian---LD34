using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Crab.Controllers;
using Crab.Utils;
using Crab;


public class Main : SceneScript {

    public enum Events {
        NONE,
        PRESENTATION,
        GREETINGS,
        BUTTONS_SETUP,
        BUTTONS_SETUP_DONE,
        GREETINGS_NO_ANSWER,
        GREETINGS_ANSWER,
        EXAMPLE1,
        EXAMPLE_DECIDE
    }

    protected EventsMap events;

    public FloodLight playerLight;
    public Light consoleLight;
    public FloodLight greenLight;
    public FloodLight blueLight;

    [Space()]
    public Console console;
    public Crab.Event buttonHolster;
    public Button greenButton;
    public Button blueButton;
    public Crab.Event greetingsText;

    [Space()]
    public Canvas greenHelp;
    public Canvas blueHelp;


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

        if (Input.GetKeyDown(KeyCode.Escape)) {

        }
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
                events.RegistryEvent((int)Events.BUTTONS_SETUP, 2000);
                break;

            case Events.BUTTONS_SETUP:
                buttonHolster.StartEvent();
                events.RegistryEvent((int)Events.BUTTONS_SETUP_DONE, 4000);
                break;

            case Events.BUTTONS_SETUP_DONE:
                greetingsText.StartEvent();
                greenButton.UnLock(Events.GREETINGS_ANSWER);
                blueButton.UnLock(Events.GREETINGS_ANSWER);
                greenHelp.enabled = true;
                blueHelp.enabled = true;

                events.RegistryEvent((int)Events.GREETINGS_NO_ANSWER, 5000);
                break;
                

            case Events.GREETINGS_NO_ANSWER:
                blueButton.Lock();
                greenButton.Lock();
                greenHelp.enabled = false;
                blueHelp.enabled = false;
                greetingsText.FinishEvent();
                console.Write("You are not very talkative today, Brian", 75);
                events.RegistryEvent((int)Events.EXAMPLE1, 5000);
                break;
            case Events.GREETINGS_ANSWER:
                events.CancelEvent((int)Events.GREETINGS_NO_ANSWER);
                blueButton.Lock();
                greenButton.Lock();
                greenHelp.enabled = false;
                blueHelp.enabled = false;
                greetingsText.FinishEvent();
                console.Write("Why are you using the buttons to answer me?", 100);
                events.RegistryEvent((int)Events.EXAMPLE1, 5000);
                break;

            case Events.EXAMPLE1:
                console.Write("Anyway, lets start.", 150);
                console.Write("");
                console.Write("Judge Nº241");
                console.Write("");
                console.Write("");
                break;

            case Events.EXAMPLE_DECIDE:

                break;



            default:
                Debug.LogWarning("Unknow event (id: " + id + ")");
                break;
        }
    }

    //Result: false->green true->blue 
    void OnDecide(Events id, bool result) {

    }

}
