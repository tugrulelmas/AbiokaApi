namespace AbiokaApi.Infrastructure.Common.Authentication
{
    public interface IAbiokaToken
    {
        /// <summary>
        /// Encodes the specified user claim.
        /// </summary>
        /// <param name="userClaim">The user claim.</param>
        /// <returns></returns>
        string Encode(UserClaim userClaim);

        /// <summary>
        /// Encodes the specified user claim.
        /// </summary>
        /// <param name="userClaim">The user claim.</param>
        /// <param name="expirationMinutes">The expiration minutes.</param>
        /// <returns></returns>
        string Encode(UserClaim userClaim, double expirationMinutes);

        /// <summary>
        /// Decodes the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        TokenPayload Decode(string token);
    }
}
