using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Simulation))]
public class SimulationEditor : Editor
{
    Editor _editor;
	bool foldout;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Simulation simulation = target as Simulation;

        if (simulation.settings != null) {
			foldout = EditorGUILayout.InspectorTitlebar(foldout, simulation.settings);
            if (foldout) {
                CreateCachedEditor(simulation.settings, null, ref _editor);
                _editor.OnInspectorGUI();
            }
        }
    }

    private void OnEnable () {
		foldout = EditorPrefs.GetBool (nameof (foldout), false);
	}
}
