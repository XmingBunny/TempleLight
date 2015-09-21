using UnityEditor;
using UnityEngine;

public class ExampleTreeViewPanel : EditorWindow
{
    [MenuItem("TreeView/Show Example Panel")]
    public static void ShowExampleTreeViewPanel()
    {
        CreateTreeView();
        RefreshPanel();
    }

    static ExampleTreeViewPanel m_instance = null;

    public static ExampleTreeViewPanel GetPanel()
    {
        if (null == m_instance)
        {
            m_instance = EditorWindow.GetWindow<ExampleTreeViewPanel>(false, "TreeView Panel", false);
        }

        return m_instance;

    }

    public static void RefreshPanel()
    {
        ExampleTreeViewPanel panel = GetPanel();
        panel.Repaint();
    }

    static GameObject m_treeViewGO = null;
    static TreeViewControl skillTree = null;
    const string GO_NAME = "MyTreeViewPanel";
    static void CreateTreeView()
    {
        skillTree = TreeViewInspector.AddTreeView();
        skillTree.DisplayInInspector = false;
        skillTree.DisplayOnGame = false;
        skillTree.DisplayOnScene = false;
        Example.PopulateExampleData(skillTree);
    }

    void OnEnable()
    {
        wantsMouseMove = true;
    }

    int i = 0;
    void OnGUI()
    {
        if (null == skillTree)
        {
            return;
        }
        wantsMouseMove = true;
        if (null != Event.current &&
            Event.current.type == EventType.MouseMove)
        {
            Repaint();
        }
        skillTree.DisplayTreeView(TreeViewControl.DisplayTypes.USE_SCROLL_VIEW);

        GUILayout.BeginHorizontal();
        GUILayout.Label((i++).ToString());
        GUILayout.EndHorizontal();
    }
}