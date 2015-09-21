using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;

public class SkillEditor : EditorWindow
{
    static Dictionary<int, Skill> skillTable = new Dictionary<int, Skill>();
    static List<string> actionNames = new List<string>();
    static TreeViewControl skillTree;
    static TreeViewItem curItem;


    static string FixedPath = "Assets/Resources/FightData/Skill/";  //技能数据存储位置.
    static string ResPath = "FightData/Skill/";

    public class ItemData
    {
        public ItemType type = ItemType.None;
        public string resPath = "";
        public int skillId = 0;
    }

    public enum ItemType
    {
        Root = 1,
        Skill = 2,
        Event = 3,
        Aciton = 4,
        None = 5
    }

    [MenuItem("SkillEditor/ShowWindow")]
    static void Show()
    {
        InitSkillTable();
        InitActionNames();
        InitSkillTree();
        RefreshPanel();
    }

    static void InitSkillTable()
    {
        skillTable.Clear();
        DirectoryInfo FolderInfo = new DirectoryInfo(FixedPath);

        foreach (var dir in FolderInfo.GetDirectories())
        {
            Skill skill = Resources.Load<Skill>("FightData/Skill/" + dir.Name + "/Skill");
            skillTable[skill.ID] = skill;
        }
    }

    static void InitActionNames()
    {
        try
        {
            actionNames.Clear();
            string actionPath = "Assets/Scripts/Skill/SkillEvent/SkillAction/";
            DirectoryInfo dirInfo = new DirectoryInfo(actionPath);

            foreach (var file in dirInfo.GetFiles())
            {
                if (!file.Name.EndsWith(".cs") || file.Name == "SkillAction.cs")
                    continue;

                actionNames.Add(file.Name.Replace(file.Extension, ""));
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    static void InitSkillTree()
    {
        skillTree = TreeViewInspector.AddTreeView();
        skillTree.DisplayInInspector = false;
        skillTree.DisplayOnGame = false;
        skillTree.DisplayOnScene = false;

        curItem = skillTree.RootItem;
        curItem.Header = "所有技能";
        ItemData itemData = new ItemData();
        itemData.type = ItemType.Root;
        curItem.DataContext = itemData;
        AddEvents(curItem);

        AddTreeItem();
    }

    private static void AddTreeItem()
    {
        SkillEditor editor=GetPanel();
        foreach (var skill in skillTable.Values)
        {
            TreeViewItem skillItem = editor.AddSkill(skillTree.RootItem, skill.ID);

            for (int i = 0; i < skill.ActiveEvents.Count; i++)
            {
                TreeViewItem eventItem = editor.AddEvent(skillItem, skill.ActiveEvents[i].name);
                for (int j = 0; j < skill.ActiveEvents[i].actions.Count; j++)
                {
                    editor.AddAction(eventItem, skill.ActiveEvents[i].actions[j].name);
                }
            }

            for (int i = 0; i < skill.AnimEvents.Count; i++)
            {
                TreeViewItem eventItem = editor.AddEvent(skillItem, skill.AnimEvents[i].name);
                for (int j = 0; j < skill.AnimEvents[i].actions.Count; j++)
                {
                    editor.AddAction(eventItem, skill.AnimEvents[i].actions[j].name);
                }
            }

        }
    }

    static void AddEvents(TreeViewItem item)
    {
        AddSelectHandler(out item.Selected);
    }

    static void AddSelectHandler(out System.EventHandler handler)
    {
        handler = new System.EventHandler(Handler);
    }

    static void Handler(object sender, System.EventArgs args)
    {
        curItem = sender as TreeViewItem;
        Selection.activeObject = Resources.Load((curItem.DataContext as ItemData).resPath);
    }

    static void RefreshPanel()
    {
        SkillEditor editor = GetPanel();
        editor.Repaint();
    }

    static SkillEditor window;

    public static SkillEditor GetPanel()
    {
        if (window == null)
        {
            window = EditorWindow.GetWindow<SkillEditor>(false, "Skill　Editor", true);
        }

        return window;
    }

    int skillId;
    int selectIndex1;
    int selectIndex2;
    int i = 0;
    void OnEnable()
    {
        wantsMouseMove = true;
    }
    void OnGUI()
    {
        //可以增加gui函数更新的速度.
        wantsMouseMove = true;
        if (null != Event.current &&
            Event.current.type == EventType.MouseMove)
        {
            Repaint();
        }

        if (skillTree == null)
            return;

        //显示树结构
        skillTree.DisplayTreeView(TreeViewControl.DisplayTypes.USE_SCROLL_VIEW);

        if (curItem == null)
            return;

        ItemData curItemData = curItem.DataContext as ItemData;

        if (curItemData.type == ItemType.Root)
        {
            skillId = EditorGUILayout.IntField("技能ID", skillId);
            if (GUILayout.Button("添加技能"))
            {
                AddSkill(curItem, skillId, true);
            }
        }
        if (curItemData.type == ItemType.Skill)
        {
            GUILayout.BeginHorizontal();
            string[] list = { "--请选择--", "AnimEvent" };
            selectIndex1 = EditorGUILayout.Popup("添加事件", selectIndex1, list);
            if (GUILayout.Button("添加") && selectIndex1 != 0)
            {
                AddEvent(curItem, list[selectIndex1], true);
            }
            GUILayout.EndHorizontal();
        }

        if (curItemData.type == ItemType.Event)
        {
            GUILayout.BeginHorizontal();
            List<string> list = new List<string>();
            list.Add("--请选择--");
            list.AddRange(actionNames);
            selectIndex2 = EditorGUILayout.Popup("添加行为", selectIndex2, list.ToArray());
            if (GUILayout.Button("添加") && selectIndex2 != 0)
            {
                AddAction(curItem, list[selectIndex2], true);
            }
            GUILayout.EndHorizontal();
        }

    }

    /// <summary>
    /// 添加技能
    /// </summary>
    TreeViewItem AddSkill(TreeViewItem parent, int skillId, bool createAsset = false)
    {
        try
        {
            //技能节点的数据
            ItemData itemData = new ItemData();
            itemData.resPath = ResPath + skillId + "/Skill";
            itemData.type = ItemType.Skill;

            //增加技能节点
            TreeViewItem item = parent.AddItem(skillId.ToString());
            item.DataContext = itemData;
            AddEvents(item);

            if (createAsset)
            {
                Skill skill = ScriptableObject.CreateInstance<Skill>();
                skill.ID = skillId;
                skillTable.Add(skill.ID, skill);
                AssetEditor.CreateAsset(skill, FixedPath + skillId, "Skill");
                TreeViewItem activeEventItem = AddEvent(item, "ActiveEvent", true);
                AddAction(activeEventItem, "AnimationAction", true);
                AddAction(activeEventItem, "FinishAction", true);
                AddEvent(item, "AnimEvent", true);
            }

            return item;
        }
        catch (Exception e)
        {
            this.ShowNotification(new GUIContent(e.Message));
        }

        return null;
    }

    TreeViewItem AddEvent(TreeViewItem parent, string type, bool createAsset = false)
    {
        try
        {
            int skillId = (parent.DataContext as ItemData).skillId;
            string name = type;
            if (createAsset)
            {
                Skill skill = skillTable[skillId];
                Assembly ass = typeof(SkillEvent).Assembly;
                SkillEvent skillEvent = System.Activator.CreateInstance(ass.GetType(type)) as SkillEvent;
                if (skillEvent is ActiveEvent)
                {
                    name = "ActiveEvent";
                    skill.ActiveEvents.Add(skillEvent as ActiveEvent);

                }
                else
                {
                    if (skill.AnimEvents.Count > 0)
                        name = type + skill.AnimEvents.Count;
                    else
                        name = type;
                    skill.AnimEvents.Add(skillEvent as AnimEvent);

                }
                AssetEditor.CreateAsset(skillEvent, FixedPath + skillId + "/" + name, name);
                EditorUtility.SetDirty(skill);
            }

            ItemData itemData = new ItemData();
            itemData.resPath = ResPath + skillId + "/" + name + "/" + name;
            itemData.type = ItemType.Event;

            TreeViewItem item = parent.AddItem(name);
            item.Header = name;
            item.DataContext = itemData;
            AddEvents(item);

            return item;
        }
        catch (Exception e)
        {
            this.ShowNotification(new GUIContent(e.Message));
        }
        return null;
    }

    TreeViewItem AddAction(TreeViewItem parent, string type, bool createAsset = false)
    {
        try
        {
            ItemData parentData = parent.DataContext as ItemData;
            string name = type;
            string[] arr = parentData.resPath.Split('/');
            string actionResPath = "";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                actionResPath += arr[i];
                actionResPath += "/";
            }

            if (createAsset)
            {
                Assembly ass = typeof(SkillAction).Assembly;
                SkillAction skillAction = System.Activator.CreateInstance(ass.GetType(type)) as SkillAction;

                SkillEvent parentEvent = Resources.Load<SkillEvent>(parentData.resPath);
                parentEvent.actions.Add(skillAction);

                int count = 0;
                foreach (var act in parentEvent.actions)
                {
                    if (act.name.StartsWith(type))
                        count++;
                }

                if (count > 0)
                    skillAction.name = type + count.ToString();
                else
                    skillAction.name = type;
                name = skillAction.name;

                AssetEditor.CreateAsset(skillAction, "Assets/Resources/" + actionResPath + "Actions", name);
                EditorUtility.SetDirty(parentEvent);
            }

            ItemData itemData = new ItemData();
            itemData.resPath = actionResPath + "Actions/" + name;
            itemData.type = ItemType.Aciton;

            TreeViewItem item = parent.AddItem(name);
            item.Header = name;
            item.DataContext = itemData;
            AddEvents(item);



            return item;
        }
        catch (Exception e)
        {
            this.ShowNotification(new GUIContent(e.Message));
        }
        return null;
    }
}
