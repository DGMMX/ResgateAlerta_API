using System;
using ResgateAlerta.Infrastructure.Persistence;

namespace ResgateAlerta.Infrastructure.Persistence
{
    public class Localizacao
    {
        public Guid IdLocalizacao { get; private set; }

        // relacionamento com bairro
        public Guid IdBairro { get; private set; }
        public Bairro Bairro { get; set; }

        // relacionamento com cidade
        public Guid IdCidade { get; private set; }
        public Cidade Cidade { get; set; }

        // relacionamento com estado
        public Guid IdEstado { get; private set; }
        public Estado Estado { get; set; }

        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }

        public Localizacao(Guid idBairro, Guid idCidade, Guid idEstado, string logradouro, string numero, string complemento)
        {
            ValidarLogradouro(logradouro);
            ValidarNumero(numero);
            ValidarComplemento(complemento);
            IdLocalizacao = Guid.NewGuid();
            IdBairro = idBairro;
            IdCidade = idCidade;
            IdEstado = idEstado;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
        }

        private void ValidarLogradouro(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
            {
                throw new Exception("Logradouro não pode ser vazio.");
            }
            if (logradouro.Length > 100)
            {
                throw new Exception("Logradouro deve ter no máximo 100 caracteres.");
            }
        }

        private void ValidarNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
            {
                throw new Exception("Número não pode ser vazio.");
            }
            if (numero.Length > 10)
            {
                throw new Exception("Número deve ter no máximo 10 caracteres.");
            }
        }

        private void ValidarComplemento(string complemento)
        {
            if (complemento != null && complemento.Length > 50)
            {
                throw new Exception("Complemento deve ter no máximo 50 caracteres.");
            }
        }

        public void AtualizaLocalizacao(Guid idBairro, Guid idCidade, Guid idEstado, string logradouro, string numero, string complemento)
        {
            IdBairro = idBairro;
            IdCidade = idCidade;
            IdEstado = idEstado;
            ValidarLogradouro(logradouro);
            ValidarNumero(numero);
            ValidarComplemento(complemento);
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
        }

        internal static Localizacao Create(Guid idBairro, Guid idCidade, Guid idEstado, string logradouro, string numero, string complemento)
        {
            return new Localizacao(idBairro, idCidade, idEstado, logradouro, numero, complemento);
        }
    }
}