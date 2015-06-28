namespace Glimpse.Adomd.Tests.AlternateType
{
    using System;
    using System.Data;
    using Adomd.AlternateType;
    using Microsoft.AnalysisServices.AdomdClient;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class GlimpseAdomdCommandTest
    {
        [Test]
        public void Constructor_NullCommand_Throws()
        {
            Assert.That(
                () => new GlimpseAdomdCommand(null, new Mock<IAdomdConnection>().Object, Guid.NewGuid()),
                Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void Constructor_NullConnection_Throws()
        {
            Assert.That(
                () => new GlimpseAdomdCommand(new Mock<IAdomdCommand>().Object, null, Guid.NewGuid()),
                Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void Constructor_ConnectionMatches_And_CommandIdIsSetToDefault()
        {
            var connectionId = Guid.NewGuid();
            var connection = new Mock<IAdomdConnection>().Object;
            var cmd = new GlimpseAdomdCommand(new Mock<IAdomdCommand>().Object, connection, connectionId);

            Assert.That(cmd.ConnectionId, Is.EqualTo(connectionId));
            Assert.That(cmd.Connection, Is.Not.Null);
            Assert.That(cmd.Connection, Is.EqualTo(connection));
            Assert.That(cmd.CommandId, Is.EqualTo(new Guid()));
        }

        [Test]
        public void WrapsCommandText()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            Assert.That(cmd.CommandText, Is.Null);
            mockCommand.VerifyGet(p => p.CommandText, Times.Once);
            cmd.CommandText = "ABC";
            mockCommand.VerifySet(command => command.CommandText = It.Is<string>(s => s == "ABC"), Times.Once);
        }

        [Test]
        public void WrapsCommandTimeout()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            Assert.That(cmd.CommandTimeout, Is.EqualTo(0));
            mockCommand.VerifyGet(p => p.CommandTimeout, Times.Once);
            cmd.CommandTimeout = 30;
            mockCommand.VerifySet(command => command.CommandTimeout = It.Is<int>(i => i == 30), Times.Once);
        }

        [Test]
        public void WrapsCommandType()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            mockCommand.SetupGet(p => p.CommandType).Returns(CommandType.StoredProcedure);
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            Assert.That(cmd.CommandType, Is.EqualTo(CommandType.StoredProcedure));
            mockCommand.VerifyGet(p => p.CommandType, Times.Once);
            cmd.CommandType = CommandType.Text;
            mockCommand.VerifySet(command => command.CommandType = It.Is<CommandType>(i => i == CommandType.Text), Times.Once);
        }

        [Test]
        public void WrapsConnection()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            Assert.That(cmd.Connection, Is.EqualTo(mockConnection.Object));
            Assert.That(
                () => cmd.Connection = new Mock<IDbConnection>().Object,
                Throws.InstanceOf<NotImplementedException>().With.Message.EqualTo("This behavior is not yet implemented."));
        }

        [Test]
        public void WrapsParameters()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            Assert.That(cmd.Parameters, Is.EqualTo(null));
            mockCommand.VerifyGet(p => p.Parameters, Times.Once);
        }

        [Test]
        public void WrapsTransaction()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            Assert.That(cmd.Transaction, Is.EqualTo(null));
            mockCommand.VerifyGet(p => p.Transaction, Times.Once);

            var mockTx = new Mock<IDbTransaction>().Object;
            cmd.Transaction = mockTx;
            mockCommand.VerifySet(command => command.Transaction = It.Is<IDbTransaction>(t => t == mockTx), Times.Once);
        }

        [Test]
        public void WrapsUpdatedRowSource()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            Assert.That(cmd.UpdatedRowSource, Is.EqualTo(UpdateRowSource.None));
            mockCommand.VerifyGet(p => p.UpdatedRowSource, Times.Once);
            cmd.UpdatedRowSource = UpdateRowSource.Both;
            mockCommand.VerifySet(command => command.UpdatedRowSource = It.Is<UpdateRowSource>(t => t == UpdateRowSource.Both), Times.Once);
        }

        [Test]
        public void WrapsCancel()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            cmd.Cancel();

            mockCommand.Verify(p => p.Cancel(), Times.Once);
        }

        [Test]
        public void WrapsCreateParameter()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockParameter = new Mock<IDbDataParameter>().Object;
            mockCommand.Setup(p => p.CreateParameter()).Returns(mockParameter);
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            var parameter = cmd.CreateParameter();
            Assert.That(parameter, Is.EqualTo(mockParameter));

            mockCommand.Verify(p => p.CreateParameter(), Times.Once);
        }

        [Test]
        public void WrapsDispose()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            cmd.Dispose();

            mockCommand.Verify(p => p.Dispose(), Times.Once);
        }

        [Test]
        public void WrapsPrepare()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            cmd.Prepare();

            mockCommand.Verify(p => p.Prepare(), Times.Once);
        }

        [Test]
        public void WrapsExecute()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            var result = cmd.Execute();
            Assert.That(result, Is.EqualTo(null));

            mockCommand.Verify(p => p.Execute(), Times.Once);
        }

        [Test]
        public void WrapsExecuteXmlReader()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            var result = cmd.ExecuteXmlReader();
            Assert.That(result, Is.EqualTo(null));

            mockCommand.Verify(p => p.ExecuteXmlReader(), Times.Once);
        }

        [Test]
        public void WrapsExecuteCellSet()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            var result = cmd.ExecuteCellSet();
            Assert.That(result, Is.EqualTo(null));

            mockCommand.Verify(p => p.ExecuteCellSet(), Times.Once);
        }

        [Test]
        public void WrapsExecuteNonQuery()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            var result = cmd.ExecuteNonQuery();
            Assert.That(result, Is.EqualTo(0));

            mockCommand.Verify(p => p.ExecuteNonQuery(), Times.Once);
        }

        [Test]
        public void WrapsExecuteReader()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            var result = cmd.ExecuteReader();
            Assert.That(result, Is.Null);
            mockCommand.Verify(p => p.ExecuteReader(), Times.Once);

            result = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
            Assert.That(result, Is.Null);
            mockCommand.Verify(p => p.ExecuteReader(It.Is<CommandBehavior>(x => x == CommandBehavior.SchemaOnly)), Times.Once);
        }

        [Test]
        public void WrapsExecuteScalar()
        {
            var mockCommand = new Mock<IAdomdCommand>();
            var mockConnection = new Mock<IAdomdConnection>();
            var cmd = new GlimpseAdomdCommand(mockCommand.Object, mockConnection.Object, Guid.NewGuid());

            var result = cmd.ExecuteScalar();
            Assert.That(result, Is.Null);
            mockCommand.Verify(p => p.ExecuteScalar(), Times.Once);
        }
    }
}
