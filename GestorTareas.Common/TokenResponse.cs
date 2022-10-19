using System;

namespace GestorTareas.Common
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}