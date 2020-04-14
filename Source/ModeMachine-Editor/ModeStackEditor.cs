using UnityEngine;
using UnityEditor;
using ModeMachine;

namespace ModeMachine_Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ModeStackEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (!(target is IModeStack))
            {
                base.OnInspectorGUI();
                return;
            }

            serializedObject.Update();
            base.OnInspectorGUI();
            DrawStackInspector((target as IModeStack).ModeStack); 
        }

        //editor state 
        bool expandModeList = true;
        bool expandStackDrawer = true;

        void DrawStackInspector(ModeStack targetStack)
        {
            expandStackDrawer = EditorGUILayout.Foldout(expandStackDrawer, "IModeStack", true);
            if (expandStackDrawer)
            {
                EditorGUI.indentLevel++;
                if(!Application.isPlaying)
                {
                    GUILayout.Label("[not available outside of play mode]");
                    return;
                }

                expandModeList = EditorGUILayout.Foldout(expandModeList, "Modes count: " + targetStack.Modes.Count, true);
                if (expandModeList)
                {
                    for (int i = 0; i < targetStack.Modes.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();

                        Rect clickArea = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect());
                        Event current = Event.current;

                        EditorGUI.DrawRect(clickArea, new Color(0f,0f,.2f,.1f));
                        EditorGUI.LabelField(clickArea, i.ToString(), targetStack.Modes[i].name);

                        if (clickArea.Contains(current.mousePosition) && current.type == EventType.MouseDown)
                        {
                            if(current.clickCount == 1)
                            {
                                EditorGUIUtility.PingObject(targetStack.Modes[i]);
                                current.Use();
                            }
                            else if(current.clickCount == 2)
                            {
                                Selection.activeGameObject = targetStack.Modes[i].gameObject;
                                current.Use();
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}