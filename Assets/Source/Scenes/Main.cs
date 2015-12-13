using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Crab.Controllers;
using Crab.Utils;
using Crab;


public class Main : SceneScript
{

    public enum Events
    {
        NONE,
        PRESENTATION,
        GREETINGS,
        BUTTONS_SETUP,
        BUTTONS_SETUP_DONE,
        GREETINGS_NO_ANSWER,
        GREETINGS_ANSWER,
        CASE,
        CASE_READY,
        CASE_INSIST,
        CASE_DECIDE,
        CASE_2
    }

    protected EventsMap events;

    [Space()]
    public FloodLight playerLight;
    public Light consoleLight;
    public FloodLight greenLight;
    public FloodLight blueLight;
    public Crab.Event HUD;

    [Space()]
    public Console console;
    public Crab.Event buttonHolster;
    public Button greenButton;
    public Button blueButton;
    public SelectionText buttonTexts;
    public GameObject[] spawnObjects;

    [Space()]
    public Canvas blueHelp;
    public Canvas greenHelp;


    protected override void BeforeGameStart()
    {
        events = new EventsMap(this);
        consoleLight.enabled = false;
    }

    protected override void OnGameStart(PlayerController player)
    {
        events.RegistryEvent((int)Events.PRESENTATION, 2000);
        AudioListener.pause = false;
        Time.timeScale = 1;
        blueHelp.enabled = false;
        greenHelp.enabled = false;
    }

    //GameStats Handling
    //

    void Update()
    {
        events.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!HUD.IsStarted())
            {
                HUD.StartEvent();
            }
            else
            {
                HUD.FinishEvent();
            }
        }
    }

    void OnApplicationFocus(bool status)
    {
        if (!status)
        {
            HUD.StartEvent();
        }
    }

    void OnEvent(int id)
    {
        switch ((Events)id)
        {
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
                UnlockButtons(Events.GREETINGS_ANSWER, "Good Morning", "Hey");
                blueHelp.enabled = true;
                greenHelp.enabled = true;
                events.RegistryEvent((int)Events.GREETINGS_NO_ANSWER, 9000);
                break;


            case Events.GREETINGS_NO_ANSWER:
                LockButtons();
                blueHelp.enabled = false;
                greenHelp.enabled = false;
                console.Write("You are not very talkative today, Brian", 75);
                events.RegistryEvent((int)Events.CASE, 5000);
                break;

            case Events.CASE:
                console.Write("Anyway, lets start.", 150);
                console.Write("");
                console.Write("Judge Mr. Brian (Nº241)");
                console.Write("");
                console.Write("Case Nº 72");
                console.Write("Mr.Burke was an architect that built a cabin for Mr.Bender and he didn't take care of it.", 50);
                console.Write("The cabin collapsed after 2 years and Bender was injured.");
                console.Write("Whose fault it is?", 100);
                events.RegistryEvent((int)Events.CASE_READY, 20000);
                break;

            case Events.CASE_READY:
                UnlockButtons(Events.CASE_DECIDE, "Mr. Burke", "Mr. Bender");
                events.RegistryEvent((int)Events.CASE_INSIST, 20000);
                break;

            case Events.CASE_INSIST:
                console.Write("We have other cases to judge, Brian.", 75);
                console.Write("Keep going my friend.", 100);
                break;

            case Events.CASE_2:
                console.Write("");
                console.Write("Okay, one less. Next case:", 150);
                console.Write("");
                console.Write("Case Nº 56");

                break;

            default:
                Debug.LogWarning("Unknow event (id: " + id + ")");
                break;
        }
    }

    //Result: false->green true->blue 
    void OnDecide(ButtonDecideResult result)
    {
        ButtonType type = result.type;

        switch (result.eventId)
        {
            case Events.GREETINGS_ANSWER:
                events.CancelEvent((int)Events.GREETINGS_NO_ANSWER);
                LockButtons();
                blueHelp.enabled = false;
                greenHelp.enabled = false;
                console.Write("Why are you using the buttons to answer me?", 100);
                events.RegistryEvent((int)Events.CASE, 5000);
                break;

            case Events.CASE_DECIDE:
                events.CancelEvent((int)Events.CASE_INSIST);
                LockButtons();
                console.Write("");
                if (type == ButtonType.GREEN)
                {
                    console.Write("Nice choose.", 100);
                    console.Write("Maybe he could have done a better job with more budget.", 100);
                    console.Write("Mr. Bender was always very greedy.", 150);
                    console.Write("He always wanted to have more money...", 100);
                    events.RegistryEvent((int)Events.CASE_2, 10000);
                }
                else
                {
                    console.Write("So your choose is Mr. Bender", 100);
                    console.Write("He was also my choose Mr.Brian! ", 100);
                    console.Write("But Mr.Bender died. It's true that the cabin were cheap but...", 150);
                    console.Write("He only wanted the best for his family and worked so hard for it.", 100);
                    events.RegistryEvent((int)Events.CASE_2, 12000);
                }

                SpawnRandom();
                break;
        }

    }

    void LockButtons()
    {
        blueButton.Lock();
        greenButton.Lock();
        buttonTexts.FinishEvent();
        buttonTexts.SetTexts("", "");
    }

    void UnlockButtons(Events eventId, string greenValue = "", string blueValue = "")
    {
        blueButton.UnLock(eventId);
        greenButton.UnLock(eventId);
        buttonTexts.StartEvent();
        buttonTexts.SetTexts(greenValue, blueValue);
    }

    void SpawnRandom() {
        for (int i = 0; i < spawnObjects.Length; i++) {
            GameObject go = spawnObjects[Random.Range(0, spawnObjects.Length)];
            if (go && !go.activeInHierarchy)
            {
                go.SetActive(true);
                return;
            }
        }
    }

}
