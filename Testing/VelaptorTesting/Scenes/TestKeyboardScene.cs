﻿// <copyright file="TestKeyboardScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace VelaptorTesting.Scenes
{
    using System;
    using System.Drawing;
    using System.Text;
    using Velaptor;
    using Velaptor.Content;
    using Velaptor.Input;
    using Velaptor.UI;
    using VelaptorTesting.Core;

    /// <summary>
    /// Used to test out if the keyboard works correctly.
    /// </summary>
    public class TestKeyboardScene : SceneBase
    {
        private const int TopMargin = 40;
        private const int LeftMargin = 5;
        private readonly Keyboard keyboard;
        private Label? instructions;
        private Label? downKeys;
        private KeyboardState currentKeyboardState;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestKeyboardScene"/> class.
        /// </summary>
        /// <param name="contentLoader">Loads content for the scene.</param>
        public TestKeyboardScene(IContentLoader contentLoader)
            : base(contentLoader) =>
                this.keyboard = new Keyboard();

        /// <inheritdoc cref="IScene.LoadContent"/>.
        public override void LoadContent()
        {
            ThrowExceptionIfLoadingWhenDisposed();

            if (IsLoaded)
            {
                return;
            }

            this.instructions = new Label(ContentLoader)
            {
                Name = "Instructions",
                Color = Color.White,
            };

            this.downKeys = new Label(ContentLoader)
            {
                Name = "DownKeys",
                Color = Color.White,
            };

            this.instructions.Text = "Hit a key on the keyboard to see if it is correct.";

            this.instructions.LoadContent();
            this.instructions.Position = new Point(LeftMargin, TopMargin);

            this.downKeys.LoadContent();

            AddControl(this.instructions);
            AddControl(this.downKeys);

            base.LoadContent();
        }

        /// <inheritdoc cref="IScene.UnloadContent"/>.
        public override void UnloadContent()
        {
            if (!IsLoaded || IsDisposed)
            {
                return;
            }

            DisposeOrUnloadContent();

            base.UnloadContent();
        }

        /// <inheritdoc cref="IUpdatable.Update"/>.
        public override void Update(FrameTime frameTime)
        {
            this.currentKeyboardState = this.keyboard.GetState();

            if (this.currentKeyboardState.GetDownKeys().Length > 0)
            {
                var downKeyText = new StringBuilder();

                foreach (var key in this.currentKeyboardState.GetDownKeys())
                {
                    downKeyText.Append(key);
                    downKeyText.Append(", ");
                }

                this.downKeys.Text = downKeyText.ToString().TrimEnd(' ').TrimEnd(',');
            }
            else
            {
                this.downKeys.Text = "No Keys Pressed";
            }

            var posX = (int)((MainWindow.WindowWidth / 2f) - (this.downKeys.Width / 2f));
            var posY = (int)((MainWindow.WindowHeight / 2f) - (this.downKeys.Height / 2f));

            this.downKeys.Position = new Point(posX, posY);

            base.Update(frameTime);
        }

        /// <inheritdoc cref="SceneBase.Dispose(bool)"/>
        protected override void Dispose(bool disposing)
        {
            if (IsDisposed || !IsLoaded)
            {
                return;
            }

            if (disposing)
            {
                DisposeOrUnloadContent();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Disposes or unloads the scenes content.
        /// </summary>
        private void DisposeOrUnloadContent()
        {
            RemoveControl(this.instructions);
            RemoveControl(this.downKeys);

            this.instructions = null;
            this.downKeys = null;
        }
    }
}