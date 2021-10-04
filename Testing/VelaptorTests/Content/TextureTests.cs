﻿// <copyright file="TextureTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace VelaptorTests.Content
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Moq;
    using Velaptor.Content;
    using Velaptor.Content.Exceptions;
    using Velaptor.Graphics;
    using Velaptor.NativeInterop.OpenGL;
    using Velaptor.OpenGL;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Texture"/> class.
    /// </summary>
    public class TextureTests
    {
        private const string TextureName = "test-texture";
        private const string TexturePath = @"C:\temp\test-texture.png";
        private const uint TextureId = 1234;
        private readonly Mock<IGLInvoker> mockGL;
        private readonly ImageData imageData;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureTests"/> class.
        /// </summary>
        public TextureTests()
        {
            this.imageData = new ImageData(new Color[2, 3], 2, 3);

            /*NOTE:
             * Create the bytes in the ARGB byte layout.
             * OpenGL expects the layout to be RGBA.  The texture class changes this
             * this layout to meet OpenGL's requirements.
             */
            for (var y = 0; y < this.imageData.Height; y++)
            {
                for (var x = 0; x < this.imageData.Width; x++)
                {
                    this.imageData.Pixels[x, y] = y switch
                    {
                        0 => this.imageData.Pixels[x, y] = Color.FromArgb(255, 255, 0, 0), // Row 1
                        1 =>  this.imageData.Pixels[x, y] = Color.FromArgb(255, 0, 255, 0), // Row 2
                        2 => this.imageData.Pixels[x, y] = Color.FromArgb(255, 0, 0, 255), // Row 3
                        _ => throw new Exception($"Row '{y}' does not exist when setting up image data for test."),
                    };

                    // If the first row
                    switch (y)
                    {
                        case 0: // Row 1
                            this.imageData.Pixels[x, y] = Color.FromArgb(255, 255, 0, 0);
                            break;
                        case 1: // Row 2
                            this.imageData.Pixels[x, y] = Color.FromArgb(255, 0, 255, 0);
                            break;
                        case 2: // Row 3
                            this.imageData.Pixels[x, y] = Color.FromArgb(255, 0, 0, 255);
                            break;
                    }
                }
            }

            this.mockGL = new Mock<IGLInvoker>();
            this.mockGL.Setup(m => m.GenTexture()).Returns(TextureId);
        }

        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_UploadsTextureDataToGPU()
        {
            // Arrange
            var expectedPixelData = new List<byte>();

            // NOTE: Swap the from ARGB to RGBA byte layout because this is expected by OpenGL
            for (var y = 0; y < this.imageData.Height; y++)
            {
                var rowBytes = new List<byte>();

                for (var x = 0; x < this.imageData.Width; x++)
                {
                    rowBytes.Add(this.imageData.Pixels[x, y].R);
                    rowBytes.Add(this.imageData.Pixels[x, y].G);
                    rowBytes.Add(this.imageData.Pixels[x, y].B);
                    rowBytes.Add(this.imageData.Pixels[x, y].A);
                }

                expectedPixelData.AddRange(rowBytes);
                rowBytes.Clear();
            }

            var expectedPixelBytes = new ReadOnlySpan<byte>(expectedPixelData.ToArray());

            // Act
            var texture = new Texture(this.mockGL.Object, "test-texture.png", $@"C:\temp\test-texture.png", this.imageData);

            // Assert
            this.mockGL.Verify(m => m.ObjectLabel(GLObjectIdentifier.Texture, TextureId, 1u, "test-texture.png"), Times.Once());
            this.mockGL.Verify(m => m.TexParameter(
                GLTextureTarget.Texture2D,
                GLTextureParameterName.TextureMinFilter,
                GLTextureMinFilter.Linear), Times.Once());

            this.mockGL.Verify(m => m.TexParameter(
                GLTextureTarget.Texture2D,
                GLTextureParameterName.TextureMagFilter,
                GLTextureMagFilter.Linear), Times.Once());

            this.mockGL.Verify(m => m.TexParameter(
                GLTextureTarget.Texture2D,
                GLTextureParameterName.TextureWrapS,
                GLTextureWrapMode.ClampToEdge), Times.Once());

            this.mockGL.Verify(m => m.TexParameter(
                GLTextureTarget.Texture2D,
                GLTextureParameterName.TextureWrapT,
                GLTextureWrapMode.ClampToEdge), Times.Once());

            var expectedPixelArray = expectedPixelData.ToArray();

            this.mockGL.Verify(m => m.TexImage2D<byte>(
                GLTextureTarget.Texture2D,
                0,
                GLInternalFormat.Rgba,
                2u,
                3u,
                0,
                GLPixelFormat.Rgba,
                GLPixelType.UnsignedByte,
                expectedPixelArray), Times.Once());
        }
        #endregion

        #region Prop Tests
        [Fact]
        public void Id_WhenCreatingTexture_ReturnsCorrectResult()
        {
            // Arrange
            var texture = CreateTexture();

            // Act
            var actual = texture.Id;

            // Assert
            Assert.Equal(TextureId, actual);
        }

        [Fact]
        public void Name_WhenCreatingTexture_ReturnsCorrectResult()
        {
            // Arrange
            var texture = CreateTexture();

            // Act
            var actual = texture.Name;

            // Assert
            Assert.Equal(TextureName, actual);
        }

        [Fact]
        public void Path_WhenCreatingTexture_ReturnsCorrectResult()
        {
            // Arrange
            var texture = CreateTexture();

            // Act
            var actual = texture.Path;

            // Assert
            Assert.Equal(TexturePath, actual);
        }

        [Fact]
        public void Width_WhenCreatingTexture_ReturnsCorrectResult()
        {
            // Arrange
            var texture = CreateTexture();

            // Act
            var actual = texture.Width;

            // Assert
            Assert.Equal(2, actual);
        }

        [Fact]
        public void Height_WhenCreatingTexture_ReturnsCorrectResult()
        {
            // Arrange
            var texture = CreateTexture();

            // Act
            var actual = texture.Height;

            // Assert
            Assert.Equal(3, actual);
        }
        #endregion

        #region Method Tests
        [Fact]
        public void Dispose_WhenUnmanagedResourcesIsNotDisposed_DisposesOfUnmanagedResources()
        {
            // Arrange
            var texture = CreateTexture();

            // Act
            texture.Dispose();
            texture.Dispose();

            // Assert
            this.mockGL.Verify(m => m.DeleteTexture(TextureId), Times.Once());
        }

        [Fact]
        public void Dispose_WhilePooled_ThrowsException()
        {
            // Arrange
            var texture = CreateTexture();
            texture.IsPooled = true;

            // Act & Assert
            Assert.Throws<PooledDisposalException>(() =>
            {
                texture.Dispose();
            });
        }
        #endregion

        /// <summary>
        /// Creates a texture for the purpose of testing.
        /// </summary>
        /// <returns>The texture instance to test.</returns>
        private Texture CreateTexture() => new (this.mockGL.Object, TextureName, TexturePath, this.imageData);
    }
}