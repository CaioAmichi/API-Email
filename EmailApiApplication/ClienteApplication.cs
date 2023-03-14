
using EmailApi.Domain.Applications;
using EmailApi.Domain.Consts;
using EmailApi.Domain.Entities;
using EmailApi.Domain.IRepository;
using EmailApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailApi.Application
{
    public class ClienteApplication : IClienteApplication
    {
        private readonly IClienteRepositorio _clienteRepository;

        public ClienteApplication(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepository = clienteRepositorio;
        }


        //convert string para base64
        public string EncodeToBase64(string texto)
        {


            byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
            string resultado = System.Convert.ToBase64String(textoAsBytes);
            return resultado;


        }

        public static bool ValidaEmail(string email)
        {
            if (new EmailAddressAttribute().IsValid(email) && !string.IsNullOrEmpty(email))
            {
                return true;
            }
            return false;
        }

        private async Task<Cliente> validateNome(string nome, List<Cliente> clientes)
        {
            foreach (var cliente in clientes)
            {
                byte[] tempBytes;
                tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(cliente.NomeEmpresa);
                string nomePadrao = System.Text.Encoding.UTF8.GetString(tempBytes);

                if (nomePadrao.ToLower() == nome.ToLower())
                {
                    return cliente;
                }
            }

            return null;
        }

        public static bool ValidaString(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public async Task<Result> CriarCliente(Cliente cliente)
        {
            Result response = Result.Ok();
            try
            {
                if (ValidaString(cliente.NomeEmpresa))
                {
                    return response = Result<Cliente>.Error(Errors.NomeEmpresaInvalido);

                }

                if (!ValidaEmail(cliente.EmailRemetente))
                {
                    return response = Result<Cliente>.Error(Errors.EmailRemetenteInvalido);
                }

                if (ValidaString(cliente.SenhaRemetente))
                {
                    return response = Result<Cliente>.Error(Errors.SenhaInvalida);
                }

                if (ValidaString(cliente.Smtp))
                {
                    return response = Result<Cliente>.Error(Errors.SmtpInvalido);
                }

                cliente.SenhaRemetente = EncodeToBase64(cliente.SenhaRemetente);
                cliente.NomeEmpresa = cliente.NomeEmpresa.Trim();
                cliente.Id = Guid.NewGuid();
                _clienteRepository.Adicionar(cliente);


            }
            catch (Exception ex)
            {
                response = Result.Error(Errors.GlobalErroDeRepositorio.Key, ex.Message);
            }

            return response;
        }

        public async Task<Result<Cliente>> ObterClienteNome(string nome)
        {
            Result<Cliente> response = Result<Cliente>.Ok(null);
            try
            {
                var clientes = _clienteRepository.ObterTodos();
                var cliente = await validateNome(nome, clientes);

                if(cliente == null)
                {
                    return response = Result<Cliente>.Error(Errors.SmtpInvalido);
                }
                
                response = Result<Cliente>.Ok(cliente);
            }
            catch (Exception ex)
            {
                response = Result<Cliente>.Error(Errors.GlobalErroDeRepositorio.Key, ex.Message);
            }

            return response;
        }

        public async Task<Result<List<Cliente>>> ListarClientes()
        {
            Result<List<Cliente>> response = Result<List<Cliente>>.Ok(null);
            try
            {
                var clientes = _clienteRepository.ObterTodos();

                response = Result<List<Cliente>>.Ok(clientes);
            }
            catch (Exception ex)
            {
                response = Result<List<Cliente>>.Error(Errors.GlobalErroDeRepositorio.Key, ex.Message);
            }

            return response;
        }

        public async Task<Result> DeletarCliente(Guid idCliente)
        {
            Result response = Result.Ok();
            try
            { 
                _clienteRepository.Remover(idCliente);
            }
            catch (Exception ex)
            {
                response = Result<Cliente>.Error(Errors.GlobalErroDeRepositorio.Key, ex.Message);
            }

            return response;
        }
    }
}

