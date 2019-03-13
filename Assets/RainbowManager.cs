using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowManager : Singleton<RainbowManager>
{
	public Transform pivot;

	private Quaternion from;
	private Quaternion to;

	public TrailRenderer rainbow;

	public float speed = 1f;

	protected override void Awake()
	{
		base.Awake();

		from = pivot.rotation;
		to = from * Quaternion.Euler(0, 0, 135);
	}

	public void SpawnRainbow()
	{
		StartCoroutine(RainbowCoroutine());
	}

	private IEnumerator RainbowCoroutine()
	{
		float step = 0;

		rainbow.emitting = true;
		rainbow.Clear();
		while(step < 1f) {

			step += speed * Time.deltaTime;
			pivot.rotation = Quaternion.Slerp(from, to, step);
			yield return null;
		}
		rainbow.emitting = false;
	}
}
