﻿// <copyright file="IImageService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Raptor.Services
{
    using Raptor.Graphics;

    /// <summary>
    /// Saves, loads and manages image files.
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Loads a image file at the given <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The directory path to the file.</param>
        /// <returns>The image data to return.</returns>
        ImageData Load(string path);

        /// <summary>
        /// Saves the given <paramref name="image"/> to the given <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path of where to save the file.</param>
        /// <param name="image">The image to save in the file.</param>
        /// <remarks>
        ///     Saves the image in the PNG file format.
        /// </remarks>
        void Save(string path, ImageData image);

        /// <summary>
        /// Flips the given <paramref name="image"/> vertically.
        /// </summary>
        /// <param name="image">The image to flip.</param>
        /// <returns>The image flipped.</returns>
        ImageData FlipVertically(ImageData image);
    }
}