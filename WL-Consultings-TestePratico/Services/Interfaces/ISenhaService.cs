namespace WL_Consultings_TestePratico.Services.Interfaces
{
    public interface ISenhaService
    {
        string GerarHashSenha(string senha);

        bool VerificarSenha(string senha, string hashSenha);

    }
}
