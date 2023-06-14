using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    public void UpdateHealthbar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    void Update() 
    {
        transform.SetPositionAndRotation(target.position + offset, camera.transform.rotation);
    }
}
