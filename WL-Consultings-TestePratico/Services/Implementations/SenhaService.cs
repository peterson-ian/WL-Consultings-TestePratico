using WL_Consultings_TestePratico.Services.Interfaces;

namespace WL_Consultings_TestePratico.Services.Implementations
{
    public class SenhaService : ISenhaService
    {
        private const int WorkFactor = 12;

        public string GerarHashSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha, WorkFactor);
        }

        public bool VerificarSenha(string senha, string hashSenha)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(senha, hashSenha);
            }
            catch
            {
                return false;
            }
        }

    }
}
