using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ArduinoController : MonoBehaviour
{
    private bool[] pins = new bool[6];

    private int currentSpace = 7;

    private ControllerScript controller;

    [Range(0, 1023)]

    public float calMin = 0f;

    [Range(0, 1023)]

    public float calMax = 1023f;

    bool endTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ControllerScript>();

        //Movement sensory pins
        UduinoManager.Instance.pinMode(AnalogPin.A0, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A1, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A2, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A3, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A4, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A5, PinMode.Input);

        //End turn sensory pins
        //UduinoManager.Instance.pinMode(2, PinMode.Input_pullup);
    }

    // Update is called once per frame
    void Update()
    {
        pins[0] = GetIsPlacedAnalog(AnalogPin.A0);
        pins[1] = GetIsPlacedAnalog(AnalogPin.A1);
        pins[2] = GetIsPlacedAnalog(AnalogPin.A2);
        pins[3] = GetIsPlacedAnalog(AnalogPin.A3);
        pins[4] = GetIsPlacedAnalog(AnalogPin.A4);
        pins[5] = GetIsPlacedAnalog(AnalogPin.A5);

        int space = 7;

        bool placed = false;

        int i = 0;
        foreach (var item in pins)
        {
            if (item == true)
            {
                space = i;
                placed = true;
                i++;
            }
            i++;
        }
        
        if (space != currentSpace)
        {
            if (placed == true)
            {
                Move(space);
                currentSpace = space;
            }
        }

        /*
        if (UduinoManager.Instance.digitalRead(2) < 1)
        {
            endTurn = true;
        }
        else
        {
            endTurn = false;
        }
        */

        //controller.Move(pin that senses);

        //controller.EndTurn(endTurn);
    }

    private void Move(int space)
    {
        if (space != null)
        {
            controller.Move(space);
        }
    }

    bool GetIsPlacedDigital(int pinToRead)
    {
        int analogValue = UduinoManager.Instance.digitalRead(pinToRead);

        float direction = MapIntToFloat(analogValue, calMin, calMax, -1f, 1f);

        if (direction > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool GetIsPlacedAnalog(AnalogPin pinToRead)
    {
        int analogValue = UduinoManager.Instance.analogRead(pinToRead);

        float direction = MapIntToFloat(analogValue, calMin, calMax, -1f, 1f);

        if (direction > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    float MapIntToFloat(int inputValue, float fromMin, float fromMax, float toMin, float toMax)
    {
        float i = ((((float)inputValue - fromMin) / (fromMax - fromMin)) * (toMax - toMin) + toMin);
        i = Mathf.Clamp(i, toMin, toMax);
        return i;
    }
}
