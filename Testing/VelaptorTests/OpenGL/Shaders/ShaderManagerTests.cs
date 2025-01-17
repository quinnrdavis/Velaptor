﻿// <copyright file="ShaderManagerTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System;
using Moq;
using Velaptor.Factories;
using Velaptor.OpenGL.Shaders;
using VelaptorTests.Helpers;
using Xunit;

namespace VelaptorTests.OpenGL.Shaders;

/// <summary>
/// Tests the <see cref="ShaderManager"/> class.
/// </summary>
public class ShaderManagerTests
{
    private const string TextureShaderName = "texture-shader";
    private const string FontShaderName = "font-shader";
    private const string RectangleShaderName = "rectangle-shader";
    private const uint TextureShaderId = 123u;
    private const uint FontShaderId = 456u;
    private const uint RectangleShaderId = 789u;
    private readonly Mock<IShaderFactory> mockShaderFactory;
    private readonly Mock<IShaderProgram> mockTextureShader;
    private readonly Mock<IShaderProgram> mockFontShader;
    private readonly Mock<IShaderProgram> mockRectShader;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderManagerTests"/> class.
    /// </summary>
    public ShaderManagerTests()
    {
        this.mockTextureShader = new Mock<IShaderProgram>();
        this.mockTextureShader.SetupGet(p => p.Name).Returns(TextureShaderName);
        this.mockTextureShader.SetupGet(p => p.ShaderId).Returns(TextureShaderId);

        this.mockFontShader = new Mock<IShaderProgram>();
        this.mockFontShader.SetupGet(p => p.Name).Returns(FontShaderName);
        this.mockFontShader.SetupGet(p => p.ShaderId).Returns(FontShaderId);

        this.mockRectShader = new Mock<IShaderProgram>();
        this.mockRectShader.SetupGet(p => p.Name).Returns(RectangleShaderName);
        this.mockRectShader.SetupGet(p => p.ShaderId).Returns(RectangleShaderId);

        this.mockShaderFactory = new Mock<IShaderFactory>();
        this.mockShaderFactory.Setup(m => m.CreateTextureShader()).Returns(this.mockTextureShader.Object);
        this.mockShaderFactory.Setup(m => m.CreateFontShader()).Returns(this.mockFontShader.Object);
        this.mockShaderFactory.Setup(m => m.CreateRectShader()).Returns(this.mockRectShader.Object);
    }

    #region Constructor Tests
    [Fact]
    public void Ctor_WithNullShaderFactoryParam_ThrowsException()
    {
        // Act & Assert
        AssertExtensions.ThrowsWithMessage<ArgumentNullException>(() =>
        {
            var unused = new ShaderManager(null);
        }, "The parameter must not be null. (Parameter 'shaderFactory')");
    }
    #endregion

    #region Method Tests
    [Theory]
    [InlineData(1, TextureShaderId)]
    [InlineData(2, FontShaderId)]
    [InlineData(3, RectangleShaderId)]
    public void GetShaderId_WhenInvoked_ReturnsCorrectValue(int shaderTypeValue, uint expected)
    {
        // Arrange
        var shaderType = (ShaderType)shaderTypeValue;
        var sut = CreateSystemUnderTest();

        // Act
        var actual = sut.GetShaderId(shaderType);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1, TextureShaderName)]
    [InlineData(2, FontShaderName)]
    [InlineData(3, RectangleShaderName)]
    public void GetShaderName_WhenInvoked_ReturnsCorrectValue(int shaderTypeValue, string expected)
    {
        // Arrange
        var shaderType = (ShaderType)shaderTypeValue;
        var sut = CreateSystemUnderTest();

        // Act
        var actual = sut.GetShaderName(shaderType);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetShaderName_WithInvalidShaderType_ThrowsException()
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act & Assert
        AssertExtensions.ThrowsWithMessage<ArgumentOutOfRangeException>(() =>
        {
            sut.GetShaderName((ShaderType)1234);
        }, $"The enum '{nameof(ShaderType)}' value is invalid. (Parameter 'shaderType'){Environment.NewLine}Actual value was 1234.");
    }

    [Fact]
    public void Use_WithTextureShaderType_UsesShader()
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        sut.Use(ShaderType.Texture);

        // Assert
        this.mockTextureShader.VerifyOnce(m => m.Use());
        this.mockFontShader.VerifyNever(m => m.Use());
        this.mockRectShader.VerifyNever(m => m.Use());
    }

    [Fact]
    public void Use_WithFontShaderType_UsesShader()
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        sut.Use(ShaderType.Font);

        // Assert
        this.mockTextureShader.VerifyNever(m => m.Use());
        this.mockFontShader.VerifyOnce(m => m.Use());
        this.mockRectShader.VerifyNever(m => m.Use());
    }

    [Fact]
    public void Use_WithRectangleShaderType_UsesShader()
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        sut.Use(ShaderType.Rectangle);

        // Assert
        this.mockTextureShader.VerifyNever(m => m.Use());
        this.mockFontShader.VerifyNever(m => m.Use());
        this.mockRectShader.VerifyOnce(m => m.Use());
    }

    [Fact]
    public void Use_WithInvalidShaderType_ThrowsException()
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act & Assert
        AssertExtensions.ThrowsWithMessage<ArgumentOutOfRangeException>(() =>
        {
            sut.Use((ShaderType)1234);
        }, $"The enum '{nameof(ShaderType)}' value is invalid. (Parameter 'shaderType'){Environment.NewLine}Actual value was 1234.");
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="ShaderManager"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private ShaderManager CreateSystemUnderTest() => new (this.mockShaderFactory.Object);
}
