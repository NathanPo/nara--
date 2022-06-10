using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour {
    
    public Slider slider;
    public Gradient gradient;
	public Image fill;
	public int maxValue = 10;


	private void Start() {
		slider.minValue = 0;
		slider.maxValue = 10;
		fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(int health) {
		this.slider.value += health;
	}

	public int GetHealth() {
		return (int)Math.Round(this.slider.value);
	}

	// public void Disable() {
	// 	this.slider.SetActive(false);
	// }
}
