﻿// <copyright file="GLFWVideoMode.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System.Runtime.InteropServices;

namespace Velaptor.NativeInterop.GLFW;

/// <summary>
/// The GLFW video mode.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct GLFWVideoMode
{
    /// <summary>
    /// The width, in screen coordinates, of the GLFWVideoMode.
    /// </summary>
    public int Width;

    /// <summary>
    /// The height, in screen coordinates, of the GLFWVideoMode.
    /// </summary>
    public int Height;

    /// <summary>
    /// The bit depth of the red channel of the GLFWVideoMode.
    /// </summary>
    public int RedBits;

    /// <summary>
    /// The bit depth of the green channel of the GLFWVideoMode.
    /// </summary>
    public int GreenBits;

    /// <summary>
    /// The bit depth of the blue channel of the GLFWVideoMode.
    /// </summary>
    public int BlueBits;

    /// <summary>
    /// The refresh rate, in Hz, of the GLFWVideoMode.
    /// </summary>
    public int RefreshRate;
}
