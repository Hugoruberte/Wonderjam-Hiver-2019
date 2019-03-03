using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interactive.Engine;
using Tools;

public class SlotInfoController : Singleton<SlotInfoController>
{
	private RectTransform main;
	private GameObject background;
	public Transform colorsFolder;
	public Text attributesText;
	public Text nameText;

	private StringBuilder stringBuilder = new StringBuilder();

	private Camera cam;
	private RectTransform canvasRectTr;

	protected override void Awake()
	{
		base.Awake();

		canvasRectTr = GameObject.Find("Canvas").GetComponent<RectTransform>();
		cam = Camera.main;

		background = transform.GetChild(0).gameObject;
		main = GetComponent<RectTransform>();
		this.Hide();
	}

	public void Set(Vector3 position, Ingredient ingredient)
	{
		_ChemicalElementEntity _ent = InteractiveEngine.instance.interactiveEngineData.GetChemicalElementDataWithEnum(ingredient.element);

		if(ingredient.sprite == null) {
			_ent = InteractiveEngine.instance.interactiveEngineData.GetChemicalElementDataWithEnum(ChemicalElement.Voidd);
		} else {
			_ent = InteractiveEngine.instance.interactiveEngineData.GetChemicalElementDataWithEnum(ingredient.element);
		}

		main.sizeDelta = new Vector2(main.sizeDelta.x, 25f * _ent.attributes.Length + 120f);
		main.anchoredPosition = Vector2Extension.WorldPositionToScreenPosition(position, canvasRectTr, cam);

		stringBuilder.Clear();
		foreach(AlcoholAttribute a in _ent.attributes) {
			stringBuilder.Append(a);
			stringBuilder.Append("\n");
		}
		attributesText.text = stringBuilder.ToString();

		for(int i = 0; i < _ent.colors.Length; i++) {
			colorsFolder.GetChild(i).GetComponent<Image>().color = InteractiveEngine.instance.interactiveEngineData.GetColorWithAlcoholColor(_ent.colors[i]);
		}
		for(int i = _ent.colors.Length; i < colorsFolder.childCount; i++) {
			colorsFolder.GetChild(i).GetComponent<Image>().color = Color.clear;
		}

		background.SetActive(true);
	}

	public void Hide()
	{
		background.SetActive(false);
	}
}
