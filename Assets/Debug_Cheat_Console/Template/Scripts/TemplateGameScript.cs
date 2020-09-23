using System;
using System.Collections;
using System.Collections.Generic;
using Debug_Cheat_Console;
using UnityEngine;
using UnityEngine.UI;

public class TemplateGameScript : MonoBehaviour
{
    [SerializeField] 
    private Transform player;

    [SerializeField] private Text _text;
    [SerializeField] private Slider _slider;
    
    [SerializeField] private Vector2 fuelBorn = new Vector2(0,100);
    private float begin;

    private void Start()
    {
        _text.text = "Fuel";
        _slider.minValue = fuelBorn.x;
        _slider.maxValue = fuelBorn.y;
        _slider.value = fuelBorn.y;
        begin = fuelBorn.y;
        CheatConsoleManager.Instance.Subscribe(
            new DebugCommand<float>("Set_Fuel", "Set the amount of fuel", "Set_Fuel <amount>",
                amount => begin = amount),
            (command, properties) =>
                ((DebugCommand<float>) command).Invoke(float.Parse(properties[1])));
    }

    private void Update()
    {
        begin -= 1 * Time.deltaTime;
        _slider.value = begin;

         _slider.fillRect.gameObject.SetActive(begin > 0);
        
    }
}
