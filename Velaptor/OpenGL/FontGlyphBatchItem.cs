﻿// <copyright file="FontGlyphBatchItem.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Text;
using Velaptor.Graphics;

namespace Velaptor.OpenGL;

/// <summary>
/// A single item in a batch of glyph items that can be rendered to the screen.
/// </summary>
internal struct FontGlyphBatchItem : IEquatable<FontGlyphBatchItem>
{
    /// <summary>
    /// The source rectangle inside of the font atlas texture to render.
    /// </summary>
    public RectangleF SrcRect;

    /// <summary>
    /// The destination rectangular area of where to render the glyph on the screen.
    /// </summary>
    public RectangleF DestRect;

    /// <summary>
    /// The font glyph.
    /// </summary>
    public char Glyph;

    /// <summary>
    /// The size of the glyph texture to be rendered.
    /// </summary>
    /// <remarks>This must be a value between 0 and 1.</remarks>
    public float Size;

    /// <summary>
    /// The angle in degrees of the glyph texture.
    /// </summary>
    /// <remarks>Needs to be a value between 0 and 360.</remarks>
    public float Angle;

    /// <summary>
    /// The color to apply to the entire glyph texture.
    /// </summary>
    public Color TintColor;

    /// <summary>
    /// The type of effects to apply to the glyph texture when rendering.
    /// </summary>
    public RenderEffects Effects;

    /// <summary>
    /// The size of the viewport.
    /// </summary>
    public SizeF ViewPortSize;

    /// <summary>
    /// The ID of the font atlas texture.
    /// </summary>
    public uint TextureId;

    /// <summary>
    /// Returns a value indicating whether or not the <paramref name="left"/> operand is equal to the <paramref name="right"/> operand.
    /// </summary>
    /// <param name="left">The left operand compared with the right operand.</param>
    /// <param name="right">The right operand compared with the left operand.</param>
    /// <returns>True if both operands are equal.</returns>
    public static bool operator ==(FontGlyphBatchItem left, FontGlyphBatchItem right) => left.Equals(right);

    /// <summary>
    /// Returns a value indicating whether or not the <paramref name="left"/> operand is not equal to the <paramref name="right"/> operand.
    /// </summary>
    /// <param name="left">The left operand compared with the right operand.</param>
    /// <param name="right">The right operand compared with the left operand.</param>
    /// <returns>True if both operands are not equal.</returns>
    public static bool operator !=(FontGlyphBatchItem left, FontGlyphBatchItem right) => !(left == right);

    /// <summary>
    /// Gets a value indicating whether or not the current <see cref="FontGlyphBatchItem"/> is empty.
    /// </summary>
    /// <returns>True if empty.</returns>
    public bool IsEmpty() =>
        this.TextureId == 0 &&
        this.Glyph == '\0' &&
        this.SrcRect.IsEmpty &&
        this.DestRect.IsEmpty &&
        this.Size == 0f &&
        this.Angle == 0f &&
        this.TintColor.IsEmpty &&
        this.Effects is 0 or RenderEffects.None &&
        this.ViewPortSize.IsEmpty;

    /// <summary>
    /// Empties the struct by setting all members to default values.
    /// </summary>
    public void Empty()
    {
        this.TextureId = 0u;
        this.Glyph = '\0';
        this.SrcRect = default;
        this.DestRect = default;
        this.Size = 0f;
        this.Angle = 0f;
        this.TintColor = default;
        this.Effects = RenderEffects.None;
        this.ViewPortSize = default;
    }

    /// <inheritdoc cref="IEquatable{T}.Equals(T?)"/>
    public bool Equals(FontGlyphBatchItem other) =>
        this.SrcRect.Equals(other.SrcRect) &&
        this.DestRect.Equals(other.DestRect) &&
        this.Size.Equals(other.Size) &&
        this.Angle.Equals(other.Angle) &&
        this.TintColor.Equals(other.TintColor) &&
        this.Effects == other.Effects &&
        this.ViewPortSize.Equals(other.ViewPortSize) &&
        this.TextureId == other.TextureId &&
        this.Glyph == other.Glyph;

    /// <inheritdoc cref="object.Equals(object?)"/>
    public override bool Equals(object? obj) => obj is FontGlyphBatchItem other && Equals(other);

    /// <inheritdoc cref="object.GetHashCode"/>
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(
            HashCode.Combine(
                this.SrcRect,
                this.DestRect,
                this.Size,
                this.Angle,
                this.TintColor,
                (int)this.Effects,
                this.ViewPortSize,
                this.TextureId),
            this.Glyph);

    /// <inheritdoc/>
    public override string ToString()
    {
        var result = new StringBuilder();

        result.AppendLine("Font Batch Item Values:");
        result.AppendLine($"Src Rect: {this.SrcRect.ToString()}");
        result.AppendLine($"Dest Rect: {this.DestRect.ToString()}");
        result.AppendLine($"Size: {this.Size.ToString(CultureInfo.InvariantCulture)}");
        result.AppendLine($"Angle: {this.Angle.ToString(CultureInfo.InvariantCulture)}");
        result.AppendLine(
            $"Tint Clr: {{A={this.TintColor.A},R={this.TintColor.R},G={this.TintColor.G},B={this.TintColor.B}}}");
        result.AppendLine($"Effects: {this.Effects.ToString()}");
        result.AppendLine($"View Port Size: {{W={this.ViewPortSize.Width},H={this.ViewPortSize.Height}}}");
        result.AppendLine($"Texture ID: {this.TextureId.ToString()}");
        result.Append($"Glyph: {this.Glyph}");

        return result.ToString();
    }
}
