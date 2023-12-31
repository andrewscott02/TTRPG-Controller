﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ArduinoController : MonoBehaviour
{
    #region Setup

    #region Variables

    public FullBoard boardRef;

    private bool[] pins = new bool[9];

    int offset = 2;

    private ControllerScript controller;

    public int[] potentiometers = new int[3];
    public int[] characterAbilities = new int[3];
    public int[] currentCharacterAbilities = new int[3];

    [Range(0, 1023)]

    public float resistorThreshold = 0f;

    bool endTurn = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ControllerScript>();
        
        //Movement sensory pin setup
        for (int n = 0; n < pins.Length; n++)
        {
            UduinoManager.Instance.pinMode(n + offset, PinMode.Input_pullup);
        }

        UduinoManager.Instance.pinMode(AnalogPin.A0, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A1, PinMode.Input);
        UduinoManager.Instance.pinMode(AnalogPin.A2, PinMode.Input);

        /*
        UduinoManager.Instance.pinMode(2, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(3, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(4, PinMode.Input_pullup);
        */

        /*
        UduinoManager.Instance.pinMode(5, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(6, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(7, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(8, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(9, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(10, PinMode.Input_pullup);
        */
    }

    #endregion

    #region Input

    // Update is called once per frame
    void Update()
    {
        #region Movement
        for (int n = 0; n < pins.Length; n++)
        {
            pins[n] = GetIsPlacedDigital(n + offset);
        }

        int space = 9;

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

        #endregion

        #region Spell Selection

        potentiometers[0] = UduinoManager.Instance.analogRead(AnalogPin.A0);
        potentiometers[1] = UduinoManager.Instance.analogRead(AnalogPin.A1);
        potentiometers[2] = UduinoManager.Instance.analogRead(AnalogPin.A2);


        for (int n = 0; n < potentiometers.Length; n++)
        {
            characterAbilities[n] = GetAbilityNum(potentiometers[n]);

            if (controller.characters[n] != null)
            {
                if (currentCharacterAbilities[n] != characterAbilities[n])
                {
                    controller.SelectSpell(n, GetAbilityNum(potentiometers[n]));

                    currentCharacterAbilities[n] = characterAbilities[n];
                }
            }
        }

        #endregion

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

    int GetAbilityNum(int value)
    {
        if (value < 256)
        {
            return 1;
        }
        else if (value < 512)
        {
            return 2;
        }
        else if (value < 768)
        {
            return 3;
        }
        else if (value >= 768)
        {
            return 4;
        }

        return 1;
    }

    #endregion
}