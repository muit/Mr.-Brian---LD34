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
        EXAMPLE,
        EXAMPLE_READY,
        EXAMPLE_INSIST,
        EXAMPLE_DECIDE
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
                events.RegistryEvent((int)Events.EXAMPLE, 5000);
                break;

            case Events.EXAMPLE:
                console.Write("Anyway, lets start.", 150);
                console.Write("");
                console.Write("Judge Mr. Brian (Nº241)");
                console.Write("");
                console.Write("Case Nº 72");
                console.Write("Mr.Burke was an architect that built a cabin", 50);
                console.Write("for Mr.Bender and he didn't take care of it.", 50);
                console.Write("The cabin collapsed after 2 years.");
                console.Write("Whose fault it is?", 100);
                events.RegistryEvent((int)Events.EXAMPLE_READY, 24000);
                break;

            case Events.EXAMPLE_READY:
                UnlockButtons(Events.EXAMPLE_DECIDE, "Mr. Burke", "Mr. Bender");
                events.RegistryEvent((int)Events.EXAMPLE_INSIST, 20000);
                break;

            case Events.EXAMPLE_INSIST:
                console.Write("We have other cases to judge, Brian.", 75);
                console.Write("Keep going my friend.", 100);
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
                events.RegistryEvent((int)Events.EXAMPLE, 5000);
                break;

            case Events.EXAMPLE_DECIDE:
                events.CancelEvent((int)Events.EXAMPLE_INSIST);
                LockButtons();
                console.Write("");
                if (type == ButtonType.GREEN)
                {
                    console.Write("Mr. Bender", 100);
                    console.Write("He was also my choose Mr.Brian! ", 100);
                    console.Write("But Mr.Bender died.", 150);
                    console.Write("He didnt like the verdict and wanned revenge.", 100);
                    console.Write("Ok, next case.", 150);
                }
                else
                {
                    console.Write("So your choose is Mr. Bender", 100);
                    console.Write("He was also my choose Mr.Brian! ", 100);
                    console.Write("But Mr.Bender died.", 150);
                    console.Write("He didnt like the verdict and wanned revenge.", 100);
                    console.Write("Ok, next case:", 150);
                }
                events.RegistryEvent((int)Events.EXAMPLE, 5000);
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

}
