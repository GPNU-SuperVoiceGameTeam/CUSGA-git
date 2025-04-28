using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PickupsController : MonoBehaviour
{
    private Light2D _light;
    private float maxIntensity = 2f;
    private float minIntensity = 0f;
    private float transitionSpeed = 2f;
    private bool isIncreasing = true;

    void Start()
    {
        _light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isIncreasing)
            {
                // 如果当前是增加状态，向最大强度过渡
                _light.intensity = Mathf.MoveTowards(_light.intensity, maxIntensity, Time.deltaTime * transitionSpeed);
                // 如果达到最大强度，切换到减少状态
                if (_light.intensity >= maxIntensity)
                {
                    isIncreasing = false;
                }
            }
            else
            {
                // 如果当前是减少状态，向最小强度过渡
                _light.intensity = Mathf.MoveTowards(_light.intensity, minIntensity, Time.deltaTime * transitionSpeed);
                // 如果达到最小强度，切换到增加状态
                if (_light.intensity <= minIntensity)
                {
                    isIncreasing = true;
                }
            }
        
    }
}
