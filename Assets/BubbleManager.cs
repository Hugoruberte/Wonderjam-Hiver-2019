using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interactive.Engine;
using Tools;

public class BubbleManager : Singleton<BubbleManager>
{
	public GameObject bubblePrefab;

	public Sprite strong;
	public Sprite sugar;
	public Sprite spicy;
	public Sprite fatal;

	public float bubbleLifetime = 1f;
	public float bubbleSpeed = 5f;
	public float bubbleScaleSpeed = 1f;

	private RectTransform canvasRectTr;

	private Camera cam;

	protected override void Start()
	{
		base.Start();

		canvasRectTr = GameObject.Find("Canvas").GetComponent<RectTransform>();
		cam = Camera.main;
	}

	public void SpawnBubble(Vector3 position, Order order)
	{
		GameObject obj = Instantiate(bubblePrefab, position, Quaternion.identity);
		obj.transform.SetParent(canvasRectTr, false);

		AlcoholColor color = order.colors[Random.Range(0, order.colors.Length)];
		AlcoholAttribute attribute = order.attributes[Random.Range(0, order.attributes.Length)];

		obj.transform.GetChild(0).GetComponent<Image>().color = InteractiveEngine.instance.interactiveEngineData.GetColorWithAlcoholColor(color);

		Sprite s = GetSpriteWithAttribute(attribute.attribute);
		s = s ?? GetSpriteWithAttribute(Attribute.Strong);
		obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = s;

		obj.GetComponent<RectTransform>().anchoredPosition = Vector2Extension.WorldPositionToScreenPosition(position, canvasRectTr, cam);

		StartCoroutine(SpawnBubbleCoroutine(obj.transform));
	}

	private Sprite GetSpriteWithAttribute(Attribute a)
	{
		switch(a) {
			case Attribute.Fatal:
				return this.fatal;
			case Attribute.Spicy:
				return this.spicy;
			case Attribute.Sugar:
				return this.sugar;
			case Attribute.Strong:
				return this.strong;

			default:
				Debug.Log(a);
				return null;
		}
	}

	private IEnumerator SpawnBubbleCoroutine(Transform bubble)
	{
		float clock = 0f;
		float step = 0f;
		Vector3 from, to;

		while(clock < bubbleLifetime) {
			clock += Time.deltaTime;
			bubble.Translate(Vector3.up * bubbleSpeed * Time.deltaTime);
			yield return null;
		}

		from = bubble.localScale;
		to = Vector3.zero;

		while(step < 1f) {
			step += bubbleScaleSpeed * Time.deltaTime;
			bubble.localScale = Vector3.Lerp(from, to, step);
			yield return null;
		}

		Destroy(bubble.gameObject);
	}
}
