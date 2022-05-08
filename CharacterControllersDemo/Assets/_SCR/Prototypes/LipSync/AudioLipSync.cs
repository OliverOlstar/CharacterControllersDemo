using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class AudioLipSync : MonoBehaviour
{
	[System.Serializable]
	public class Point
	{
		[SerializeField, Min(0)] private float decreaseRate = 1.0f;
		public int timeOffset = 50;
		[SerializeField, Min(0)] private float scale = 2.0f;
		[SerializeField, Min(0)] private float xPosition = 0.0f;

		private float value = 0.0f;

		public void Update(int pIndex, int pTotal, float pLoudness, LineRenderer pBottem, LineRenderer pTop, float pDeltaTime)
		{
			value -= pDeltaTime * decreaseRate;
			value = Mathf.Max(value, pLoudness, 0.0f);

			pTop.SetPosition(pIndex, new Vector3(-xPosition, value * scale, 0.0f));
			pBottem.SetPosition(pIndex, new Vector3(-xPosition, value * -scale, 0.0f));
			
			if (pIndex < Mathf.FloorToInt((float)pTotal * 0.5f))
			{
				pTop.SetPosition((pTotal - 1) - pIndex, new Vector3(xPosition, value * scale, 0.0f));
				pBottem.SetPosition((pTotal - 1) - pIndex, new Vector3(xPosition, value * -scale, 0.0f));
			}
		}
	}

	private AudioSource source;
	[SerializeField] private LineRenderer bottemMouth = null;
	[SerializeField] private LineRenderer topMouth = null;
	
	[SerializeField] private Point[] points = new Point[3];

	[Header("Scale")]
	[SerializeField] private float sizeDecreaseRate = 1.0f;
	[SerializeField] private float sizeIncreaseRate = 1.0f;
	[SerializeField] private float sizeScale = 2.0f;
	[SerializeField] private float sizeMinScale = 1.0f;

	private float sizeValue = 0.0f;

	public void SizeUpdate(float pLoudness, float pDeltaTime)
	{
		// Decrease
		if (sizeValue > 0.0f)
			sizeValue -= pDeltaTime * sizeDecreaseRate;
		sizeValue = Mathf.Max(sizeValue, 0.0f);

		// Increase
		if (sizeValue < pLoudness)
			sizeValue += sizeIncreaseRate * pDeltaTime;

		// Set
		transform.localScale = Vector3.one * ((sizeValue * sizeScale) + sizeMinScale);
	}

	[Space]
	public float updateStep = 0.1f;
	public int sampleDataLength = 1024;
 
	private float currentUpdateTime = 0f;
	private float[] clipSampleData;

	private void Start() 
	{
		source = GetComponent<AudioSource>();
		clipSampleData = new float[sampleDataLength];
	}

	private void Update() 
	{
		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep) 
		{
			int total = ((points.Length - 1) * 2) + 1;
			for (int i = 0; i < points.Length; i++)
			{
				float loudness = GetLoadness(source.timeSamples - points[i].timeOffset);
				points[i].Update(i, total, loudness, bottemMouth, topMouth, currentUpdateTime);

				if (i == points.Length - 1)
					SizeUpdate(loudness, currentUpdateTime);
			}
			
			currentUpdateTime = 0f;
		}
	}

	private float GetLoadness(int pTimeSamples)
	{
		if (pTimeSamples < 0 || source.isPlaying == false)
			return 0.0f;

		source.clip.GetData(clipSampleData, pTimeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
		float clipLoudness = 0.0f;
		foreach (var sample in clipSampleData) 
		{
			clipLoudness += Mathf.Abs(sample);
		}
		clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
		return clipLoudness;
	}
}
