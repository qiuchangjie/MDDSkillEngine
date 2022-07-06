using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class Game : MonoBehaviour
    {
        public void Start()
        {
            InitMDDComponent();
        }

        public static SkillComponent Skill
        {
            get;
            private set;
        }

        public static BuffComponent Buff
        {
            get;
            private set;
        }

        public static FsmComponent Fsm
        {
            get;
            private set;
        }

        public static EventComponent Event
        {
            get;
            private set;
        }

        public static ObjectPoolComponent ObjectPool
        {
            get;
            private set;
        }

        public static EntityComponent Entity
        {
            get;
            private set;
        }

        public static NPBehaveComponent NPBehave
        {
            get;
            private set;
        }

        public static SceneComponent Scene
        {
            get;
            private set;
        }

        public static UIComponent UI
        {
            get;
            private set;
        }

        public static HPBarComponent HpBar
        {
            get;
            private set;
        }

        public static TextBarComponent TextBar
        {
            get;
            private set;
        }

        public static ResourceComponent Resource
        {
            get;
            private set;
        }

        public static ProcedureComponent Procedure
        {
            get;
            private set;
        }

        public static DataTableComponent DataTable
        {
            get;
            private set;
        }

        public static MDDBaseComponent Base
        {
            get;
            private set;
        }

        public static SelectEntity Select
        {
            get;
            private set;
        }

        public static InputSystemComponent Input
        {
            get;
            private set;
        }

        public static CoroutineComponent Coroutine
        {
            get;
            private set;
        }

        public static CameraComponent Camera
        {
            get;
            private set;
        }

        private void InitMDDComponent()
        {
            Buff = MDDGameEntry.GetComponent<BuffComponent>();
            Fsm = MDDGameEntry.GetComponent<FsmComponent>();
            Event = MDDGameEntry.GetComponent<EventComponent>();
            ObjectPool = MDDGameEntry.GetComponent<ObjectPoolComponent>();
            Entity = MDDGameEntry.GetComponent<EntityComponent>();
            NPBehave = MDDGameEntry.GetComponent<NPBehaveComponent>();
            Scene = MDDGameEntry.GetComponent<SceneComponent>();
            UI = MDDGameEntry.GetComponent<UIComponent>();
            HpBar = MDDGameEntry.GetComponent<HPBarComponent>();
            TextBar = MDDGameEntry.GetComponent<TextBarComponent>();
            Resource = MDDGameEntry.GetComponent<ResourceComponent>();
            Procedure = MDDGameEntry.GetComponent<ProcedureComponent>();
            DataTable = MDDGameEntry.GetComponent<DataTableComponent>();
            Base = MDDGameEntry.GetComponent<MDDBaseComponent>();
            Select = MDDGameEntry.GetComponent<SelectEntity>();
            Skill = MDDGameEntry.GetComponent<SkillComponent>();
            Input = MDDGameEntry.GetComponent<InputSystemComponent>();
            Coroutine = MDDGameEntry.GetComponent<CoroutineComponent>();
            Camera = MDDGameEntry.GetComponent<CameraComponent>();
        }
    }
}


