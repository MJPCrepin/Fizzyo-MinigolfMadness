using UnityEngine;

public static class UserInput {

    // Adapter class for breath framework
    // Designed to easily change inputs when Fizzyo package becomes available

    public static bool isHoldingButtonDown()
    {
        var validInput = (
            Input.GetMouseButton(1) || // mouse control
            Input.GetKeyDown("joystick button 0") || // fizzyo device control
            Input.GetKey(KeyCode.RightArrow) ); // keyboard control
        return validInput;
    }

    public static bool isExhaling()
    {
        var validInput = (
            Input.GetMouseButton(0) || // mouse control
            isValidBreath() || // fizzyo device control
            Input.GetKey(KeyCode.UpArrow)); // keyboard control
        return validInput;
    }

    public static bool isValidBreath()
    { // Would need to know the lower and upper threshholds for a valid breath
        return Input.GetAxisRaw("Horizontal") > 0;
        // FizzyoDevice.Instance().Pressure();
    }

    public static bool aBreathIsDetected()
    { // Used to increment stroke counter in LevelContent 
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow) || isValidBreath();
    }

}
