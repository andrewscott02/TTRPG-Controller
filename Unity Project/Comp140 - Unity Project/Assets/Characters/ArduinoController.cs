using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ArduinoController : MonoBehaviour
{
    #region Setup

    #region Variables

    public FullBoard boardRef;

    private bool[] pins = new bool[6];

    private ControllerScript controller;

    [Range(0, 1023)]

    public float resistorThreshold = 0f;

    bool endTurn = false;

    #endregion

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

    #endregion

    #region Input

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
            Space spaceScript = boardRef.spaces[i + 6].GetComponent<Space>();

            if (item == true)
            {
                //The pin has sensed that there is a figure placed on the sensor, check if that character is the character that we intend to place

                if (spaceScript.GetSpace())
                {
                    //The space was empty, move the current character to the space
                    space = i;
                    placed = true;
                }
                else
                {
                    //There was a character on the space, check that character is the current character
                    if (spaceScript.character == controller.CheckCharacter())
                    {
                        space = i;
                        placed = true;
                    }
                }
            }
            else
            {
                //The pin has sensed that there is not a figure placed on the sensor, check if the pin had a character on the space
                if (!spaceScript.GetSpace())
                {
                    //Changes the current character to the removed character
                    controller.SelectCharacter(spaceScript.character, 0);
                    controller.IdlePosition();
                    placed = false;
                }
            }
            i++;
        }
        
        if (placed == true)
        {
            Move(space);
        }
        else
        {
            controller.IdlePosition();
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

    #endregion

    #region Movement

    private void Move(int space)
    {
        controller.Move(space);
    }

    #endregion

    #region Helper Functions

    bool GetIsPlacedAnalog(AnalogPin pinToRead)
    {
        int analogValue = UduinoManager.Instance.analogRead(pinToRead);

        if (analogValue > resistorThreshold)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    #endregion
}