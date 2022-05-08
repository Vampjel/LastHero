using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	CameraScript cam_script;
	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	private CameraScript camera;
	void Awake()
	{
		camera = FindObjectOfType<CameraScript>();
		cam_script = FindObjectOfType<CameraScript>();
	}


	void Update()
	{
		Vector3 camPos;
		if(shakeDuration <= 0f)
        {
			shakeDuration = 0f;
		}
		else
        {
			camPos   = camera.transform.position;
			camTransform.localPosition = camPos + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
	}
}
