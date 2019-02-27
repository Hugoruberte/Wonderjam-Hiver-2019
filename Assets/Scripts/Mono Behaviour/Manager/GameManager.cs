using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public GameData gameData;

	protected override void Awake()
	{
		base.Awake();

		this.CreateNewGarbage();
	}

	private void CreateNewGarbage()
	{
		GameObject g = new GameObject();
		g.name = "Garbage";
		gameData.garbage = g.transform;
	}

	public static void PutInGarbage(GameObject o)
	{
		o.SetActive(false);

		MonoBehaviour[] monos = o.GetComponentsInChildren<MonoBehaviour>();
		foreach(MonoBehaviour m in monos) {
			m.StopAllCoroutines();
		}

		o.transform.parent = instance.gameData.garbage;
	}

	public static void EmptyTheGarbage()
	{
		Destroy(instance.gameData.garbage.gameObject);

		instance.CreateNewGarbage();
	}
}
