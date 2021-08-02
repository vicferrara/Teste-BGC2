using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace TesteSolid.Classes
{
    public class ClienteCadastrar : IClienteCadastroServico
    {

        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteCadastrar(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public void Cadastrar()
        {

            Cliente cliente = DadosCliente();

            if (!cliente.email.Contains('@'))
            {
                RetornarMensagem("CPF inválido");
                return;
            }
                
            if (cliente.cpf.Replace(".", "").Replace("-", "").Length != 11)
            {
                RetornarMensagem("CPF inválido");
                return;
            }

            Boolean retorno = _clienteRepositorio.Insert(cliente);

            if (retorno)
                ConcluirCadastroCliente(cliente.email);
            else
                RetornarMensagem("Ocorreu um erro ao cadastrar o cliente.");

        }

        private static Cliente DadosCliente()
        {
            Console.WriteLine();

            Console.WriteLine("Informe o nome do Cliente");
            string nome = Console.ReadLine();

            Console.WriteLine("Informe o e-mail do Cliente");
            string email = Console.ReadLine();

            Console.WriteLine("Informe o cpf do Cliente");
            string cpf = Console.ReadLine();

            Cliente cliente = new Cliente()
            {
                nome = nome,
                email = email,
                cpf = cpf,
                dataCriacao = DateTime.Now
            };

            return cliente;
        }

        private static void ConcluirCadastroCliente(string email)
        {
            MailMessage mail = new MailMessage("empresa@empresa.com", email);

            SmtpClient client = new SmtpClient()
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smpt.google.com"
            };

            mail.Subject = "Bem Vindo";
            mail.Body = "Parabéns! Você está cadastrado";
            client.Send(mail);

            Console.WriteLine("O cliente foi cadastrado com sucesso");
        }

        private static void RetornarMensagem(string mensagem)
        {
            Console.WriteLine(mensagem);
        }

    }
}
