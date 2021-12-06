using MDDGameFramework;
using Animancer;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    public class Enemy : TargetableObject
    {
        EnemyData data;

        public override ImpactData GetImpactData()
        {
            return new ImpactData(data.HP,0);
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            Game.Fsm.CreateFsm<Enemy, AiStateAttribute>(this);

            Game.Buff.CreatBuffSystem(Id.ToString(),this);

            Game.HpBar.ShowHPBar(this,1,1);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            Name = "Ai";

            IFsm<Enemy> fsm = Game.Fsm.GetFsm<Enemy>(Entity.Id.ToString());

            fsm.Start<AiIdleState>();

            data = userData as EnemyData;
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            Game.HpBar.HideHPBar(this);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);         
        }     
    }
}


