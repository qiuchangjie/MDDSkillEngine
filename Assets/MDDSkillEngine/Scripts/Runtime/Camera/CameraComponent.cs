using MDDGameFramework.Runtime;
using Cinemachine;
using MDDGameFramework;
using UnityEngine;

namespace MDDSkillEngine
{
    public class CameraComponent : MDDGameFrameworkComponent
    {
        public CinemachineBrain CameraBrain;
        public CinemachineVirtualCamera curVirtualCamera;



        public void Start()
        {
            Game.Event.Subscribe(MDDGameFramework.Runtime.LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            Game.Event.Subscribe(SelectEntityEventArgs.EventId, OnSelectEntity);
        }


        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("Cinemachine");

            if (gameObject == null)
            {
                return;
            }
            else
            {
                curVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
            }
        }

        private void OnSelectEntity(object sender, GameEventArgs e)
        {
            SelectEntityEventArgs n = (SelectEntityEventArgs)e;

            if (curVirtualCamera != null)
            {
                curVirtualCamera.Follow = n.entity.transform;
            }
        }


    }
  
}


