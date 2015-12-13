using UnityEngine;

public class SelectionText : MonoBehaviour {
    public TextMesh greenText;
    public TextMesh blueText;

    public void SetTexts(string greenValue, string blueValue) {
        greenText.text = greenValue;
        blueText.text = blueValue;
    }


    public void StartEvent()
    {
        GetComponent<Crab.Event>().StartEvent();
    }

    public void FinishEvent()
    {
        GetComponent<Crab.Event>().FinishEvent();
    }
}
