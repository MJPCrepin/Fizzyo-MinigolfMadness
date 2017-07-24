using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Designed to easily change inputs when Fizzyo package becomes available
public static class UserInput {

    public static bool isHoldingButtonDown()
    {
        return Input.GetMouseButton(1);
    }

    public static bool isExhaling()
    {
        return Input.GetMouseButton(0);
    }

    public static bool isValidBreath()
    {
        return true;
    }

    public static int getBreathCount()
    {
        return 1;
    }

}
