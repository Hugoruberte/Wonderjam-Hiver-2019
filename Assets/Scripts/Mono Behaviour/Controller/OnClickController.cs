using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class OnClickController : MonoBehaviour
{
	public UnityEvent onClick = new UnityEvent();
	public OnClickEventWithReference onClickWithReference = new OnClickEventWithReference();

	void OnMouseDown()
	{
		onClick.Invoke();
		onClickWithReference.Invoke(this);
	}
}

public class OnClickEventWithReference : UnityEvent<OnClickController> {}
