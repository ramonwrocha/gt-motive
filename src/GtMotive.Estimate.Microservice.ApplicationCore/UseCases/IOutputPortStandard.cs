namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases
{
    /// <summary>
    /// Interface to define the Standard Output Port.
    /// </summary>
    /// <typeparam name="TUseCaseOutput">Tyoe of the use case response dto.</typeparam>
    public interface IOutputPortStandard<in TUseCaseOutput>
        where TUseCaseOutput : IUseCaseOutput
    {
        /// <summary>
        /// Writes to the Standard Output.
        /// </summary>
        /// <param name="response">The Output Port Message.</param>
        void StandardHandle(TUseCaseOutput response);

        /// <summary>
        /// Handles validation errors by providing an error message.
        /// </summary>
        /// <param name="message">The validation error message.</param>
        void ValidationError(string message);
    }
}
