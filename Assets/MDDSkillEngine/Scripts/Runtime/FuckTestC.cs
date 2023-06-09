﻿using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MDDSkillEngine
{
    /// <summary>
    /// 绘制选择框
    /// </summary>
    class FuckTestC : MonoBehaviour
    {
        //画笔颜色
        [SerializeField]
        private Color brushColor = Color.white;

        //画线的材质
        [SerializeField]
        private Material drawMaterial;

        //开始绘制标志
        [SerializeField]
        private bool isStartDraw = false;

        private bool isEndDraw = false;

        //开始和结束绘制点
        [SerializeField]
        private Vector3 mouseStartPos, mouseEndPos;

        //设置选择区域的Color
        [SerializeField]
        private Color selectionAreaColor;

        //获取绘制状态（开始绘制标志）
        public bool IsStartDraw { get => isStartDraw; set => isStartDraw = value; }

        //绘制开始坐标
        public Vector3 MouseStartPos { get => mouseStartPos; set => mouseStartPos = value; }

        //绘制结束坐标
        public Vector3 MouseEndPos { get => mouseEndPos; set => mouseEndPos = value; }

        //设置画笔颜色
        public Color BrushColor { get => brushColor; set => brushColor = value; }

        //设置选择区域的Color
        public Color SelectionAreaColor { get => selectionAreaColor; set => selectionAreaColor = value; }



        List<IEntity> entities = new List<IEntity>();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Awake()
        {
            drawMaterial = new Material(Shader.Find("UI/Default"));
            this.drawMaterial.hideFlags = HideFlags.HideAndDontSave;
            this.drawMaterial.shader.hideFlags = HideFlags.HideAndDontSave;         
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                isStartDraw = true;

                mouseStartPos = Mouse.current.position.ReadValue();
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                isStartDraw = false;

                SelectEntitys();
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    isStartDraw = true;

            //mouseStartPos = Input.mousePosition;
            //}

            //if (Input.GetMouseButtonUp(0))
            //{
            //    isStartDraw = false;
            //    SelectEntitys();
            //}
        }


        private void SelectEntitys()
        {          
            if (Game.Entity.EntityCount == 0)
                return;

            Game.Entity.GetEntityGroup("Player").GetAllEntities(entities);

            foreach (var v in entities)
            {
                Vector3 location = Game.Scene.UICamera.WorldToScreenPoint(Game.Entity.GetEntity(v.Id).Logic.CachedTransform.position);

                if (SelectUtility.IsInsert(mouseStartPos, mouseEndPos, location))
                {
                    Log.Error("EntityName:{0}", Game.Entity.GetEntity(v.Id).Logic.Name);
                }
            }          
        }


        private void OnPostRender()
        {
            if (isStartDraw)
            {
                mouseEndPos = Mouse.current.position.ReadValue();

                drawMaterial.SetPass(0);
                GL.LoadOrtho();
                //设置用屏幕坐标绘图
                GL.LoadPixelMatrix();
                DrawRect();
                DrawRectLine();
            }        
        }

        /// <summary>
        /// 绘制框选区
        /// </summary>
        private void DrawRect()
        {
            GL.Begin(GL.QUADS);
            //设置颜色和透明度
            GL.Color(selectionAreaColor);
            if ((MouseStartPos.x > MouseEndPos.x && MouseStartPos.y > MouseEndPos.y) || (MouseStartPos.x < MouseEndPos.x && MouseStartPos.y < MouseEndPos.y))
            {
                GL.Vertex3(MouseStartPos.x, MouseStartPos.y, 0);
                GL.Vertex3(MouseStartPos.x, MouseEndPos.y, 0);
                GL.Vertex3(MouseEndPos.x, MouseEndPos.y, 0);
                GL.Vertex3(MouseEndPos.x, MouseStartPos.y, 0);

            }
            else
            {
                GL.Vertex3(MouseStartPos.x, MouseStartPos.y, 0);
                GL.Vertex3(MouseEndPos.x, MouseStartPos.y, 0);
                GL.Vertex3(MouseEndPos.x, MouseEndPos.y, 0);
                GL.Vertex3(MouseStartPos.x, MouseEndPos.y, 0);
            }
            GL.End();
        }

        /// <summary>
        /// 绘制框选边框
        /// </summary>
        private void DrawRectLine()
        {
            GL.Begin(GL.LINES);
            //设置方框的边框颜色 边框不透明
            GL.Color(BrushColor);
            GL.Vertex3(MouseStartPos.x, MouseStartPos.y, 0);
            GL.Vertex3(MouseEndPos.x, MouseStartPos.y, 0);
            GL.Vertex3(MouseEndPos.x, MouseStartPos.y, 0);
            GL.Vertex3(MouseEndPos.x, MouseEndPos.y, 0);
            GL.Vertex3(MouseEndPos.x, MouseEndPos.y, 0);
            GL.Vertex3(MouseStartPos.x, MouseEndPos.y, 0);
            GL.Vertex3(MouseStartPos.x, MouseEndPos.y, 0);
            GL.Vertex3(MouseStartPos.x, MouseStartPos.y, 0);
            GL.End();
        }      
    }

}


