using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.Serialization;

public class NPBehaveNodeMenuTree : OdinEditorWindow
{
    
    public OdinMenuTree tree;

    [MenuItem("My Game/My Window")]
    public static NPBehaveNodeMenuTree OpenWindow()
    {
        var window =  GetWindow<NPBehaveNodeMenuTree>();

        return window;
    }


    public void Awake()
    {
        tree = new OdinMenuTree(supportsMultiSelect: true)
{
    { "Home",                           this,                           EditorIcons.House       },
    { "Odin Settings",                  null,                           EditorIcons.SettingsCog },
    { "Odin Settings/Color Palettes",   ColorPaletteManager.Instance,   EditorIcons.EyeDropper  },
    { "Odin Settings/AOT Generation",   AOTGenerationConfig.Instance,   EditorIcons.SmartPhone  },
    { "Camera current",                 Camera.current                                          },
    { "Some Class",                     null                                           }
};

        tree.AddAllAssetsAtPath("Some Menu Item", "Some Asset Path", typeof(ScriptableObject), true)
      .AddThumbnailIcons();

        tree.AddAssetAtPath("Some Second Menu Item", "SomeAssetPath/SomeAssetFile.asset");

        var customMenuItem = new OdinMenuItem(tree, "Menu Style", tree.DefaultMenuStyle);
        tree.MenuItems.Insert(2, customMenuItem);

        tree.Add("Menu/Items/Are/Created/As/Needed", new GUIContent());
        tree.Add("Menu/Items/Are/Created", new GUIContent("And can be overridden"));

       
    }

    







}
