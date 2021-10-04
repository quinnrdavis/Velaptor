﻿// <copyright file="TestRenderGraphicsScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace VelaptorTesting.Scenes
{
    using System.Drawing;
    using Velaptor;
    using Velaptor.Content;
    using Velaptor.Graphics;
    using VelaptorTesting.Core;

    /// <summary>
    /// Tests that graphics properly render to the screen.
    /// </summary>
    public class TestRenderGraphicsScene : SceneBase
    {
        private IAtlasData? mainAtlas;
        private AtlasSubTextureData[]? frames;
        private int elapsedTime;
        private int currentFrame;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRenderGraphicsScene"/> class.
        /// </summary>
        /// <param name="contentLoader">Loads content for the scene.</param>
        public TestRenderGraphicsScene(IContentLoader contentLoader)
            : base(contentLoader)
        {
        }

        /// <inheritdoc cref="IScene.LoadContent"/>
        public override void LoadContent()
        {
            ThrowExceptionIfLoadingWhenDisposed();

            if (IsLoaded)
            {
                return;
            }

            this.mainAtlas = ContentLoader.Load<IAtlasData>("Main-Atlas");
            this.frames = this.mainAtlas.GetFrames("square");

            base.LoadContent();
        }

        /// <inheritdoc cref="IScene.UnloadContent"/>
        public override void UnloadContent()
        {
            if (!IsLoaded || IsDisposed)
            {
                return;
            }

            this.mainAtlas = null;
            this.frames = null;

            base.UnloadContent();
        }

        /// <inheritdoc cref="IUpdatable.Update"/>
        public override void Update(FrameTime frameTime)
        {
            if (this.elapsedTime >= 32)
            {
                this.elapsedTime = 0;

                this.currentFrame = this.currentFrame >= this.frames.Length - 1
                    ? 0
                    : this.currentFrame + 1;
            }
            else
            {
                this.elapsedTime += frameTime.ElapsedTime.Milliseconds;
            }
        }

        /// <inheritdoc cref="IDrawable.Render"/>
        public override void Render(ISpriteBatch spriteBatch)
        {
            var sqrPosX = (MainWindow.WindowWidth / 2) - (this.frames[this.currentFrame].Bounds.Width / 2);
            var sqrPosY = (MainWindow.WindowHeight / 2) - (this.frames[this.currentFrame].Bounds.Height / 2);

            spriteBatch.Render(
                this.mainAtlas.Texture,
                this.frames[this.currentFrame].Bounds,
                new Rectangle(sqrPosX, sqrPosY, this.mainAtlas.Width, this.mainAtlas.Height),
                1f,
                0f,
                Color.White,
                RenderEffects.None);
            base.Render(spriteBatch);
        }

        /// <inheritdoc cref="SceneBase.Dispose(bool)"/>
        protected override void Dispose(bool disposing)
        {
            if (IsDisposed || !IsLoaded)
            {
                return;
            }

            base.Dispose(disposing);
        }
    }
}