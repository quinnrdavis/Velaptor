﻿// <copyright file="SceneBase.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Velaptor;
using Velaptor.Content;
using Velaptor.Graphics;
using Velaptor.UI;

namespace VelaptorTesting.Core;

/// <summary>
/// A base scene to be used for creating new custom scenes.
/// </summary>
public abstract class SceneBase : IScene
{
    private readonly List<IControl> controls = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneBase"/> class.
    /// </summary>
    /// <param name="contentLoader">Loads content for a scene.</param>
    protected SceneBase(IContentLoader contentLoader)
    {
        ContentLoader = contentLoader;
        IsActive = false;
    }

    /// <inheritdoc cref="IScene.Name"/>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the list of controls that have been added to the scene.
    /// </summary>
    public ReadOnlyCollection<IControl> Controls => this.controls.ToReadOnlyCollection();

    /// <inheritdoc cref="IScene.Id"/>
    public Guid Id { get; } = Guid.NewGuid();

    /// <inheritdoc cref="IScene.Name"/>
    public bool IsLoaded { get; private set; }

    /// <inheritdoc cref="IScene.IsActive"/>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets a value indicating whether or not the scene has been disposed.
    /// </summary>
    protected bool IsDisposed { get; private set; }

    /// <summary>
    /// Gets the content loader to load scene content.
    /// </summary>
    protected IContentLoader ContentLoader { get; }

    /// <inheritdoc cref="IScene.AddControl"/>
    public void AddControl(IControl control) => this.controls.Add(control);

    /// <inheritdoc cref="IScene.RemoveControl"/>
    public void RemoveControl(IControl control) => this.controls.Remove(control);

    /// <summary>
    /// Gets all of the controls that match the given type <typeparamref name="TControlType"/>.
    /// </summary>
    /// <typeparam name="TControlType">The type of control to return.</typeparam>
    /// <returns>A list of controls whose type that matches the type <typeparamref name="TControlType"/>.</returns>
    public IControl[] GetControls<TControlType>()
        where TControlType : IControl
    {
        return (from c in this.controls
            where c is TControlType
            select c).ToArray();
    }

    /// <inheritdoc cref="IScene.LoadContent"/>
    public virtual void LoadContent()
    {
        foreach (var control in this.controls)
        {
            control.LoadContent();
        }

        IsLoaded = true;
    }

    /// <inheritdoc cref="IScene.UnloadContent"/>
    public virtual void UnloadContent()
    {
        if (!IsLoaded)
        {
            return;
        }

        foreach (var control in this.controls)
        {
            control.UnloadContent();
        }

        this.controls.Clear();
        IsLoaded = false;
    }

    /// <inheritdoc cref="IUpdatable.Update"/>
    public virtual void Update(FrameTime frameTime)
    {
        if (IsLoaded is false || IsActive is false)
        {
            return;
        }

        foreach (var control in this.controls)
        {
            control.Update(frameTime);
        }
    }

    /// <inheritdoc cref="IDrawable.Render"/>
    public virtual void Render(IRenderer renderer)
    {
        if (renderer == null)
        {
            throw new ArgumentNullException(nameof(renderer), "The parameter must not be null.");
        }

        if (IsLoaded is false)
        {
            return;
        }

        foreach (var control in this.controls)
        {
            control.Render(renderer);
        }
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// <inheritdoc cref="IDisposable.Dispose"/>
    /// </summary>
    /// <param name="disposing">Disposes managed resources when <c>true</c>.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            foreach (var control in this.controls)
            {
                control.UnloadContent();
            }

            this.controls.Clear();
        }

        IsDisposed = true;
    }
}
