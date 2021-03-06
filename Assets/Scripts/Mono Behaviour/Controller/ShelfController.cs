﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class ShelfController : Singleton<ShelfController>
{
	[Header("Prefab")]
	public GameObject slotPrefab;

	[Header("Transform")]
	public Transform mainTransform;
	public Transform firstSlotsTransform;
	public Transform secondSlotsTransform;
	public Transform triggerTransform;

	private Quaternion triggerTransformStandardRotation;

	[Header("Shader")]
	public Shader lightedShader;
	public Shader grayscaleShader;

	private IEnumerator switchCoroutine = null;
	private IEnumerator leverCoroutine = null;

	private float shelfSpeed = 5f;
	public Vector3 outerShelfPosition = Vector3.zero;

	private Transform currentShelf;
	private int shelfIndex = 0;

	private const int NUMBER_INGREDIENT_BY_ROW = 8;
	private const int NUMBER_INGREDIENT_BY_COLUMN = 2;
	private const int NUMBER_INGREDIENT_BY_SHELF = NUMBER_INGREDIENT_BY_ROW * NUMBER_INGREDIENT_BY_COLUMN;

	private IngredientDatabase ingredientDatabase;
	private RecipeController recipeController;
	private SlotInfoController slotInfoController;

	protected override void Start()
	{
		base.Start();

		this.triggerTransformStandardRotation = this.triggerTransform.rotation;
		
		this.recipeController = RecipeController.instance;
		this.ingredientDatabase = InteractiveEngine.instance.ingredientDatabase;
		this.slotInfoController = SlotInfoController.instance;

		this.InitializeShelf();

		this.UpdateShelfDisplay();
	}

	private void InitializeShelf()
	{
		this.shelfIndex = 0;
		this.currentShelf = firstSlotsTransform;

		OnMouseController[] clicks;

		clicks = firstSlotsTransform.GetComponentsInChildren<OnMouseController>();
		foreach(OnMouseController click in clicks) {
			click.onClickWithReference.AddListener(SelectIngredient);
			click.onEnterWithReference.AddListener(StartDisplaySlotInfo);
			click.onExitWithReference.AddListener(EndDisplaySlotInfo);
		}

		clicks = secondSlotsTransform.GetComponentsInChildren<OnMouseController>();
		foreach(OnMouseController click in clicks) {
			click.onClickWithReference.AddListener(SelectIngredient);
			click.onEnterWithReference.AddListener(StartDisplaySlotInfo);
			click.onExitWithReference.AddListener(EndDisplaySlotInfo);
		}
	}

	private void UpdateShelfDisplay()
	{
		int index;
		SpriteRenderer rend;
		Ingredient ingredient;

		for(int i = 0; i < NUMBER_INGREDIENT_BY_SHELF; i++) {
			index = i + shelfIndex * NUMBER_INGREDIENT_BY_SHELF;

			if(index < this.ingredientDatabase.ingredients.Count - 1) {
				ingredient = this.ingredientDatabase.ingredients[index];
				rend = currentShelf.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>();
				rend.sprite = ingredient.sprite;
				rend.material.shader = (ingredient.isUnlocked && !ingredient.isUsed) ? lightedShader : grayscaleShader;
			} else {
				currentShelf.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
			}
		}
	}

	public void SwitchShelf()
	{
		Transform inner, outer;

		if(currentShelf == firstSlotsTransform) {
			inner = secondSlotsTransform;
			outer = firstSlotsTransform;
			currentShelf = secondSlotsTransform;
			shelfIndex = 1;
		} else {
			inner = firstSlotsTransform;
			outer = secondSlotsTransform;
			currentShelf = firstSlotsTransform;
			shelfIndex = 0;
		}

		if(leverCoroutine != null) {
			StopCoroutine(leverCoroutine);
		}
		leverCoroutine = LeverCoroutine();
		StartCoroutine(leverCoroutine);


		CameraEffect.Shake(0.5f);

		inner.gameObject.SetActive(true);
		this.UpdateShelfDisplay();

		if(switchCoroutine != null) {
			StopCoroutine(switchCoroutine);
		}
		switchCoroutine = SwitchShelfCoroutine(inner, outer);
		StartCoroutine(switchCoroutine);
	}
	private IEnumerator SwitchShelfCoroutine(Transform inner, Transform outer)
	{
		float step = 0f;
		Vector3 fromInner = inner.position;
		Vector3 fromOuter = outer.position;

		while(step < 1f) {
			step += shelfSpeed * Time.deltaTime;

			inner.position = Vector3.Lerp(fromInner, Vector3.zero, step);
			outer.position = Vector3.Lerp(fromOuter, outerShelfPosition, step);

			yield return null;
		}

		inner.position = Vector3.zero;
		outer.position = outerShelfPosition;

		outer.gameObject.SetActive(false);
	}
	private IEnumerator LeverCoroutine()
	{
		Quaternion from, to;
		float step = 0f;
		float speed = 7.5f;

		from = triggerTransformStandardRotation;
		to = from * Quaternion.Euler(0, 0, -75);
		while(step < 1f) {
			step += speed * Time.deltaTime;
			triggerTransform.rotation = Quaternion.Slerp(from, to, step);

			yield return null;
		}

		step = 0f;
		from = triggerTransform.rotation;
		to = triggerTransformStandardRotation;
		while(step < 1f) {
			step += speed * Time.deltaTime;
			triggerTransform.rotation = Quaternion.Slerp(from, to, step);

			yield return null;
		}
	}

	public void RemovedElementFromCombo(ChemicalElement element)
	{
		int index = this.ingredientDatabase.ingredients.FindIndex(x => x.element == element);
		this.ingredientDatabase.ingredients[index] = new Ingredient(this.ingredientDatabase.ingredients[index], false);

		this.UpdateShelfDisplay();
	}

	public void MadeCocktail(ChemicalElement element)
	{
		if(this.ingredientDatabase == null) {
			return;
		}

		this.ClearUsedIngredient();

		int index = this.ingredientDatabase.ingredients.FindIndex(x => x.element == element);
		this.ingredientDatabase.ingredients[index] = new Ingredient(true, this.ingredientDatabase.ingredients[index]);

		this.UpdateShelfDisplay();
	}

	public void ClearUsedIngredient()
	{
		if(this.ingredientDatabase == null) {
			return;
		}

		for(int i = 0; i < this.ingredientDatabase.ingredients.Count - 1; i++) {
			this.ingredientDatabase.ingredients[i] = new Ingredient(this.ingredientDatabase.ingredients[i], false);
		}

		this.UpdateShelfDisplay();
	}

	private void SelectIngredient(OnMouseController click)
	{
		int index;

		index = click.transform.GetSiblingIndex() + NUMBER_INGREDIENT_BY_SHELF * shelfIndex;

		if(index >= this.ingredientDatabase.ingredients.Count - 1) {
			return;
		}

		click.transform.GetChild(0).GetComponent<SpriteRenderer>().material.shader = grayscaleShader;
		Ingredient ingredient = this.ingredientDatabase.ingredients[index];
		this.ingredientDatabase.ingredients[index] = new Ingredient(ingredient, true);

		if(ingredient.isUnlocked) {
			CameraEffect.Shake(0.1f);
			this.recipeController.AddElementToCocktail(ingredient.element);
			click.GetComponent<Animator>().SetTrigger("Select");
		} else {
			CameraEffect.Shake(0.05f);
			click.GetComponent<Animator>().SetTrigger("Giggle");
		}
	}

	private void StartDisplaySlotInfo(OnMouseController click)
	{
		int index = click.transform.GetSiblingIndex() + NUMBER_INGREDIENT_BY_SHELF * shelfIndex;

		if(index >= this.ingredientDatabase.ingredients.Count - 1) {
			return;
		}

		Ingredient ingredient = this.ingredientDatabase.ingredients[index];

		this.slotInfoController.Set(click.transform.position, ingredient);
	}

	private void EndDisplaySlotInfo(OnMouseController click)
	{
		this.slotInfoController.Hide();
	}
}
