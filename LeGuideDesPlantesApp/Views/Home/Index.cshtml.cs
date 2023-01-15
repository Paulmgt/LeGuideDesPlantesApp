
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SendGrid.Helpers.Mail;
using SendGrid;
using LeGuideDesPlantesApp.Services;
using Microsoft.Extensions.Options;
using Microsoft.Build.Framework;
using Microsoft.Data.SqlClient.DataClassification;

namespace LeGuideDesPlantesApp.Areas.Identity.Pages.Account
{
    public class Index : PageModel
    {


        public Index(IOptions<AuthMessageSenderOptions> optionsAccessor,IConfiguration configuration)
        {
            Options = optionsAccessor.Value;
            Configuration = configuration;

        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager

        public IConfiguration Configuration { get; }



        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>

        public InputModel Input { get; set; }





        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 


        public class InputModel
        {

            public string Message { get; set; }

            public string Subject { get; set; }
            public string Email { get; set; }
        }

        public class LabelModel
        {
            public string ReplyTo { get; set; }
        }
        

            public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            ReturnUrl = returnUrl;
        }


       


        public async Task SendEmailAsync(string toEmail, string subject, string message, string replyTo)
        {
            if (string.IsNullOrWhiteSpace(Options.SendGridKey))
            {
                Options.SendGridKey = Configuration["SendGrid:APIKey"];
            }

            if (string.IsNullOrWhiteSpace(Options.SendGridUser))
            {
                Options.SendGridUser = Configuration["AppSettings:AppUserName"];
            }

            await OnPostAsync(Options.SendGridKey, subject, message, toEmail, replyTo);
        }

        
      


        public async Task<IActionResult> OnPostAsync( string subject, string message, string toEmail,string replyTo, string returnUrl = null)
        {



            returnUrl ??= Url.Content("~/");




            if (!ModelState.IsValid)
            {

                var apiKey = Configuration["SendGrid:APIKey"];

                SendGridClient client = new(apiKey);


                string ReplyTo = Configuration["ismaelmaingault@gmail.com"];

                SendGridMessage msg = new()
                {

                    From = new EmailAddress(Input.Email),
                    Subject = (Input.Subject),
                    PlainTextContent = (Input.Message),
                    HtmlContent = (Input.Message)

                };
                msg.SetReplyTo(new EmailAddress(ReplyTo));
                

            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

       
    }
}
