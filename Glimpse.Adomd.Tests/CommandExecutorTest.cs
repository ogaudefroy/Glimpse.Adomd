namespace Glimpse.Adomd.Tests
{
    using System;
    using Adomd.AlternateType;
    using Messages;
    using Microsoft.AnalysisServices.AdomdClient;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class CommandExecutorTest
    {
        [Test]
        public void CommandExecutor_NullCommand_Throws()
        {
            Assert.That(
                () => new CommandExecutor(null), 
                Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void CommandExecutor_NullTimerStrategy_Throws()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            Assert.That(
                () => new CommandExecutor(new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid()), null), 
                Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void CommandExecutor_ExecuteNormalCallEmitsStartAndStopMessages()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var guidConnection = Guid.NewGuid();
            var glimpseCommand = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, guidConnection);
            var mockTimedMessagePublisher = new Mock<ITimedMessagePublisher>();
            var commandExecutor = new CommandExecutor(glimpseCommand, mockTimedMessagePublisher.Object);

            var result = commandExecutor.Execute<int>(() => 13, "Execute");

            Assert.That(result, Is.EqualTo(13));
            Assert.That(glimpseCommand.CommandId, Is.Not.Null);
            mockTimedMessagePublisher.Verify(p => p.EmitStartMessage(
                It.IsAny<CommandExecutedMessage>()), Times.Once);
            mockTimedMessagePublisher.Verify(p => p.EmitStopMessage(
                It.IsAny<CommandDurationAndRowCountMessage>()), Times.Once);
        }
    }
}
