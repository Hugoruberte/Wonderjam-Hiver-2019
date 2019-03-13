using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tools;

public class ClockManager : Singleton<ClockManager>
{
	private struct CoroutineAndTransform {
		public IEnumerator coroutine;
		public Transform transform;

		public CoroutineAndTransform(Transform t, IEnumerator c) {
			this.transform = t;
			this.coroutine = c;
		}
	}

	public GameObject clockPrefab;

	[SerializeField] private float clockScaleSpeed = 1f;

	private Camera cam;
	private RectTransform canvasRectTr;

	private Dictionary<MonsterScript, CoroutineAndTransform> coroutines = new Dictionary<MonsterScript, CoroutineAndTransform>();



	protected override void Awake()
	{
		base.Awake();

		canvasRectTr = GameObject.Find("Canvas").GetComponent<RectTransform>();
		cam = Camera.main;
	}

	public void SpawnClock(MonsterScript script, Vector3 position, float duration)
	{
		GameObject obj = Instantiate(clockPrefab);
		obj.transform.SetParent(canvasRectTr, false);

		position -= Vector3.up * 0.5f;
		obj.GetComponent<RectTransform>().anchoredPosition = Vector2Extension.WorldPositionToScreenPosition(position, canvasRectTr, cam);

		IEnumerator co = ClockCoroutine(obj.transform, duration);
		coroutines.Add(script, new CoroutineAndTransform(obj.transform, co));
		StartCoroutine(co);
	}

	public void RemoveClock(MonsterScript script)
	{
		if(this.coroutines.ContainsKey(script)) {
			Transform tr = this.coroutines[script].transform;

			StopCoroutine(this.coroutines[script].coroutine);

			StartCoroutine(ClockExitCoroutine(tr));
		}
	}

	private IEnumerator ClockCoroutine(Transform tr, float duration)
	{
		Image target = tr.GetChild(0).GetChild(0).GetComponent<Image>();
		float value = 1f;
		float speed = 1f / duration;

		while(value > 0f) {
			value -= speed * Time.deltaTime;
			target.fillAmount = value;
			yield return null;
		}

		yield return this.ClockExitCoroutine(tr);
	}

	private IEnumerator ClockExitCoroutine(Transform tr)
	{
		float step = 0f;
		Vector3 from, to;

		from = tr.localScale;
		to = Vector3.zero;

		while(step < 1f) {

			step += clockScaleSpeed * Time.deltaTime;
			tr.localScale = Vector3.Lerp(from, to, step);
			yield return null;
		}

		Destroy(tr.gameObject);
	}
}
