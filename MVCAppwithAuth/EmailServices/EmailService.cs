using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using MVCAppwithAuth.EmailServices;
namespace MVCAppwithAuth.EmailServices
{
    public class EmailService
    {

       

        // Send the forgot password email using MailKit
        public void SendForgotPasswordEmail(string recipientEmail, string resetPasswordLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Somnath Bera", "no-reply@sombee.com"));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = "Forgot Password";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Please click  the following token to reset your password: <a href=\"{resetPasswordLink}\">{resetPasswordLink}</a></p>";
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("sandbox.smtp.mailtrap.io", 2525, SecureSocketOptions.StartTls);
                client.Authenticate("9e60e4770bffee", "c47ecdce2e3e68");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        public static string GenerateForgotPasswordTokenAsync()
        {
            // Generate a random token using a cryptographically strong random number generator
            byte[] randomBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            // Convert the random bytes to a string representation (e.g., hexadecimal or Base64)
            string token = Convert.ToBase64String(randomBytes);

            // Generate and store the password reset token for the user
            //var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            //await userManager.SetAuthenticationTokenAsync(user, "ResetPassword", token);

            // Return the generated token
            return token;
        }
    }
}