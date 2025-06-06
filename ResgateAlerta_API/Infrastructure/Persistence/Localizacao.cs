namespace EcoDenuncia.Infrastructure.Persistence
{
    public class Localizacao
    {

        public Guid IdLocalização { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cep { get; private set; }

        // relacionamento com Bairro
        public Guid IdBairro { get; private set; }
        public Bairro Bairro { get; set; }

        // relacionamento com Denuncia
        public ICollection<Denuncia> Denuncias { get; private set; } = new List<Denuncia>();

        public Localizacao(string logradouro, string numero, string complemento, string cep, Guid idBairro)
        {
            ValidarLogradouro(logradouro);
            ValidarNumero(numero);
            ValidarCep(cep);

            IdLocalização = Guid.NewGuid();
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;

            IdBairro = idBairro;
        }

        public void SetLogradouro(string logradouro)
        {
            ValidarLogradouro(logradouro);
            Logradouro = logradouro;
        }

        public void SetNumero(string numero)
        {
            ValidarNumero(numero);
            Numero = numero;
        }

        public void SetComplemento(string complemento)
        {
            Complemento = complemento;
        }

        public void SetCep(string cep)
        {
            ValidarCep(cep);
            Cep = cep;
        }

        public void SetIdBairro(Guid idBairro)
        {
            IdBairro = idBairro;
        }


        private void ValidarLogradouro(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
                throw new Exception("Logradouro não pode ser vazio.");
            if (logradouro.Length > 200)
                throw new Exception("Logradouro deve ter no máximo 200 caracteres.");
        }

        private void ValidarNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new Exception("Número não pode ser vazio.");
            if (numero.Length > 10)
                throw new Exception("Número deve ter no máximo 10 caracteres.");
        }

        private void ValidarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new Exception("CEP não pode ser vazio.");
            if (cep.Length != 8)
                throw new Exception("CEP deve ter exatamente 8 caracteres.");
        }
        internal static Localizacao Create(string logradouro, string numero, string complemento, string cep, Guid idBairro)
        {
            return new Localizacao(logradouro, numero, complemento, cep, idBairro);

        }
    }
}