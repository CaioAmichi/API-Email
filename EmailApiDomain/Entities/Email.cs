using System;
using System.Collections.Generic;

namespace EmailApi.Domain.Entities
{
    public enum ListaPrioridade
    {
        Baixa,
        Normal,
        Alta
    }

    public class Email
    {
        public Guid idCliente { get; set; }
        public string NomeRemetente { get; set; }
        public int Porta { get; set; }
        public List<string> Destinatarios { get; set; } = new List<string>();
        public List<string> DestinatariosCopia { get; set; } = new List<string>();
        public List<string> DestinatariosCopiaOculto { get; set; } = new List<string>();
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
        public bool SSL { get; set; } 
        public bool Html { get; set; }
        public int Prioridade { get; set; }
    }
}