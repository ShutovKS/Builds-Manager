using BuildsManager.Core;
using BuildsManager.Data;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace BuildsManager.Window
{
    public class BuildManagerWindow : EditorWindow
    {
        private static GeneralBuildData Settings => MainManager.GeneralBuildData;

        private static string _settingsPath;

        private static bool _globalDataFoldout = true;
        private static bool _buildsDataFoldout = true;

        private static Vector2 _scrollPosSequence = Vector2.zero;
        private Vector2 _scrollPositionBuilds = Vector2.zero;
        
        [MenuItem("File/Builds Manager", false, 205)]
        public static void ShowWindow()
        {
            GetWindow<BuildManagerWindow>(false, "Builds Manager", true);

            MainManager.LoadSettings();
        }

        private void OnGUI()
        {
            if (Settings == null)
            {
                MainManager.LoadSettings();
            }

            _globalDataFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_globalDataFoldout, "Глобальные данные");

            if (_globalDataFoldout)
            {
                ++EditorGUI.indentLevel;

                DrawGlobalBuildData();

                --EditorGUI.indentLevel;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            
            GUILayout.Label("Build Data List", EditorStyles.boldLabel);

            _scrollPositionBuilds = EditorGUILayout.BeginScrollView(_scrollPositionBuilds);

            if (Settings != null && Settings.builds != null)
            {
                for (var i = 0; i < Settings.builds.Count; i++)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                    EditorGUILayout.LabelField($"Build {i + 1}", EditorStyles.boldLabel);

                    Settings.builds[i].isEnabled = EditorGUILayout.Toggle("Enabled", Settings.builds[i].isEnabled);
                    Settings.builds[i].isCompress = EditorGUILayout.Toggle("Compress", Settings.builds[i].isCompress);
                    Settings.builds[i].buildPath = EditorGUILayout.TextField("Build Path", Settings.builds[i].buildPath);
                    Settings.builds[i].options = (BuildOptions)EditorGUILayout.EnumPopup("Build Options", Settings.builds[i].options);
                    Settings.builds[i].target = (BuildTarget)EditorGUILayout.EnumPopup("Build Target", Settings.builds[i].target);
                    Settings.builds[i].targetGroup = (BuildTargetGroup)EditorGUILayout.EnumPopup("Target Group", Settings.builds[i].targetGroup);

                    // Можно добавить пользовательский интерфейс для настройки AddonsUsed
                    
                    if (GUILayout.Button("Remove", GUILayout.Width(80)))
                    {
                        Settings.builds.RemoveAt(i);
                        break;
                    }

                    EditorGUILayout.EndVertical();
                }
            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("Add Build"))
            {
                Settings.builds?.Add(new BuildData());
            }
            
            _buildsDataFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_buildsDataFoldout, "Данные билда");

            if (_buildsDataFoldout)
            {
                ++EditorGUI.indentLevel;

                DrawConfiguredBuilds();

                DrawPathData();

                --EditorGUI.indentLevel;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            DrawBuildButtons();

            EditorGUILayout.Space(5f);

            EditorUtility.SetDirty(Settings);
        }

        #region Main Drawers

        private static void DrawGlobalBuildData()
        {
            PlayerSettings.companyName = EditorGUILayout.TextField("Название компании", PlayerSettings.companyName);
            PlayerSettings.productName = EditorGUILayout.TextField("Название проекта", PlayerSettings.productName);
            PlayerSettings.bundleVersion = EditorGUILayout.TextField("Версия", PlayerSettings.bundleVersion);

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Для Android");
            PlayerSettings.Android.bundleVersionCode = EditorGUILayout.IntField("Версия пакета Android",
                PlayerSettings.Android.bundleVersionCode);
            PlayerSettings.Android.keystorePass =
                EditorGUILayout.TextField("Android keystore pass", PlayerSettings.Android.keystorePass);
            PlayerSettings.Android.keyaliasPass =
                EditorGUILayout.TextField("Android keyalias pass", PlayerSettings.Android.keyaliasPass);
        }

        private static void DrawBuildButtons()
        {
            var prevColor = GUI.backgroundColor;
            GUI.backgroundColor = new Color(0.773f, 0.345098f, 0.345098f);

            EditorGUILayout.Space(2.5f);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button($"Open AddonsUsed Data", GUILayout.Height(20f)))
            {
                MainManager.OpenAddonsUsedData();
            }

            if (GUILayout.Button($"Build All", GUILayout.Height(20f)))
            {
                MainManager.RunBuild();
            }

            EditorGUILayout.EndHorizontal();

            GUI.backgroundColor = prevColor;
        }

        private static void DrawConfiguredBuilds()
        {
            EditorGUILayout.BeginVertical();

            Settings.generalScriptingDefineSymbols = EditorGUILayout.TextField("Scripting Define Symbols Default",
                Settings.generalScriptingDefineSymbols);
            Settings.isReleaseBuild = EditorGUILayout.Toggle("Is Release build", Settings.isReleaseBuild);

            EditorGUILayout.EndVertical();
        }

        private static void DrawPathData()
        {
            Settings.outputRoot = EditorGUILayout.TextField("Output root", Settings.outputRoot);
            Settings.middlePath = EditorGUILayout.TextField("Middle path", Settings.middlePath);
            Settings.dirPathForPostProcess =
                EditorGUILayout.TextField("Dir path for process", Settings.dirPathForPostProcess);
        }

        #endregion

        #region Callbacks

        private static void OnBuildSelectionChanged(ReorderableList list)
        {
        }

        private static void OnBuildAdd(object target)
        {
            Settings.builds.Add((target as BuildData)?.Clone() as BuildData);
        }

        #endregion
    }
}