// <copyright file="TextureShaderResourceLoaderServiceTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using Moq;
using Velaptor.OpenGL.Services;
using Velaptor.Services;
using VelaptorTests.Helpers;
using Xunit;

namespace VelaptorTests.OpenGL.Services;

/// <summary>
/// Tests the <see cref="TextureShaderResourceLoaderService"/> class.
/// </summary>
public class TextureShaderResourceLoaderServiceTests
{
    private const string BatchSizeVarName = "BATCH_SIZE";
    private const string InjectionStart = "${{";
    private const string InjectionStop = "}}";
    private const string ProcessedVertShaderSample = "uniform mat4 uTransform[10];";
    private const string TextureShaderName = "test-source";
    private const string NoProcessingFragShaderSample = "int totalClrs = 4;";
    private readonly string unprocessedFragShaderSample = $"uniform mat4 uTransform[{InjectionStart} {BatchSizeVarName} {InjectionStop}];";
    private readonly Mock<ITemplateProcessorService> mockShaderSrcTemplateService;
    private readonly Mock<IEmbeddedResourceLoaderService<string>> mockResourceLoaderService;
    private readonly Mock<IPath> mockPath;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextureShaderResourceLoaderServiceTests"/> class.
    /// </summary>
    public TextureShaderResourceLoaderServiceTests()
    {
        var fragFileName = $"{TextureShaderName}.frag";
        var vertFileName = $"{TextureShaderName}.vert";

        this.mockPath = new Mock<IPath>();
        this.mockPath.Setup(m => m.HasExtension(fragFileName)).Returns(true);
        this.mockPath.Setup(m => m.GetFileNameWithoutExtension(fragFileName)).Returns(TextureShaderName);

        this.mockPath.Setup(m => m.HasExtension(vertFileName)).Returns(true);
        this.mockPath.Setup(m => m.GetFileNameWithoutExtension(vertFileName)).Returns(TextureShaderName);

        this.mockShaderSrcTemplateService = new Mock<ITemplateProcessorService>();

        this.mockResourceLoaderService = new Mock<IEmbeddedResourceLoaderService<string>>();
        this.mockResourceLoaderService.Setup(m
                => m.LoadResource(fragFileName))
            .Returns(NoProcessingFragShaderSample);
        this.mockResourceLoaderService.Setup(m
                => m.LoadResource(vertFileName))
            .Returns(this.unprocessedFragShaderSample);
    }

    #region Constructor Tests
    [Fact]
    public void Ctor_WithNullShaderSrcTemplateServiceParam_ThrowsException()
    {
        // Act & Assert
        AssertExtensions.ThrowsWithMessage<ArgumentNullException>(() =>
        {
            _ = new TextureShaderResourceLoaderService(
                null,
                this.mockResourceLoaderService.Object,
                this.mockPath.Object);
        }, "The parameter must not be null. (Parameter 'shaderSrcTemplateService')");
    }

    [Fact]
    public void Ctor_WithNullResourceLoaderServiceParam_ThrowsException()
    {
        // Act & Assert
        AssertExtensions.ThrowsWithMessage<ArgumentNullException>(() =>
        {
            _ = new TextureShaderResourceLoaderService(
                this.mockShaderSrcTemplateService.Object,
                null,
                this.mockPath.Object);
        }, "The parameter must not be null. (Parameter 'resourceLoaderService')");
    }

    [Fact]
    public void Ctor_WithNullPathParam_ThrowsException()
    {
        // Act & Assert
        AssertExtensions.ThrowsWithMessage<ArgumentNullException>(() =>
        {
            _ = new TextureShaderResourceLoaderService(
                this.mockShaderSrcTemplateService.Object,
                this.mockResourceLoaderService.Object,
                null);
        }, "The parameter must not be null. (Parameter 'path')");
    }
    #endregion

    #region Method Tests
    [Fact]
    public void LoadVerSource_WhenPropsParamAreNull_ThrowsException()
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act & Assert
        AssertExtensions.ThrowsWithMessage<Exception>(() =>
        {
            sut.LoadVertSource(It.IsAny<string>());
        }, "Missing the property 'BATCH_SIZE' template variable for shader processing.");
    }

    [Fact]
    public void LoadVertSource_WithNoProps_ThrowsException()
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act & Assert
        AssertExtensions.ThrowsWithMessage<Exception>(() =>
        {
            sut.LoadVertSource(It.IsAny<string>(), Array.Empty<(string, uint)>());
        }, "Missing the property 'BATCH_SIZE' template variable for shader processing.");
    }

    [Fact]
    public void LoadVertSource_WhenBatchSizePropDoesNotExist_ThrowsException()
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act & Assert
        AssertExtensions.ThrowsWithMessage<Exception>(() =>
        {
            sut.LoadVertSource(It.IsAny<string>(), Array.Empty<(string, uint)>());
        }, "Missing the property 'BATCH_SIZE' template variable for shader processing.");
    }

    [Fact]
    public void LoadVertSource_WhenInvoked_ReturnsCorrectSourceCode()
    {
        // Arrange
        // Setup the template variable mock for frag source and variables
        this.mockShaderSrcTemplateService
            .Setup(m
                => m.ProcessTemplateVariables(It.IsAny<string>(), It.IsAny<IEnumerable<(string, string)>>()))
            .Returns(ProcessedVertShaderSample);

        var sut = CreateSystemUnderTest();
        var vertFileName = $"{TextureShaderName}.vert";

        // Act
        var actual = sut.LoadVertSource(TextureShaderName, new[] { ("BATCH_SIZE", 10u) });

        // Assert
        this.mockResourceLoaderService.Verify(m => m.LoadResource(vertFileName), Times.Once);
        Assert.Equal(ProcessedVertShaderSample, actual);
    }

    [Fact]
    public void LoadFragSource_WhenInvoked_ReturnsCorrectSourceCode()
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        var actual = sut.LoadFragSource(TextureShaderName);

        // Assert
        Assert.Equal(NoProcessingFragShaderSample, actual);
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="TextureShaderResourceLoaderService"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private TextureShaderResourceLoaderService CreateSystemUnderTest()
        => new (this.mockShaderSrcTemplateService.Object, this.mockResourceLoaderService.Object, this.mockPath.Object);
}
