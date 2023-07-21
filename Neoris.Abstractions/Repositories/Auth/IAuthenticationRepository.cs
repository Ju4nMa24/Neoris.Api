namespace Neoris.Abstractions.Repositories.Auth
{
    /// <summary>
    /// Authentication repository (contains the definition of the actions to perform).
    /// </summary>
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// JWT Create
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        Task<string> Generate(string tokenId);
    }
}
