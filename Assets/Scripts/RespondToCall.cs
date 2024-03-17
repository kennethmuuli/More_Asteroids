using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public static class RespondToCall
{
    public static bool ShouldRespondToCall(int difficultyCallStep, int currentCallNum){
        float remainder = currentCallNum % difficultyCallStep;
        if(remainder == 0) {
            return true;
        } else return false;
    }
}
