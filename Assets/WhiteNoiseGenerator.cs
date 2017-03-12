using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WhiteNoiseGenerator : MonoBehaviour {

	public double frequency = 440;
	public double gain = 0.05f;

	private double increment;
	private System.Random RandomNumber = new System.Random();
	AudioLowPassFilter lowPassFilter;
	public float offset = 0;
	private double phase;
	private double sampling_frequency = 48000;
	public int lowPassFilterWaveSize;
	private int lowPassFrequency = 1000;

	// Use this for initialization
	void Awake () {
		lowPassFilter = GetComponent<AudioLowPassFilter>();
		lowPassFilter.cutoffFrequency = 800;
	}

	// Update is called once per frame
	void Update () {
		lowPassFilter.cutoffFrequency = lowPassFrequency + (lowPassFilterWaveSize * Mathf.Sin(Time.time));
	}

	void OnAudioFilterRead(float[] data, int channels) {
	for(int i = 0; i < data.Length; i++) {
		data[i] = offset - 1.0f + (float)RandomNumber.NextDouble() * 2.0f;
	}
	/*increment = frequency * 2 * Math.PI / sampling_frequency;
	for(var i = 0; i < data.Length; i = i + channels) {
		phase = phase + increment;
		data[i] = (float)(gain * Math.Sin(phase));
		if(channels == 2) data[i + 1] = data[i];
		if(phase > 2 * Math.PI) phase = 0;
	}*/
	}
}
