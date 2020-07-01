﻿// <copyright file="ISpriteBatch.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Raptor.Graphics
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Renders a single or batch of textures.
    /// </summary>
    public interface ISpriteBatch : IDisposable
    {
        /// <summary>
        /// Gets or sets the size of the batch.
        /// </summary>
        int BatchSize { get; set; }

        /// <summary>
        /// Gets or sets the render surface width.
        /// </summary>
        int RenderSurfaceWidth { get; set; }

        /// <summary>
        /// Gets or sets the render surface height.
        /// </summary>
        int RenderSurfaceHeight { get; set; }

        /// <summary>
        /// Starts the batch rendering process.  Must be called before calling
        /// the <see cref="Render()"/> methods.
        /// </summary>
        void BeginBatch();

        /// <summary>
        /// Ends the sprite batch process.  Calling this will render any textures
        /// still in the batch.
        /// </summary>
        void EndBatch();

        /// <summary>
        /// Renders the given texture at the given <paramref name="x"/> and <paramref name="y"/> location.
        /// </summary>
        /// <param name="texture">The texture to render.</param>
        /// <param name="x">The X location of the texture.</param>
        /// <param name="y">The y location of the texture.</param>
        /// <exception cref="Exception">Thrown if the <see cref="BeginBatch"/>() method has not been called.</exception>
        void Render(ITexture texture, int x, int y);

        /// <summary>
        /// Renders the given texture at the given <paramref name="x"/> and <paramref name="y"/> location.
        /// </summary>
        /// <param name="texture">The texture to render.</param>
        /// <param name="x">The X location of the texture.</param>
        /// <param name="y">The y location of the texture.</param>
        /// <param name="tintColor">The color to apply to the texture.</param>
        /// <exception cref="Exception">Thrown if the <see cref="BeginBatch"/>() method has not been called.</exception>
        void Render(ITexture texture, int x, int y, Color tintColor);

        /// <summary>
        /// Renders the given <see cref="Texture"/> using the given parametters.
        /// </summary>
        /// <param name="texture">The texture to render.</param>
        /// <param name="srcRect">The rectangle of the sub texture within the texture to render.</param>
        /// <param name="destRect">The destination rectangle of rendering.</param>
        /// <param name="size">The size to render the texture at. 1 is for 100%/normal size.</param>
        /// <param name="angle">The angle of rotation in degrees of the rendering.</param>
        /// <param name="tintColor">The color to apply to the rendering.</param>
        /// <exception cref="Exception">Thrown if the <see cref="BeginBatch"/>() method has not been called.</exception>
        void Render(ITexture texture, Rectangle srcRect, Rectangle destRect, float size, float angle, Color tintColor);
    }
}