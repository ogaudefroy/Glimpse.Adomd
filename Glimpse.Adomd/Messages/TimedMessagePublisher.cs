namespace Glimpse.Adomd.Messages
{
    using System;
    using Core.Message;
    using Core.Extensibility;
    using Core.Framework;

    /// <summary>
    /// ITimedMessagePublisher's implementation.
    /// </summary>
    public class TimedMessagePublisher : ITimedMessagePublisher
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IExecutionTimer _executionTimer;
        private TimeSpan _timerTimeSpan;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedMessagePublisher"/> class.
        /// </summary>
        public TimedMessagePublisher()
            : this(GlimpseConfiguration.GetConfiguredMessageBroker(), GlimpseConfiguration.GetConfiguredTimerStrategy()())
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedMessagePublisher"/> class.
        /// </summary>
        /// <param name="messageBroker">The message broker.</param>
        /// <param name="executionTimer">The execution timer.</param>
        public TimedMessagePublisher(IMessageBroker messageBroker, IExecutionTimer executionTimer)
        {
            _messageBroker = messageBroker;
            _executionTimer = executionTimer;
        }

        /// <inheritdoc />
        public void EmitStartMessage(AdomdMessage message)
        {
            if (_messageBroker != null && _executionTimer != null)
            {
                _timerTimeSpan = _executionTimer.Start();
                _messageBroker.Publish(message.AsTimedMessage(_timerTimeSpan));
            }
        }

        /// <inheritdoc />
        public void EmitStopMessage(AdomdMessage message)
        {
            if (_messageBroker != null && _executionTimer != null && _timerTimeSpan != TimeSpan.Zero)
            {
                _messageBroker.Publish(message.AsTimedMessage(_executionTimer.Stop(_timerTimeSpan)));
                _timerTimeSpan = TimeSpan.Zero;
            }
        }
    }
}
