﻿// <copyright file="IShaderFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using Velaptor.OpenGL.Shaders;

namespace Velaptor.Factories;

/// <summary>
/// Creates different shaders.
/// </summary>
internal interface IShaderFactory
{
    /// <summary>
    /// Creates a shader for rendering textures.
    /// </summary>
    /// <returns>The shader program.</returns>
    IShaderProgram CreateTextureShader();

    /// <summary>
    /// Creates a shader for rendering text using a font.
    /// </summary>
    /// <returns>The shader program.</returns>
    IShaderProgram CreateFontShader();

    /// <summary>
    /// Creates a shader for rendering rectangles.
    /// </summary>
    /// <returns>The shader program.</returns>
    IShaderProgram CreateRectShader();
}
