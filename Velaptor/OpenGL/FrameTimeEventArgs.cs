﻿// <copyright file="FrameTimeEventArgs.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Velaptor.OpenGL;

/// <summary>
/// Holds timing information about a single frame during a frame loop event.
/// </summary>
internal struct FrameTimeEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameTimeEventArgs"/> struct.
    /// </summary>
    /// <param name="frameTime">The amount of time that has passed for the current frame.</param>
    public FrameTimeEventArgs(double frameTime) => FrameTime = frameTime;

    /// <summary>
    /// Gets the time that the frame took to run.
    /// </summary>
    public double FrameTime { get; }
}
