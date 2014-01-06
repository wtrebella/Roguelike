using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
	[HideInInspector] public float shakeDecay = 0;
	[HideInInspector] public float shakeIntensity = 0;
	
	void Update () {
		if (shakeIntensity > 0) {
			transform.localPosition = Vector3.zero + Random.insideUnitSphere * shakeIntensity;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-shakeIntensity,shakeIntensity) * 0.2f));
			shakeIntensity -= shakeDecay;

			if (shakeIntensity <= 0) {
				transform.localPosition = Vector3.zero;
				transform.rotation = Quaternion.identity;
				shakeIntensity = 0;
			}
		}
	}
	
	public void Shake(float intensity, float decayTime) {
		shakeIntensity = intensity;
		shakeDecay = decayTime;
	}
}