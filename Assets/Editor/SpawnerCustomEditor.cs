using UnityEngine;
using UnityEditor;
using System.Collections;

//Custom Inspector for Spawner.cs
	[CustomEditor(typeof(Spawner))]
	public class SpawnerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		var spawner = (Spawner)target;
		
		spawner.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", spawner.prefab, typeof(GameObject),true);
		
		spawner.spawningType = (Spawner.Spawning)EditorGUILayout.EnumPopup("Limit Options", spawner.spawningType);
		
		if (spawner.spawningType != Spawner.Spawning.NoLimit)
		{
			spawner.limit = EditorGUILayout.IntField("Spawn Limit",spawner.limit);
			if (spawner.spawningType == Spawner.Spawning.DespawnAtLimit)
			{
				spawner.despawnTimerBool = EditorGUILayout.Toggle("Despawn Timer" , spawner.despawnTimerBool);
				
				if(spawner.despawnTimerBool)
					spawner.despawnTimer = EditorGUILayout.FloatField("Despawn Timer", spawner.despawnTimer);
			}
		}
		spawner.timer = EditorGUILayout.FloatField("Spawn Timer", spawner.timer);
		
	}
}