using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public Text text;

    public Health health;

    private void Update()
    {
        float currentHealthFloat = (health.GetHealth() * 100);

        text.text = currentHealthFloat.ToString();
    }
}
