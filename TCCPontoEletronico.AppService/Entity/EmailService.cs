using System.Net.Mail;
using System;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;
using System.Net;

namespace TCCPontoEletronico.AppService.Entity
{
    public class EmailService: IEmailService
    {
        public bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (email.Contains(">="))
                return false;

            if (email.Contains("<="))
                return false;

            return true;
        }

        public void SendDefaultMail(string receiver, string subject, string bodyMessage)
        {
            try
            {
                string sender = "pontonationcontato@gmail.com"; // Email fixo para contatos
                string senderName = "Ponto Eletrônico";

                MailMessage email = new MailMessage();
                email.From = new MailAddress(sender, senderName);
                email.To.Add(receiver);
                email.Priority = MailPriority.Normal;
                email.IsBodyHtml = true;
                email.Subject = subject;
                email.Body = bodyMessage;
                email.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                email.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

                SmtpClient smpt = new SmtpClient("smtp.gmail.com", 465);
                smpt.Send(email);
            }
            catch (Exception ex)
            {
                //ToDo Gerar log de erro
                throw;
            }
        }

        public void SendMailNewUser(NewRegisterDTO user)
        {
            try
            {
                string sender = "pontonationcontato@gmail.com"; // Email fixo para contatos
                string senderPassword = "pontonation";
                string senderName = "Ponto Eletrônico";

                MailMessage email = new MailMessage();
                email.From = new MailAddress(sender, senderName);
                email.To.Add(user.EmployeeEmail);
                email.Priority = MailPriority.Normal;
                email.IsBodyHtml = true;
                email.Subject = "Nosso primeiro contato";
                email.Body =
                    "<html> "+
                    "<body>" +
                    "Olá + <strong> "+user.EmployeeName+"</strong>, <br/>" +
                    "  &nbsp;    Olha que legal, esse é nosso primeiro contato e o motivo dele é seu cadastro. <br/>" +
                    "  &nbsp;    Em nome dos CEOs do Ponto Eletrônico gostaria de agradecer por começar a utilizar nossa ferramenta. <br/> " +
                    "<br/>" +
                    "  &nbsp; Seu cadastro foi criado com sucesso. E os dados para você acessar são: <br/> " +
                    "  &nbsp;&nbsp; E-mail: " + user.EmployeeEmail + " <br/>" +
                    "  &nbsp;&nbsp;   Senha: " + user.EmployeePassword + " <br/>" +
                    "  &nbsp;&nbsp;   Empresa: " + user.OrganizationName + " <br/>" +
                    "<br/>" +
                    "  &nbsp;   Mais uma vez seja bem vindo, e qualquer coisa faça contato conosco(email para contato: <strong> pontonationcontato@gmail.com</strong>). <br/>" +
                    "  &nbsp;      Sua opinião é <strong> IMPORTANTÍSSIMA </strong> para nós! <br/> " +
                    "<br/>" +
                    "Atenciosamente,<br/>" +
                    "Equipe Ponto Nation <br/> "+
                    "</body>" +
                    "</html>";


                    //           "Olá " + user.EmployeeName + ",  \n" +
                    //" Olha que legal, esse é nosso primeiro contato e o motivo dele é seu cadastro. \n" +
                    //" Em nome dos CEOs do Ponto Eletrônico gostaria de agradecer por começar a utilizar nossa ferramenta." +
                    //" Seu cadastro foi criado com sucesso. E os dados para você acessar são:" +
                    //" E-mail: " + user.EmployeeEmail.ToUpper() + " \n" +
                    //" Senha: " + user.EmployeePassword + " \n" +
                    //"\n" +
                    //"Mais uma vez seja bem vindo, e qualquer coisa faça contato conosco(email para contato: 'pontonationcontato@gmail.com'). Sua opinião é IMPORTANTÍSSIMA para nós!"+
                    //"\n" +
                    //"Atenciosamente," +
                    //"Equipe Ponto Nation";
                    
                email.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                email.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(sender, senderPassword);
                //smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                //ToDo Gerar log de erro
                throw;
            }
        }

        public void NotifyNewUserForCEOs(string employeeId)
        {
            //TODO implementar
            // Enviar e-mail para ARTHUR, MARLON e CHARLES
            throw new NotImplementedException();
        }

        
    }
}
