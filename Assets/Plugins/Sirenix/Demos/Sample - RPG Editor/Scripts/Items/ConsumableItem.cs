#if UNITY_EDITOR
using OdinSerializer;
using UnityEngine;

namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    public class ConsumableItem : Item
    {
        [SuffixLabel("seconds ", true)]
        [BoxGroup(STATS_BOX_GROUP)]
        [SerializeField]
        private float Cooldown;

        [HorizontalGroup(STATS_BOX_GROUP + "/Dur")]
        public bool ConsumeOverTime;

        [HideLabel]
        [HorizontalGroup(STATS_BOX_GROUP + "/Dur")]
        [SuffixLabel("seconds ", true), EnableIf("ConsumeOverTime")]
        [LabelWidth(20)]
        public float Duration;

        [VerticalGroup(LEFT_VERTICAL_GROUP)]
        public StatList Modifiers;

        public override ItemTypes[] SupportedItemTypes
        {
            get
            {
                return new ItemTypes[]
                {
                    ItemTypes.Consumable,
                    ItemTypes.Flask
                };
            }
        }
    }
}
#endif
