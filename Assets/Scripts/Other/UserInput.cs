using UnityEngine;

public static class UserInput {

    // Adapter class for breath framework
    // Designed to easily change inputs when Fizzyo package becomes available

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
