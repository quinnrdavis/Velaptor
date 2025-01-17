﻿// <copyright file="SilkWindowFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using Silk.NET.Windowing;

namespace Velaptor.Factories;

/// <inheritdoc/>
[ExcludeFromCodeCoverage]
internal sealed class SilkWindowFactory : IWindowFactory
{
    private static IWindow? window;

    /// <inheritdoc/>
    public IWindow CreateSilkWindow()
    {
        if (window is not null)
        {
            return window;
        }

        var windowOptions = WindowOptions.Default;
        windowOptions.ShouldSwapAutomatically = false;

        window = Window.Create(windowOptions);
        return window;
    }
}
