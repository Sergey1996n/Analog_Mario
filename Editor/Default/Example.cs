using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine;

public class Example : MonoBehaviour
{
	/*ывафыв*/
    //    private void OnDrawGizmos()
    //    {
    //        if (RunTestsFromMenu.isError)
    //            GizmosUtils.DrawText(GUI.skin, "Custom text", Vector3.zero);
    //    }
    //    internal static void CreateRoot()
    //    {
    //        GameObject[] gameObjects = MonoBehaviour.FindObjectsOfType<GameObject>().ToArray();
    //        GameObject root = new GameObject("Root");

    //        foreach (var gameObject in gameObjects)
    //        {
    //            var g = MonoBehaviour.Instantiate(gameObject);
    //            //PrefabUtility.IsPartOfPrefabAsset(Selection.activeObject as GameObject).transform.SetParent(root.transform, false);
    //            g.transform.SetParent(root.transform, false);
    //        }
    //        if (!Directory.Exists("Assets/Resources"))
    //        {
    //            AssetDatabase.CreateFolder("Assets", "Resources");
    //        }

    //        string pathPrefab = "Assets/Resources/Root.prefab";

    //        if (File.Exists(pathPrefab))
    //        {
    //            AssetDatabase.DeleteAsset(pathPrefab);
    //        }

    //        string localPath = AssetDatabase.GenerateUniqueAssetPath(pathPrefab);

    //        PrefabUtility.SaveAsPrefabAssetAndConnect(root, localPath, InteractionMode.UserAction, out bool prefabSuccess);
    //        MonoBehaviour.DestroyImmediate(root);
    //    }

    //    internal static void DeleteRoot()
    //    {
    //        string pathPrefab = "Assets/Resources/Root.prefab";
    //        if (File.Exists(pathPrefab))
    //        {
    //            AssetDatabase.DeleteAsset(pathPrefab);
    //        }
    //    }ываыва
    /*ываячс*/
}


public class RunTestsFromMenu : ScriptableObject, ICallbacks
{
    private bool isError = false;
    private TestMode mode;
    private int numberLesson = 1;
    private static string pathProgram;

    [MenuItem("Tools/Check Work &c")]
    public static void DoRunTests()
    {
        pathProgram = Path.Combine(Directory.GetParent(Application.persistentDataPath).FullName, "Unity learning");
        var pathInput = Path.Combine(pathProgram, "testInput.txt");
        try {
            using (var sw = new StreamReader(pathInput))
            {
                sw.ReadLine();
            }
        }
        catch {
            
        }
        //CreateInstance<RunTestsFromMenu>().StartTestRunPlayMode();
        CreateInstance<RunTestsFromMenu>().StartTestRunEditMode();


    }
    private void StartTestRunEditMode()
    {
        hideFlags = HideFlags.HideAndDontSave;
        mode = TestMode.EditMode;
        CreateInstance<TestRunnerApi>().Execute(new ExecutionSettings
        (new Filter()
        {
            testMode = TestMode.EditMode,
            testNames = new string[] { "Lesson" + numberLesson }
        }
        ));
    }
    private void StartTestRunPlayMode()
    {
        hideFlags = HideFlags.HideAndDontSave;
        mode = TestMode.PlayMode;
        CreateInstance<TestRunnerApi>().Execute(new ExecutionSettings
        (new Filter()
        {
            testMode = TestMode.PlayMode,
            testNames = new string[] { "Lesson" + numberLesson }
        }
        ));
    }

    public void OnEnable() { CreateInstance<TestRunnerApi>().RegisterCallbacks(this); }

    public void OnDisable() { CreateInstance<TestRunnerApi>().UnregisterCallbacks(this); }
    public void RunFinished(ITestResultAdaptor results)
    {
        if (numberLesson == 0) {
            WarningEditorGUI.Init();
            WarningEditorGUI.Text = "Запустите \"Unity leaning\" для проверки тестов";
            return;
        }

        Debug.Log(results.AssertCount);

        if (mode == TestMode.PlayMode && results.FailCount == 0)
        {
            //pathProgram = Path.Combine(Directory.GetParent(Application.persistentDataPath).FullName, "Unity learning");
            var pathResult = Path.Combine(pathProgram, "testResults.txt");

            using (var sw = new StreamWriter(pathResult))
            {
                sw.WriteLine(numberLesson);
            }
        }

        if (mode == TestMode.EditMode && results.FailCount == 0)
        {
            Debug.Log("Количество ошибочных тестов: " + results.FailCount);
            Debug.Log("Не должен заходить");
            CreateInstance<RunTestsFromMenu>().StartTestRunPlayMode();
        }
        DestroyImmediate(this);

        //Application.Quit(results.FailCount > 0 ? 1 : 0);

    }

    public void RunStarted(ITestAdaptor testsToRun) { }

    public void TestStarted(ITestAdaptor test) { }

    public void TestFinished(ITestResultAdaptor result)
    {

        if (result.FailCount == 1 && !isError)
        {
            isError = true;
            WarningEditorGUI.Init();
            WarningEditorGUI.Text = result.Message;
        }
        else if (!isError)
        {
            WarningEditorGUI.Text = result.Message;
        }
    }
}

public class WarningEditorGUI : EditorWindow
{
    private static string text = "";
    private Vector2 scrollPos = Vector2.zero;
    internal static string Text
    {
        set => text = value;
    }
    internal static void Init()
    {
        EditorWindow window = GetWindow(typeof(WarningEditorGUI));
        window.Show();
    }


    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 14;
        myStyle.normal.textColor = Color.white;


        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        //EditorGUILayout.TextArea(text, GUILayout.ExpandHeight(true));
        GUILayout.Label(text, myStyle);
        //EditorGUILayout.TextArea;
        EditorGUILayout.EndScrollView();
        //GUILayout.FlexibleSpace();
        this.Repaint();

    }
}
