using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : Singleton<CameraEffect>
{
	private static Transform _myCamera;

	private static float _currentShakeIntensity = 0f;

	private static IEnumerator _shakeCoroutine = null;

	protected override void Awake()
	{
		base.Awake();

		_myCamera = Camera.main.transform;
	}

	public static void Shake(float intensity)
	{
		if(intensity >= _currentShakeIntensity)
		{
			if(_shakeCoroutine != null)
				_instance.StopCoroutine(_shakeCoroutine);
			_currentShakeIntensity = intensity;
			_shakeCoroutine = ShakeCoroutine(intensity);
			_instance.StartCoroutine(_shakeCoroutine);
		}
	}

	private static IEnumerator ShakeCoroutine(float intensity)
	{
		float current = intensity;
		float speed;
		Vector3 shakePosition;

		while(current > 0.05f)
		{
			shakePosition = Random.insideUnitSphere * current;
			// v = d/t : take 0.05s to reach shake position
			speed = Vector3.Distance(_myCamera.localPosition, shakePosition) / 0.05f;
			while(Vector3.Distance(_myCamera.localPosition, shakePosition) > 0.01f)
			{
				_myCamera.localPosition = Vector3.MoveTowards(_myCamera.localPosition, shakePosition, speed * Time.deltaTime);
				yield return null;
			}
			current *= 0.575f;	// fade away ratio
			_currentShakeIntensity = current;
		}

		speed = Vector3.Distance(_myCamera.localPosition, Vector3.zero) / 0.05f;
		
		// set camera back to 0
		while(Vector3.Distance(_myCamera.localPosition, Vector3.zero) > 0.01f)
		{
			_myCamera.localPosition = Vector3.MoveTowards(_myCamera.localPosition, Vector3.zero, speed * Time.deltaTime);
			yield return null;
		}

		_shakeCoroutine = null;
		_currentShakeIntensity = 0f;
		_myCamera.localPosition = Vector3.zero;
	}
}
