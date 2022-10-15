#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Fracture))]
public class FractureEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Fracture script = (Fracture) target;
		script.DestroyParticles = EditorGUILayout.Toggle(
			new GUIContent("Destroy Particles", "If we should destroy the generated particles."), script.DestroyParticles
		);
		using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(script.DestroyParticles)))
		{
			if (group.visible)
			{
				EditorGUI.indentLevel++;
				script.DestroyParticlesAfterSeconds = EditorGUILayout.FloatField(new GUIContent("Seconds", "Seconds to wait before destroying the particles"), script.DestroyParticlesAfterSeconds);
				EditorGUI.indentLevel--;
			}
		}
		script.UseGravity = EditorGUILayout.Toggle(
			new GUIContent("Use Gravity", "Apply gravity to the fragmented objects."),
			script.UseGravity
		);
		script.Force = EditorGUILayout.FloatField(
			new GUIContent("Force", "The force of the fragmentation. e.g. How far away objects are pushed."),
			script.Force
		);
		script.Radius = EditorGUILayout.FloatField(
			new GUIContent("Radius", "The radius of objects that are affected by the explosion that are affected."),
			script.Radius
		);
		script.Audio = (AudioSource) EditorGUILayout.ObjectField(
			new GUIContent("Audio", "Audio source to play when triggering the explosion."),
			script.Audio,
			typeof(AudioSource)
		);
		script.Debris = (GameObject) EditorGUILayout.ObjectField(
			new GUIContent("Debris", "Game Object to use as a parent for the fragmented pieces generated."),
			script.Debris,
			typeof(GameObject)
		);
		script.Filter = (MeshFilter) EditorGUILayout.ObjectField(
			new GUIContent("Filter", "Mesh filter to use generate the triangles from."),
			script.Filter,
			typeof(MeshFilter)
		);
		script.Renderer = (MeshRenderer) EditorGUILayout.ObjectField(
			new GUIContent("Renderer", "Mesh renderer to get the materials from."),
			script.Renderer,
			typeof(Renderer)
		);
	}
}
#endif