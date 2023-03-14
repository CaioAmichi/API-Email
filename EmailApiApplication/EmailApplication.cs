
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
    public class EmailApplication : IEmailApplication
    {
        private readonly IClienteRepositorio _clienteRepository;

        public EmailApplication(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepository = clienteRepositorio;
        }

        public static bool ValidaEmail(string email)
        {
            if(new EmailAddressAttribute().IsValid(email) && !string.IsNullOrEmpty(email))
            {
                return true;
            }
            return false;
        }

        public static bool ValidaString(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        //converte de base64 para texto
        static public string DecodeFrom64(string dados)
        {


            byte[] dadosAsBytes = System.Convert.FromBase64String(dados);
            string resultado = System.Text.ASCIIEncoding.ASCII.GetString(dadosAsBytes);
            return resultado;


        }

        public async Task<Result> EnviarEmail(Email email)
        {

            Result response = Result.Ok();

            bool destinatarioExiste = false;

            try
            {
                var cliente = _clienteRepository.ObterPorId(email.idCliente);
                if(cliente == null)
                {
                    return response = Result<Email>.Error(Errors.ClienteInexistente);
                }

                //Cria objeto com dados do e-mail.
                MailMessage objEmail = new MailMessage();

                if (!ValidaEmail(cliente.EmailRemetente))
                {
                    return response = Result<Email>.Error(Errors.RemetenteInvalido);
                }
                
                //Define o Campo From e ReplyTo do e-mail.
                if(email.NomeRemetente != null)
                {
                    objEmail.From = new System.Net.Mail.MailAddress(email.NomeRemetente + "<" + cliente.EmailRemetente + ">");
                }
                else
                {
                    objEmail.From = new MailAddress(email.NomeRemetente);
                }
                

                
                //Define os destinatários do e-mail.
                foreach (var destinatario in email.Destinatarios)
                {
                    if (ValidaEmail(destinatario))
                    {
                        objEmail.To.Add(destinatario);
                        destinatarioExiste = true;
                    }
                    
                }


                //Enviar cópia para.
                if (email.DestinatariosCopia.Count() > 0)
                {
                    foreach (var destinatarioCopia in email.DestinatariosCopia)
                    {
                        if (ValidaEmail(destinatarioCopia))
                        {
                            objEmail.CC.Add(destinatarioCopia);
                            destinatarioExiste = true;
                        }
                    }
                }

                //Enviar cópia oculta para.
                if (email.DestinatariosCopia.Count() > 0)
                {
                    foreach (var destinatarioOculto in email.DestinatariosCopiaOculto)
                    {
                        if (ValidaEmail(destinatarioOculto))
                        {
                            objEmail.Bcc.Add(destinatarioOculto);
                            destinatarioExiste = true;
                        }
                    }
                }
                
                if(destinatarioExiste == false)
                {
                    return response = Result<Email>.Error(Errors.SemDestinatario);
                }

                //Define a prioridade do e-mail.
                if(email.Prioridade <= 0 && email.Prioridade > 3)
                {
                    return response = Result<Email>.Error(Errors.ListaPrioridadeInvalida);
                }

                switch (email.Prioridade)
                {
                    case 1:
                        objEmail.Priority = MailPriority.Low;
                        break;
                    case 2:
                        objEmail.Priority = MailPriority.Normal;
                        break;
                    case 3:
                        objEmail.Priority = MailPriority.High;
                        break;
                    default:
                        break;
                }
                
          

                //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
                objEmail.IsBodyHtml = email.Html;

                //Define título do e-mail.
                if (ValidaString(email.Assunto))
                {
                    return response = Result<Email>.Error(Errors.AssuntoVazio); 
                }           
                objEmail.Subject = email.Assunto;
                
                //Define o corpo do e-mail.
                if (ValidaString(email.Conteudo))
                {
                    return response = Result<Email>.Error(Errors.ConteudoVazio);
                }

                if(email.Html == true)
                {
                    byte[] dadosAsBytes = System.Convert.FromBase64String(email.Conteudo);
                    objEmail.Body = System.Text.ASCIIEncoding.UTF7.GetString(dadosAsBytes); //desconverte o comprovante pra String
                }
                else
                {
                    objEmail.Body = email.Conteudo;
                }
                

                //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
                objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                // Caso queira enviar um arquivo anexo
                //Caminho do arquivo a ser enviado como anexo
                //foreach (string arquivo in ListaAnexos)
                //{
                //    // Cria o anexo para o e-mail
                //    Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);
                //    // Anexa o arquivo a mensagem
                //    objEmail.Attachments.Add(anexo);
                //}

                //Cria objeto com os dados do SMTP
                SmtpClient objSmtp = new SmtpClient();

                var senhaDescriptografada = DecodeFrom64(cliente.SenhaRemetente);
                //Alocamos o endereço do host para enviar os e-mails  
                objSmtp.Credentials = new System.Net.NetworkCredential(cliente.EmailRemetente, senhaDescriptografada);

                objSmtp.Host = cliente.Smtp;

                objSmtp.Port = email.Porta;

                //Caso deseje habilitar o SSL
                objSmtp.EnableSsl = email.SSL;


                //Enviamos o e-mail 
                objSmtp.Send(objEmail);

                objEmail.Dispose();
            }
            catch (Exception ex)
            {
                response = Result.Error(Errors.GlobalErroDeRepositorio.Key, ex.Message);
            }

            
            return response;
        }
    }
}
