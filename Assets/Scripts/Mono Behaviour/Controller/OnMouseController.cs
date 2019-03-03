using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class OnMouseController : MonoBehaviour
{
	public UnityEvent onClick = new UnityEvent();
	public OnEventWithReference onClickWithReference = new OnEventWithReference();

	public UnityEvent onEnter = new UnityEvent();
	public OnEventWithReference onEnterWithReference = new OnEventWithReference();

	public UnityEvent onExit = new UnityEvent();
	public OnEventWithReference onExitWithReference = new OnEventWithReference();

	void OnMouseDown()
	{
		onClick.Invoke();
		onClickWithReference.Invoke(this);
	}

	void OnMouseEnter()
	{
		onEnter.Invoke();
		onEnterWithReference.Invoke(this);
	}

	void OnMouseExit()
	{
		onExit.Invoke();
		onExitWithReference.Invoke(this);
	}
}

public class OnEventWithReference : UnityEvent<OnMouseController> {}
