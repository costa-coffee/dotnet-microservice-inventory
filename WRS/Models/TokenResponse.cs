using System;

namespace WRS
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
