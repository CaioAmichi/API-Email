using EmailApi.Domain.Models;
using System.Collections.Generic;
namespace EmailApi.Domain.Consts
{
    public static class Errors
    {
        #region Global
        public static readonly Notificacao GlobalErroDeRepositorio = new Notificacao("Err01", "Erro durante a execução da requisição, tente mais tarde");
        #endregion
        public static readonly Notificacao RequestsInvalido = new Notificacao("Err", "Dados enviados incorretamente");
        #region Email
        public static readonly Notificacao RemetenteInvalido = new Notificacao("Err01", "O remetente enviado é inválido");
        public static readonly Notificacao SemDestinatario = new Notificacao("Err01", "O email deve ser enviado para algum destinatário");
        public static readonly Notificacao ListaPrioridadeInvalida = new Notificacao("Err01", "Envie uma Lista de Prioridade valída");
        public static readonly Notificacao AssuntoVazio = new Notificacao("Err01", "Envie um assunto de email valído");
        public static readonly Notificacao ConteudoVazio = new Notificacao("Err01", "Envie um conteúdo de email valído");
        #endregion
        #region Cliente
        public static readonly Notificacao NomeEmpresaInvalido = new Notificacao("Err01", "Nome da empresa enviado Invalído");
        public static readonly Notificacao EmailRemetente = new Notificacao("Err01", "O  email remetente enviado é invalído");
        public static readonly Notificacao SenhaInvalida = new Notificacao("Err01", "A senha enviada é invalída");
        public static readonly Notificacao EmailRemetenteInvalido = new Notificacao("Err01", "Envie um Email Remetente valído");
        public static readonly Notificacao SmtpInvalido = new Notificacao("Err01", "Envie um Smtp valído");
        public static readonly Notificacao ClienteInexistente = new Notificacao("Err01", "Cliente não existe no banco");
        #endregion

    }
}