//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using PD.EmailSender.Helpers;
//using PD.EmailSender.Helpers.Model;
//using Newtonsoft.Json;
//using System.IO;
//using Microsoft.AspNetCore.Http;

//namespace SmtpTestConnection.Web.Controllers.APIControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SendmailController: SmtpTestConnectionControllerBase
//    {
//        public SendmailController()
//        {

//        }

//        [HttpGet("getme")]
//        public IActionResult GetMeth()
//        {
//            string dd = string.Empty;
//            //var objString = JsonConvert.SerializeObject(new MessageObject() {  Message = new MessageDTO { }, Settings = new  SenderSettingsDTO { } });
//            var objString = JsonConvert.SerializeObject(new MessageObject());
//            var obj = new MessageObject();
//            //var obj = new MessageObject() { Message = new  MessageDTO { }, Settings = new  SenderSettingsDTO { } };

//            SendSingleMail(obj);

//            return Ok(obj);
//        }

//        [Route("sendoneemail")]
//        [HttpPost]
//        public IActionResult SendSingleMail([FromForm] MessageObject messageObject)
//        {
//            //check the followings - reciever emails - sender domain settings - dispaly name - message

//            //if (messageObject == null || messageObject.Message == null || messageObject.Settings == null)
//            //{
//            //    return BadRequest("Invalid request");
//            //}

//            if (messageObject == null || messageObject.Message == null || messageObject.Domain == null)
//            {
//                return BadRequest("Invalid request");
//            }

//            //var emailMessModel = MapMessageModel(new List<MessageDTO> { messageObject.Message });
//            var emailMessModel = MapObjects(messageObject).MessageMappedModel;
//            var emailSenderSettings = MapObjects(messageObject).Setting;
//            //var emailSenderSettings = MapDomainSettings(messageObject.Settings);

//            //string modelValidationMess = ValidateMessageModel(emailMessModel);
//            string modelValidationMess = ValidateMessageModel(new List<MessageModel> { emailMessModel });
//            if (!string.IsNullOrWhiteSpace(modelValidationMess))
//            {
//                return BadRequest(modelValidationMess);
//            }

//            if (!ValidateDomainSettings(emailSenderSettings))
//            {
//                return BadRequest("Invalid smtp settings");
//            }
            
//            bool res = SendMail.SendSingleEmail(emailMessModel, emailSenderSettings);

//            if (res)
//            {
//                return Ok();
//            }
//            else
//            {
//                return BadRequest("Email failed to send.");
//            }
//        }


//        [HttpPost("sendmanyemail")]
//        public IActionResult SendManyMail(MultipleMessageObject messageObject)
//        {
//            //check the followings - reciever emails - sender domain settings - dispaly name - message

//            if (!messageObject.Messages?.Any() ?? true)
//            {
//                return BadRequest("Invalid request");
//            }
//            if (messageObject.Settings == null)
//            {
//                return BadRequest("Invalid request");
//            }

//            var emailMessModel = MapMessageModel(messageObject.Messages);
//            var emailSenderSettings = MapDomainSettings(messageObject.Settings);

//            string modelValidationMess = ValidateMessageModel(emailMessModel);
//            if (!string.IsNullOrWhiteSpace(modelValidationMess))
//            {
//                return BadRequest(modelValidationMess);
//            }

//            if (!ValidateDomainSettings(emailSenderSettings))
//            {
//                return BadRequest("Invalid smtp settings");
//            }

//            var res = SendMail.SendMultipleEmail(emailMessModel, emailSenderSettings);

//            if (res.Count(x=>x == true) > 0)
//            {
//                return Ok(res);
//            }
//            else
//            {
//                return BadRequest("Email failed to send.");
//            }
//        }

//        [HttpPost("sendemailattached")]
//        public IActionResult SendMailWithAttachedFile([FromForm] MessageObject messageObject)
//        {
//           return SendSingleMail(messageObject);
//        }

//        private string ValidateMessageModel(List<MessageModel> models)
//        {
//            List<bool> isValidated = new List<bool>();

//            models.ForEach(x =>
//            {
//                isValidated.Add(x.EmailAddresses == null);
//                isValidated.Add(x.EmailAddresses.Length < 1);
//                isValidated.Add(string.IsNullOrWhiteSpace(x.Message));
//                isValidated.Add(string.IsNullOrWhiteSpace(x.EmailDisplayName));


//            });

//            if (isValidated.Count(x=> x == false) > 0)
//            {
//                return "Invalid message model. You may check to ensure you send reciever's email address(es), email message and display name";
//            }

//            return "";
            
//        }

//        private bool ValidateDomainSettings(SenderSettings settings)
//        {
//            if (string.IsNullOrWhiteSpace(settings.Domain))
//            {
//                return false;
//            }
//            else if (string.IsNullOrWhiteSpace(settings.Email) || !settings.Email.Contains("@"))
//            {
//                return false;
//            }
//            else if (string.IsNullOrWhiteSpace(settings.Password))
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

//       private (MessageModel MessageMappedModel, SenderSettings Setting) MapObjects(MessageObject item)
//        {
//            var mess = new MessageModel
//            {
//                AttachmentInCode = item.AttachmentInCode,
//                Attachments = item.Attachments,
//                Bcc = item.Bcc,
//                Cc = item.Cc,
//                CompanyLogoLink = item.CompanyLogoLink,
//                CopyrightName = item.CopyrightName,
//                CopyrightYear = item.CopyrightYear,
//                EmailAddresses = item.EmailAddresses,
//                EmailDisplayName = item.EmailDisplayName,
//                FacebookLink = item.FacebookLink,
//                Filename = item.Filename,
//                Message = item.Message,
//                ReplyTo = item.ReplyTo,
//                Subject = item.Subject,
//                TwitterLink = item.TwitterLink,
//                User = item.User
//            };

//            var set = new SenderSettings
//            {
//                Domain = item.Domain,
//                Email = item.Email,
//                Password = item.Password,
//                Port = item.Port
//            };

//            return (mess, set);
//        }


//        private List<MessageModel> MapMessageModel(List<MessageDTO> mess)
//        {
//            List<MessageModel> resModel = new List<MessageModel>();
//            foreach (var item in mess)
//            {
//                resModel.Add(new MessageModel
//                {
//                    AttachmentInCode = item.AttachmentInCode,
//                    Attachments = item.Attachments,
//                    Bcc = item.Bcc,
//                    Cc = item.Cc,
//                    CompanyLogoLink = item.CompanyLogoLink,
//                    CopyrightName = item.CopyrightName,
//                    CopyrightYear = item.CopyrightYear,
//                    EmailAddresses = item.EmailAddresses,
//                    EmailDisplayName = item.EmailDisplayName,
//                    FacebookLink = item.FacebookLink,
//                    Filename = item.Filename,
//                    Message = item.Message,
//                    ReplyTo = item.ReplyTo,
//                    Subject = item.Subject,
//                    TwitterLink = item.TwitterLink,
//                    User = item.User
//                });
//            }
//            return resModel;
//        }

//        private SenderSettings MapDomainSettings(SenderSettingsDTO settingsDTO)
//        {
//            return new SenderSettings
//            {
//                Domain = settingsDTO.Domain,
//                Email = settingsDTO.Email,
//                Password = settingsDTO.Password,
//                Port = settingsDTO.Port
//            };
//        }
//    }

//    public class MultipleMessageObject
//    {
//        public List<MessageDTO> Messages { get; set; }
//        public SenderSettingsDTO Settings { get; set; }
//    }

//    //public class MessageObject
//    //{
//    //    public MessageDTO Message { get; set; }
//    //    public SenderSettingsDTO Settings { get; set; }
//    //}

//    //public class MessageObject
//    //{
//    //    public string CompanyLogoLink { get; set; }
//    //    public string TwitterLink { get; set; }
//    //    public string FacebookLink { get; set; }
//    //    public string User { get; set; }
//    //    public string EmailDisplayName { get; set; }
//    //    public string ReplyTo { get; set; }
//    //    public string CopyrightName { get; set; }
//    //    public string[] Cc { get; set; }
//    //    public string[] EmailAddresses { get; set; }
//    //    public string Filename { get; set; }
//    //    public List<Stream> AttachmentInCode { get; set; }
//    //    public List<IFormFile> Attachments { get; set; }
//    //    public string Message { get; set; }
//    //    public string Subject { get; set; }
//    //    public string[] Bcc { get; set; }
//    //    public string CopyrightYear { get; set; }
//    //    public string Domain { get; set; }
//    //    public int Port { get; set; }
//    //    public string Email { get; set; }
//    //    public string Password { get; set; }
//    //}

//    public class MessageDTO
//    {
//        public string CompanyLogoLink { get; set; }
//        public string TwitterLink { get; set; }
//        public string FacebookLink { get; set; }
//        public string User { get; set; }
//        public string EmailDisplayName { get; set; }
//        public string ReplyTo { get; set; }
//        public string CopyrightName { get; set; }
//        public string[] Cc { get; set; }
//        public string[] EmailAddresses { get; set; }
//        public string Filename { get; set; }
//        public List<Stream> AttachmentInCode { get; set; }
//        public List<IFormFile> Attachments { get; set; }
//        public string Message { get; set; }
//        public string Subject { get; set; }
//        public string[] Bcc { get; set; }
//        public string CopyrightYear { get; set; }
//    }

//    public class SenderSettingsDTO
//    {
//        public string Domain { get; set; }
//        public int Port { get; set; }
//        public string Email { get; set; }
//        public string Password { get; set; }
//    }

    

//}
