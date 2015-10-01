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
            mockCommand.Setup(p => p.Execute()).Returns(13);
            mockCommand.SetupAllProperties();
            var command = mockCommand.Object;
            var mockConnection = new Mock<IAdomdConnection>();
            var guidConnection = Guid.NewGuid();
            var glimpseCommand = new GlimpseAdomdCommand(command, mockConnection.Object, guidConnection)
            {
                CommandText = "ABC"
            };
            var mockTimedMessagePublisher = new Mock<ITimedMessagePublisher>();
            var commandExecutor = new CommandExecutor(glimpseCommand, mockTimedMessagePublisher.Object);

            var result = commandExecutor.Execute(c => c.Execute(), "Execute");

            Assert.That(result, Is.EqualTo(13));
            Assert.That(glimpseCommand.CommandId, Is.Not.Null);
            mockTimedMessagePublisher.Verify(p => p.EmitStartMessage(
                It.Is<CommandExecutedMessage>(c =>
                    c.CommandId == glimpseCommand.CommandId
                    && c.ConnectionId == guidConnection
                    && c.CommandText == "ABC"
                    && c.HasTransaction == false
                    && c.IsAsync == false
                )), Times.Once);
            mockTimedMessagePublisher.Verify(p => p.EmitStopMessage(
                It.Is<CommandDurationMessage>(c =>
                    c.CommandId == glimpseCommand.CommandId
                    && c.ConnectionId == guidConnection
                    && c.EventCategory == AdomdTimelineCategory.Command
                    && c.EventName == "Command: Executed"
                    && c.EventSubText == "Execute"
                    && c.IsAsync == false)), Times.Once);
        }

        [Test]
        public void CommandExecutor_ExecuteWithErrorsEmitsStartsAndStopMessages()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            mockCommand.Setup(p => p.Execute()).Throws<NotSupportedException>();
            mockCommand.SetupAllProperties();
            var command = mockCommand.Object;
            var mockConnection = new Mock<IAdomdConnection>();
            var guidConnection = Guid.NewGuid();
            var glimpseCommand = new GlimpseAdomdCommand(command, mockConnection.Object, guidConnection)
                                    {
                                        CommandText = "ABC"
                                    };
            var mockTimedMessagePublisher = new Mock<ITimedMessagePublisher>();
            var commandExecutor = new CommandExecutor(glimpseCommand, mockTimedMessagePublisher.Object);

            Assert.That(() => commandExecutor.Execute(c => c.Execute(), "Execute"), Throws.InstanceOf<NotSupportedException>());

            Assert.That(glimpseCommand.CommandId, Is.Not.Null);
            mockTimedMessagePublisher.Verify(p => p.EmitStartMessage(
                It.Is<CommandExecutedMessage>(c =>
                    c.CommandId == glimpseCommand.CommandId
                    && c.ConnectionId == guidConnection
                    && c.CommandText == "ABC"
                    && c.HasTransaction == false
                    && c.IsAsync == false
                )), Times.Once);
            mockTimedMessagePublisher.Verify(p => p.EmitStopMessage(
                It.Is<CommandErrorMessage>(c =>
                    c.CommandId == glimpseCommand.CommandId
                    && c.ConnectionId == guidConnection
                    && c.Exception != null && c.Exception.GetType() == typeof(NotSupportedException)
                    && c.EventCategory == AdomdTimelineCategory.Command
                    && c.EventName == "Command: Error"
                    && c.EventSubText == "Execute"
                    && c.IsAsync == false)), Times.Once);
        }
    }
}
