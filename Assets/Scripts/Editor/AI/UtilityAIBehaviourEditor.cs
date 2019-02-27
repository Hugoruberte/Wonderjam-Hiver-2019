using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(UtilityAIBehaviour), true)]
public class UtilityAIBehaviourEditor : Editor
{
	private SerializedProperty actions;
	private SerializedProperty actionRef;
	private SerializedProperty scorers;
	private SerializedProperty scoreRef;

	private string[] actionMethodNames;
	private string[] scorerConditionMethodNames;
	private string[] scorerCurveMethodNames;

	private Rect baserect;
	private Rect cacherect;

	private readonly Color actionColor = new Color32(170, 170, 170, 255);
	private readonly Color scorerColor = new Color32(147, 147, 147, 255);

	// float val = 0f;


	void OnEnable()
	{
		actions = serializedObject.FindProperty("actions");

		SerializedProperty actionCandidateNamesProperty = serializedObject.FindProperty("actionCandidates");
		SerializedProperty scorerConditionCandidateNamesProperty = serializedObject.FindProperty("scorerConditionCandidates");
		SerializedProperty scorerCurveCandidateNamesProperty = serializedObject.FindProperty("scorerCurveCandidates");

		actionMethodNames = InitializeActionCandidateList(serializedObject.targetObject.GetType(), actionCandidateNamesProperty);

		scorerConditionMethodNames = InitializeScorerCandidateList<bool>(serializedObject.targetObject.GetType(), scorerConditionCandidateNamesProperty);
		scorerCurveMethodNames = InitializeScorerCandidateList<float>(serializedObject.targetObject.GetType(), scorerCurveCandidateNamesProperty);
	}

	public sealed override void OnInspectorGUI()
	{
		UtilityAIBehaviour script = target as UtilityAIBehaviour;

		baserect = EditorGUILayout.GetControlRect(true, 0);

		/*Rect n = new Rect();
		n.x = baserect.x;
		n.y += 500;
		n.width = 200;
		n.height = 16;
		val = EditorGUI.FloatField(n, "Val", val);*/

		baserect.height += 17;

		GUI.enabled = false;
		EditorGUI.ObjectField(baserect, "Script", MonoScript.FromScriptableObject(script), typeof(UtilityAIBehaviour), false);
		GUI.enabled = true;

		baserect.y += 20;
		EditorGUI.LabelField(baserect, "Update Interval", EditorStyles.miniLabel);
		baserect.x += 90;
		baserect.width += -95;
		script.updateRate = EditorGUI.Slider(baserect, script.updateRate, 0.02f, 1f); 

		// reset
		baserect.y += 10;
		baserect.x += -90;
		baserect.width += 95;
		baserect.height += -17;

		for(int act = 0; act < actions.arraySize; act++)
		{
			actionRef = actions.GetArrayElementAtIndex(act);
			scorers = actionRef.FindPropertyRelative("scorers");

			baserect.x += -5;
			baserect.y += 11;
			baserect.height += 50;
			EditorGUI.DrawRect(baserect, actionColor);

			baserect.x += 8;
			baserect.y += 7;
			baserect.width += -15;
			baserect.height += -38;
			this.ActionMethodField(baserect, actionRef);

			baserect.x += 12;
			baserect.y += 19;
			cacherect = baserect;
			cacherect.width = 60;
			script.displayScorers[act] = EditorGUI.Foldout(cacherect, script.displayScorers[act], "Scorers");
			
			cacherect = baserect;
			cacherect.x = baserect.width + -91;
			cacherect.y += -2;
			cacherect.width = 108;
			cacherect.height = 17;
			if(EditorApplication.isPlaying){GUI.enabled = false;}
			if(GUI.Button(cacherect, "REMOVE")) {
				script.displayScorers.RemoveAt(act);
				script.RemoveActionAt(act);
				serializedObject.Update();
				return;
			}
			if(EditorApplication.isPlaying){GUI.enabled = true;}
			// reset
			baserect.x += 108;
			baserect.y += -2;
			baserect.width += -120;
			baserect.height += 5;
			
			if(script.displayScorers[act])
			{
				baserect.x += -128;
				baserect.y += 26;
				cacherect = baserect;
				cacherect.width += 135;
				cacherect.height = (scorers.arraySize > 0) ? 68 * scorers.arraySize + 25 : 20;
				EditorGUI.DrawRect(cacherect, actionColor);


				for(int sco = 0; sco < scorers.arraySize; sco++)
				{
					baserect.x += 11;
					baserect.y += -4;
					cacherect = baserect;
					cacherect.width += 118;
					cacherect.height = 65;
					EditorGUI.DrawRect(cacherect, scorerColor);


					baserect.x += 6;
					baserect.y += 6;
					baserect.width += 106;
					baserect.height += -2;

					cacherect = baserect;
					cacherect.y += 20;
					cacherect.height = 16;

					scoreRef = scorers.GetArrayElementAtIndex(sco);

					if(script.actions[act].scorers[sco].isCondition)
					{
						this.ScorerConditionMethodField(baserect, scoreRef);
						script.actions[act].scorers[sco].score = EditorGUI.IntField(cacherect, "Score", script.actions[act].scorers[sco].score);

						cacherect.x += 82;
						cacherect.y += -21;
						cacherect.width = 39;
						cacherect.height = 15;
						script.actions[act].scorers[sco].not = GUI.Toggle(cacherect, script.actions[act].scorers[sco].not, "Not");
					}
					else
					{
						this.ScorerCurveMethodField(baserect, scoreRef);
						script.actions[act].scorers[sco].curve = EditorGUI.CurveField(cacherect, "Curve", script.actions[act].scorers[sco].curve);
					}

					baserect.x += 2;
					baserect.y += 37;
					cacherect = baserect;
					cacherect.width = 59;
					cacherect.height = 15;
					if(EditorApplication.isPlaying){GUI.enabled = false;}
					if(GUI.Button(cacherect, "REMOVE")) {
						script.actions[act].RemoveScorerAt(sco);
						serializedObject.Update();
						return;
					}
					if(EditorApplication.isPlaying){GUI.enabled = true;}

					baserect.x += -19;
					baserect.y += 29;
					baserect.width += -106;
					baserect.height += 2;
				}

				if(scorers.arraySize > 0) {
					baserect.y += 5;
				}

				cacherect = baserect;
				cacherect.x = baserect.width + 14;
				cacherect.y += -7;
				cacherect.width = 61;
				cacherect.height = 20;
				if(EditorApplication.isPlaying){GUI.enabled = false;}
				if(GUI.Button(cacherect, "BOOL")) {
					if((scorerConditionMethodNames.Length + scorerConditionMethodNames.Length) == script.actions[act].scorers.Count) {
						Debug.Log("No need to add another scorer, there is enough !");
					} else {
						script.actions[act].AddCondition(this.scorerConditionMethodNames[0]);
						serializedObject.Update();
					}
				}
				cacherect.x = baserect.width + 76;
				if(GUI.Button(cacherect, "FLOAT")) {
					if((scorerConditionMethodNames.Length + scorerConditionMethodNames.Length) == script.actions[act].scorers.Count) {
						Debug.Log("No need to add another scorer, there is enough !");
					} else {
						script.actions[act].AddCurve(this.scorerCurveMethodNames[0]);
						serializedObject.Update();
					}
				}
				if(EditorApplication.isPlaying){GUI.enabled = true;}

				baserect.x += 10;
				baserect.y += -7;

				// reset
				baserect.x += -5;
				baserect.y += 20;
				baserect.width += 135;
				baserect.height += -17;
			}
			else
			{
				// loop reset
				baserect.x += -123;
				baserect.y += 19;
				baserect.width += 135;
				baserect.height += -17;
			}
		}

		baserect.x += -5;
		baserect.y += 14;
		baserect.width += 1;
		baserect.height = 23;
		if(EditorApplication.isPlaying){GUI.enabled = false;}
		if(GUI.Button(baserect, "ADD NEW ACTION")) {
			if(actionMethodNames.Length == script.actions.Count) {
				Debug.Log($"There is no more action to add ! (Number of action found: {actionMethodNames.Length})");
			} else {
				script.displayScorers.Add(false);
				script.AddAction(this.actionMethodNames[script.actions.Count], script.actions.Count);
				serializedObject.Update();
			}
		}
		if(EditorApplication.isPlaying){GUI.enabled = true;}

		GUILayout.Space(baserect.y + 25);
		
		serializedObject.ApplyModifiedProperties();
	}





	private void ActionMethodField(Rect pos, SerializedProperty properties)
	{
		SerializedProperty methodNameProperty = properties.FindPropertyRelative("method");
		SerializedProperty indexProperty = properties.FindPropertyRelative("index");
		SerializedProperty activeProperty = properties.FindPropertyRelative("active");

		float w = pos.width;
		pos.width = 14;
		activeProperty.boolValue = EditorGUI.Toggle(pos, activeProperty.boolValue);

		pos.x += 15;
		pos.width += w - 29;

		// place holder when no candidates are available
		if(actionMethodNames.Length == 0) {
			EditorGUI.LabelField(pos, "Action", "No action found !");
			return;
		}

		// select method from candidates
		indexProperty.intValue = EditorGUI.Popup(pos, "Action", indexProperty.intValue, actionMethodNames);
		if(indexProperty.intValue < actionMethodNames.Length) {
			methodNameProperty.stringValue = actionMethodNames[indexProperty.intValue];
		} else {
			indexProperty.intValue = Array.IndexOf(actionMethodNames, methodNameProperty.stringValue);
		}
	}

	private void ScorerConditionMethodField(Rect pos, SerializedProperty properties)
	{
		SerializedProperty methodNameProperty = properties.FindPropertyRelative("method");
		SerializedProperty indexProperty = properties.FindPropertyRelative("index");

		// place holder when no candidates are available
		if(scorerConditionMethodNames.Length == 0) {
			EditorGUI.LabelField(pos, "Condition", "No condition found !");
			return;
		}

		// select method from candidates
		indexProperty.intValue = EditorGUI.Popup(pos, "Condition", indexProperty.intValue, scorerConditionMethodNames);
		if(indexProperty.intValue < scorerConditionMethodNames.Length) {
			methodNameProperty.stringValue = scorerConditionMethodNames[indexProperty.intValue];
		} else {
			indexProperty.intValue = Array.IndexOf(scorerConditionMethodNames, methodNameProperty.stringValue);
		}
	}

	private void ScorerCurveMethodField(Rect pos, SerializedProperty properties)
	{
		SerializedProperty methodNameProperty = properties.FindPropertyRelative("method");
		SerializedProperty indexProperty = properties.FindPropertyRelative("index");

		// place holder when no candidates are available
		if(scorerCurveMethodNames.Length == 0){
			EditorGUI.LabelField(pos, "Mapper", "No mapper found !" );
			return;
		}

		// select method from candidates
		indexProperty.intValue = EditorGUI.Popup(pos, "Mapper", indexProperty.intValue, scorerCurveMethodNames);
		if(indexProperty.intValue < scorerCurveMethodNames.Length) {
			methodNameProperty.stringValue = scorerCurveMethodNames[indexProperty.intValue];
		} else {
			indexProperty.intValue = Array.IndexOf(scorerCurveMethodNames, methodNameProperty.stringValue);
		}
	}

	private string[] InitializeActionCandidateList(System.Type type, SerializedProperty candidateNamesProperty)
	{
		System.Type[] paramTypes_1 = new System.Type[] {typeof(MovementController)};
		System.Type[] paramTypes_2 = new System.Type[] {typeof(MovementController), typeof(UtilityAction)};
		IList<MemberInfo> candidateList = new List<MemberInfo>();
		string[] candidateNames;
		string[] blacklist = new string[] {"AddAction", "CancelInvoke", "StopAllCoroutines"};
		int i = 0;

		type.FindMembers(
			MemberTypes.Method,
			BindingFlags.Instance | BindingFlags.Public,
			(member, criteria) => {
				MethodInfo method;
				if(((method = type.GetMethod(member.Name, paramTypes_1)) != null || (method = type.GetMethod(member.Name, paramTypes_2)) != null)
					&& (method.ReturnType == typeof(void) || method.ReturnType == typeof(IEnumerator))
					&& Array.IndexOf(blacklist, method.Name) == -1) {
					candidateList.Add(method);
					return true;
				}
				return false;
			},
			null
		);

		// clear/resize/initialize storage containers
		candidateNamesProperty.ClearArray();
		candidateNamesProperty.arraySize = candidateList.Count;
		candidateNames = new string[candidateList.Count];

		// assign storage containers
		i = 0;
		foreach(SerializedProperty element in candidateNamesProperty) {
			element.stringValue = candidateNames[i] = candidateList[i++].Name;
		}
		
		return candidateNames;
	}

	private string[] InitializeScorerCandidateList<T>(System.Type type, SerializedProperty candidateNamesProperty)
	{
		System.Type[] paramTypes = new System.Type[] {typeof(MovementController)};
		IList<MemberInfo> candidateList = new List<MemberInfo>();
		string[] candidateNames;
		string[] blacklist = new string[] {"IsInvoking", "Equals", "get_useGUILayout", "get_runInEditMode", "get_enabled", "get_isActiveAndEnabled"};
		int i = 0;

		type.FindMembers(
			MemberTypes.Method,
			BindingFlags.Instance | BindingFlags.Public,
			(member, criteria) => {
				MethodInfo method;
				if((method = type.GetMethod(member.Name, paramTypes)) != null
					&& method.ReturnType == typeof(T)
					&& Array.IndexOf(blacklist, method.Name) == -1) {
					candidateList.Add(method);
					return true;
				}
				return false;
			},
			null
		);

		// clear/resize/initialize storage containers
		candidateNamesProperty.ClearArray();
		candidateNamesProperty.arraySize = candidateList.Count;
		candidateNames = new string[candidateList.Count];

		// assign storage containers
		i = 0;
		foreach(SerializedProperty element in candidateNamesProperty) {
			element.stringValue = candidateNames[i] = candidateList[i++].Name;
		}
		
		return candidateNames;
	}
}

