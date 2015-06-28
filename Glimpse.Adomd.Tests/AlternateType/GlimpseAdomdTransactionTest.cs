namespace Glimpse.Adomd.Tests.AlternateType
{
    using System;
    using System.Data;
    using Adomd.AlternateType;
    using Messages;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class GlimpseAdomdTransactionTest
    {
        [Test]
        public void Constructor_NullTransaction_Throws()
        {
            Assert.That(
                () => new GlimpseAdomdTransaction(null, new Mock<IDbConnection>().Object, Guid.NewGuid()),
                Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void Constructor_NullConnecction_Throws()
        {
            Assert.That(
                () => new GlimpseAdomdTransaction(new Mock<IDbTransaction>().Object, null, Guid.NewGuid()),
                Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void Constructor_SetsValuesAndEmitsStartMessage()
        {
            var mockTx = new Mock<IDbTransaction>();
            mockTx.SetupGet(p => p.IsolationLevel).Returns(IsolationLevel.Chaos);
            var mockConn = new Mock<IDbConnection>();
            var mockPublisher = new Mock<ITimedMessagePublisher>();
            var connGuid = Guid.NewGuid();

            var tx = new GlimpseAdomdTransaction(mockTx.Object, mockConn.Object, connGuid, mockPublisher.Object);
            Assert.That(tx.Connection, Is.EqualTo(mockConn.Object));
            Assert.That(tx.TransactionId, Is.Not.Null);
            Assert.That(tx.IsolationLevel, Is.EqualTo(IsolationLevel.Chaos));
            mockTx.Verify(p => p.IsolationLevel, Times.AtLeastOnce);
            mockPublisher.Verify(
                p => p.EmitStartMessage(
                    It.Is<TransactionBeganMessage>(s => s.ConnectionId == connGuid && s.TransactionId == tx.TransactionId)),
                    Times.Once);
        }

        [Test]
        public void WrapsCommit()
        {
            var mockTx = new Mock<IDbTransaction>();
            var mockConn = new Mock<IDbConnection>();
            var mockPublisher = new Mock<ITimedMessagePublisher>();
            var connGuid = Guid.NewGuid();

            var tx = new GlimpseAdomdTransaction(mockTx.Object, mockConn.Object, connGuid, mockPublisher.Object);

            tx.Commit();
            mockTx.Verify(p => p.Commit(), Times.Once);
            mockPublisher.Verify(
                p => p.EmitStopMessage(
                    It.Is<TransactionCommitMessage>(s => s.ConnectionId == connGuid && s.TransactionId == tx.TransactionId)),
                    Times.Once);
        }

        [Test]
        public void WrapsRollback()
        {
            var mockTx = new Mock<IDbTransaction>();
            var mockConn = new Mock<IDbConnection>();
            var mockPublisher = new Mock<ITimedMessagePublisher>();
            var connGuid = Guid.NewGuid();

            var tx = new GlimpseAdomdTransaction(mockTx.Object, mockConn.Object, connGuid, mockPublisher.Object);

            tx.Rollback();
            mockTx.Verify(p => p.Rollback(), Times.Once);
            mockPublisher.Verify(
                p => p.EmitStopMessage(
                    It.Is<TransactionRollbackMessage>(s => s.ConnectionId == connGuid && s.TransactionId == tx.TransactionId)),
                    Times.Once);
        }

        [Test]
        public void WrapsDisposeOnCommitedTransactionDoesNotPublishRollback()
        {
            var mockTx = new Mock<IDbTransaction>();
            mockTx.SetupGet(p => p.IsolationLevel).Returns(IsolationLevel.Chaos);
            var mockConn = new Mock<IDbConnection>();
            var mockPublisher = new Mock<ITimedMessagePublisher>();
            var connGuid = Guid.NewGuid();

            var tx = new GlimpseAdomdTransaction(mockTx.Object, mockConn.Object, connGuid, mockPublisher.Object);
            tx.Commit();
            tx.Dispose();

            mockTx.Verify(p => p.Dispose(), Times.Once);
            mockPublisher.Verify(
             p => p.EmitStopMessage(
                 It.Is<TransactionRollbackMessage>(s => s.ConnectionId == connGuid && s.TransactionId == tx.TransactionId)),
                 Times.Never);
        }

        [Test]
        public void WrapsDisposeOnNonCommitedTransactionPublishesRollback()
        {
            var mockTx = new Mock<IDbTransaction>();
            mockTx.SetupGet(p => p.IsolationLevel).Returns(IsolationLevel.Chaos);
            var mockConn = new Mock<IDbConnection>();
            var mockPublisher = new Mock<ITimedMessagePublisher>();
            var connGuid = Guid.NewGuid();

            var tx = new GlimpseAdomdTransaction(mockTx.Object, mockConn.Object, connGuid, mockPublisher.Object);
            tx.Dispose();

            mockTx.Verify(p => p.Dispose(), Times.Once);
            mockPublisher.Verify(
             p => p.EmitStopMessage(
                 It.Is<TransactionRollbackMessage>(s => s.ConnectionId == connGuid && s.TransactionId == tx.TransactionId)),
                 Times.Once);
        }
    }
}
