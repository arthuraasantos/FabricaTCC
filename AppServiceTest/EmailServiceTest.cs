﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCCPontoEletronico.AppService.Entity;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace AppServiceTest
{
    /// <summary>
    /// Testes do serviço de e-mail. Testes relacionados ao serviço de e-mail
    /// </summary>
    [TestClass]
    public class EmailServiceTest
    {
        [TestMethod]
        public void EmptyEmailTest()
        {
            IEmailService EmailService = new EmailService();
            string email = string.Empty;

            var response = EmailService.IsValid(email);

            Assert.IsFalse(response);
        }

        [TestMethod]
        public void WhiteSpaceEmailTest()
        {
            IEmailService EmailService = new EmailService();
            string email = "  ";

            var response = EmailService.IsValid(email);

            Assert.IsFalse(response);
        }

        [TestMethod]
        public void EmailValidTest()
        {
            IEmailService EmailService = new EmailService();
            string email = "contoso@pontonation.com";

            var isValid = EmailService.IsValid(email);

            Assert.IsTrue(isValid);
        }

        
    }
}
