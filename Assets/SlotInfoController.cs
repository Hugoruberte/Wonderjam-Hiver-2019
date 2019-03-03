using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interactive.Engine;

public class SlotInfoController : Singleton<SlotInfoController>
{
	private RectTransform main;
	private GameObject background;
	public Transform colorsFolder;
	public Text attributesText;
	public Text nameText;

	protected override void Awake()
	{
		base.Awake();

		background = transform.GetChild(0).gameObject;
		main = GetComponent<RectTransform>();
		this.Hide();
	}

	public void Set(Vector3 position, Ingredient ingredient)
	{
		_ChemicalElementEntity _ent = InteractiveEngine.instance.interactiveEngineData.GetChemicalElementDataWithEnum(ingredient.element);

		main.sizeDelta = new Vector2(main.sizeDelta.x, 25f * _ent.attributes.Length + 120f);

		background.SetActive(true);
	}

	public void Hide()
	{
		background.SetActive(false);
	}
}
