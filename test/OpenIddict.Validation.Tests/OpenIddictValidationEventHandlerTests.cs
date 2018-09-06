﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Threading.Tasks;
using Xunit;

namespace OpenIddict.Validation.Tests
{
    public class OpenIddictValidationEventHandlerTests
    {
        [Fact]
        public void Constructor_ThrowsAnExceptionForNullHandler()
        {
            // Arrange, act and assert
            var exception = Assert.Throws<ArgumentNullException>(()
                => new OpenIddictValidationEventHandler<Event>(handler: null));

            Assert.Equal("handler", exception.ParamName);
        }

        [Fact]
        public async Task HandleAsync_ThrowsAnExceptionForNullNotification()
        {
            // Arrange
            var handler = new OpenIddictValidationEventHandler<Event>(
                notification => Task.FromResult(OpenIddictValidationEventState.Handled));

            // Act and assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(()
                => handler.HandleAsync(notification: null));

            Assert.Equal("notification", exception.ParamName);
        }

        [Fact]
        public async Task HandleAsync_InvokesInlineHandler()
        {
            // Arrange
            var marker = false;
            var handler = new OpenIddictValidationEventHandler<Event>(
                notification =>
                {
                    marker = true;
                    return Task.FromResult(OpenIddictValidationEventState.Handled);
                });

            // Act
            await handler.HandleAsync(new Event());

            // Assert
            Assert.True(marker);
        }

        public class Event : IOpenIddictValidationEvent { }
    }
}
