using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MDDSkillEngine
{
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// 朝向
        /// </summary>
        public Quaternion rotation { get; }

        /// <summary>
        /// 坐标
        /// </summary>
        public Vector3 position { get; }

        /// <summary>
        /// 速度
        /// </summary>
        public Vector3 velocity { get; }

        /// <summary>
        /// 速度
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// 是否到达目的地
        /// </summary>
        public bool reachedDestination { get; }


        /// <summary>
        /// 距离目的地的距离
        /// </summary>
        public float remainingDistance { get; }

        /// <summary>
        /// 目的地坐标
        /// </summary>
        public Vector3 destination { get; set; }

        public bool canSearch { get; set; }

        public bool canMove { get; set; }

        /// <summary>
        /// 代理此时是否路径
        /// </summary>
        public bool hasPath { get; }

        /// <summary>
        /// 路径是否在计算中
        /// </summary>
        public bool pathPending { get; }

        /// <summary>
        /// 想开始或者停止代理
        /// </summary>
        public bool isStopped { get; set; }

        /// <summary>
        /// 每次计算路径时开始调用
        /// </summary>
        public System.Action onSearchPath { get; set; }

        /// <summary>
        /// 重新计算路径的频率
        /// </summary>
        public float repathRate = 0.5F;

        /// <summary>
        /// 开始计算当前的路径
        /// </summary>
        public void SearchPath()
        {

        }

        /// <summary>
        /// 设定路径
        /// </summary>
        public void SetPath(Path path)
        {
            
        }


        /// <summary>
        /// 瞬移
        /// </summary>
        /// <param name="newPosition"></param>
        /// <param name="clearPath"></param>
        public void Teleport(Vector3 newPosition, bool clearPath = true)
        {
            
        }
        

        /// <summary>
        /// 外部影响的移动
        /// </summary>
        public void Move()
        {
            
        }

        /// <summary>
        /// 计算移动坐标
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="nextPosition"></param>
        /// <param name="nextRotation"></param>
        public void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
        {
            nextPosition = Vector3.one;
            nextRotation = new Quaternion();
        }


        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="nextPosition"></param>
        /// <param name="nextRotation"></param>
        public void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation)
        {
            
        }
    }

}

