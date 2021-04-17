using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;

    public void setMaxStamina(float maxStamina)
    {
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void setCurrentStamina(float newStamina)
    {
        if (newStamina > 0)
        {
            staminaBar.value = newStamina;
        } else
        {
            staminaBar.value = 0; //visual reasons
        }
    }
}
