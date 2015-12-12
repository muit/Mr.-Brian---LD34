using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Crab.Utils;

public class Console : MonoBehaviour
{
    public int keyDelay = 200;
    public int randomDelayDiff = 50;

    private List<string> text = new List<string>();

    private Text textComp;
    private AudioSource keySound;

    private Delay delay;
    private string line = "";

    void Start()
    {
        textComp = GetComponent<Text>();
        keySound = GetComponent<AudioSource>();
        delay = new Delay(keyDelay + UnityEngine.Random.Range(-randomDelayDiff, randomDelayDiff), true);
    }


    public void Write(string line, int delay = -1)
    {
        if (delay != -1) {
            line = "<s>" + delay + "</s>" + line;
        }

        this.line += "\n" + line;
    }

    private void RenderText()
    {
        textComp.text = String.Join("\n", text.ToArray());
    }


    void Update()
    {
        if (delay.Over() && !String.IsNullOrEmpty(line))
        {
            delay.Start(keyDelay + UnityEngine.Random.Range(-randomDelayDiff, randomDelayDiff));

            //Detect speed change
            if (line.IndexOf("<s>") == 0)
            {
                line = line.Substring(3, line.Length - 3);
                int endIndex = line.IndexOf("</s>");

                keyDelay = int.Parse(line.Substring(0, endIndex));
                line = line.Substring(endIndex+4, line.Length - (endIndex + 4));
            }


            if (line.IndexOf("\n") == 0)
            {
                text.Add("");
                line = line.Substring(1, line.Length - 1);

                if (text.Count >= 4)
                {
                    text.RemoveAt(0);
                }
            }
            else
            {
                char letter = line[0];
                line = line.Substring(1, line.Length - 1);

                text[text.Count - 1] += letter;

                //Play Sound
                if (letter != ' ')
                    keySound.Play();
            }

            RenderText();
        }
    }
}
