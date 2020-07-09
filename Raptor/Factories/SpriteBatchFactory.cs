﻿// <copyright file="SpriteBatchFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Raptor.Factories
{
    using System.Diagnostics.CodeAnalysis;
    using Raptor.Graphics;

    /// <summary>
    /// Creates instances of a sprite batch.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SpriteBatchFactory
    {
        /// <summary>
        /// Initializes and instance of a <see cref="ISpriteBatch"/>.
        /// </summary>
        /// <param name="renderSurfaceWidth">The width of the render surface.</param>
        /// <param name="renderSurfaceHeight">The height of the render surface.</param>
        /// <returns>A raptor implemented sprite batch.</returns>
        public static ISpriteBatch CreateSpriteBatch(int renderSurfaceWidth, int renderSurfaceHeight)
        {
            var result = IoC.Container.GetInstance<ISpriteBatch>();

            result.RenderSurfaceWidth = renderSurfaceWidth;
            result.RenderSurfaceHeight = renderSurfaceHeight;

            return result;
        }
    }
}