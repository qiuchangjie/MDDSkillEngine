using MDDSkillEngine;
using UnityEngine;
using System.Linq;
using Werewolf.StatusIndicators.Services;
using System.Collections;

namespace Werewolf.StatusIndicators.Components {
	public class AngleMissile : SpellIndicator {

		// Properties

		public override ScalingType Scaling { get { return ScalingType.LengthAndHeight; } }

		// Methods

		public override void Update() {

			base.Update();

			if(Manager != null) {

                if (Game.Select.selectEntity != null)
                {
                    Manager.transform.position = Game.Select.selectEntity.CachedTransform.position;
                    transform.position = Game.Select.selectEntity.CachedTransform.position;
                }


                Vector3 v = FlattenVector(Get3DMousePosition()) - Manager.transform.position;
				if(v != Vector3.zero) {
					Manager.transform.rotation = Quaternion.LookRotation(v);
				}
			}
		}
	}
}
