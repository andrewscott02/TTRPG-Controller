using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ArduinoController : MonoBehaviour
{
    #region Setup

    #region Variables

    public FullBoard boardRef;

    private bool[] pins = new bool[9];

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
        /*
        UduinoManager.Instance.pinMode(AnalogPin.A0, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A1, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A2, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A3, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A4, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A5, PinMode.Input);
        */

        //End turn sensory pins
        
        UduinoManager.Instance.pinMode(2, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(3, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(4, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(5, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(6, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(7, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(8, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(9, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(10, PinMode.Input_pullup);

    }

    #endregion

    #region Input

    // Update is called once per frame
    void Update()
    {
        /*
        pins[0] = GetIsPlacedAnalog(AnalogPin.A0);
        pins[1] = GetIsPlacedAnalog(AnalogPin.A1);
        pins[2] = GetIsPlacedAnalog(AnalogPin.A2);
        pins[3] = GetIsPlacedAnalog(AnalogPin.A3);
        pins[4] = GetIsPlacedAnalog(AnalogPin.A4);
        pins[5] = GetIsPlacedAnalog(AnalogPin.A5);
        */
        
        //Board Spaces
        pins[0] = GetIsPlacedDigital(5);
        pins[1] = GetIsPlacedDigital(6);
        pins[2] = GetIsPlacedDigital(7);
        pins[3] = GetIsPlacedDigital(8);
        pins[4] = GetIsPlacedDigital(9);
        pins[5] = GetIsPlacedDigital(10);
        pins[6] = GetIsPlacedDigital(2);
        pins[7] = GetIsPlacedDigital(3);
        pins[8] = GetIsPlacedDigital(4);

        //Idle Spaces
        /*
        pins[9] = GetIsPlacedDigital(11);
        pins[10] = GetIsPlacedDigital(12);
        pins[12] = GetIsPlacedDigital(13);
        */

        int space = 7;

        bool placed = false;

        int i = 0;
        foreach (var item in pins)
        {
            Space spaceScript = boardRef.spaces[i + 9].GetComponent<Space>();

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

    bool GetIsPlacedDigital(int pinToRead)
    {
        int analogValue = UduinoManager.Instance.digitalRead(pinToRead);

        if (analogValue < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
}