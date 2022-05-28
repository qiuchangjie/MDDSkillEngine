

namespace MDDSkillEngine
{
    /// <summary>
    /// 界面编号。
    /// </summary>
    public enum UIFormId : byte
    {
        Undefined = 0,

        /// <summary>
        /// 弹出框。
        /// </summary>
        DialogForm = 1,
        
        /// <summary>
        /// 开始界面
        /// </summary>
        Login = 2,

        /// <summary>
        /// 加载界面
        /// </summary>
        LoadingForm = 3,

        /// <summary>
        /// 主菜单。
        /// </summary>
        MenuForm = 100,

        /// <summary>
        /// 设置。
        /// </summary>
        SettingForm = 101,

        /// <summary>
        /// 关于。
        /// </summary>
        AboutForm = 102,

        /// <summary>
        /// 黑板
        /// </summary>
        Blackboard = 103, 
        
        /// <summary>
        /// 技能槽
        /// </summary>
        Ablities = 104,

        /// <summary>
        /// 技能库
        /// </summary>
        SkillList = 105,

        /// <summary>
        /// 拖拽层ui
        /// </summary>
        Drag = 106,
      
    }
}
