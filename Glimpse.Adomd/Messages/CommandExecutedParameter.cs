namespace Glimpse.Adomd.Messages
{
    /// <summary>
    /// Represents a command parameter.
    /// </summary>
    public class CommandExecutedParameter
    {
        /// <summary>
        /// Gets or sets the parameter name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parameter value.
        /// </summary>
        public object Value { get; set; }
    }
}