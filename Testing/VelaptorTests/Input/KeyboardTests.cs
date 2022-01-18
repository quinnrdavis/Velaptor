﻿// <copyright file="KeyboardTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace VelaptorTests.Input
{
    using System;
    using System.Linq;
    using Velaptor.Input;
    using VelaptorTests.Helpers;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Keyboard"/> class.
    /// </summary>
    public class KeyboardTests : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardTests"/> class.
        /// </summary>
        public KeyboardTests() => IKeyboardInput<KeyCode, KeyboardState>.InputStates.Clear();

        #region Method Tests
        [Fact]
        public void GetState_WhenInvokedWithNoKeyStates_SetsUpKeyStates()
        {
            // Act
            var keyboard = new Keyboard();
            keyboard.GetState();

            // Assert
            Assert.Equal(119, IKeyboardInput<KeyCode, KeyboardState>.InputStates.Count);
        }

        [Fact]
        public void GetState_WhenInvoked_ReturnsCorrectResult()
        {
            // Arrange
            var keyboard = new Keyboard();
            keyboard.SetState(KeyCode.T, true);

            // Act
            var actual = keyboard.GetState();

            // Assert
            AssertExtensions.AllItemsAre(actual.GetKeyStates(), state =>
            {
                if (state.Key == KeyCode.T)
                {
                    return state.Value;
                }
                else
                {
                    return true;
                }
            });
        }

        [Fact]
        public void SetState_WhenInvoked_InitializesKeyStates()
        {
            // Arrange
            var keyboard = new Keyboard();

            // Act & Assert
            Assert.All(Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().ToArray(), (keyCode) =>
            {
                keyboard.SetState(keyCode, true);
                Assert.True(IKeyboardInput<KeyCode, KeyboardState>.InputStates[keyCode]);
            });
        }

        [Fact]
        public void SetState_WhenInvoked_SetsProperKey()
        {
            // Act
            var keyboard = new Keyboard();
            keyboard.SetState(KeyCode.F, true);
            var actual = keyboard.GetState();

            // Assert
            AssertExtensions.AllItemsAre(actual.GetKeyStates(), state =>
                state.Key != KeyCode.F || state.Value);
        }

        [Fact]
        public void Reset_WhenInvoked_InitializesDefaultStates()
        {
            // Arrange
            var keyboard = new Keyboard();

            // Act
            keyboard.Reset();

            // Assert
            Assert.Equal(Enum.GetValues(typeof(KeyCode)).Length, IKeyboardInput<KeyCode, KeyboardState>.InputStates.Count);
            Assert.All(IKeyboardInput<KeyCode, KeyboardState>.InputStates, state =>
            {
                var (_, value) = state;
                Assert.False(value);
            });
        }
        #endregion

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose() => IKeyboardInput<KeyCode, KeyboardState>.InputStates.Clear();
    }
}
