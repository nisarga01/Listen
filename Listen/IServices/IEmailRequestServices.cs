using Listen.Model;
using Listen.ServiceResponse;

namespace Listen.IServices
{
    public interface IEmailRequestServices
    {
        Task<ServiceResponse<EmailRequest>> SendEmailAsync(EmailRequest emailRequest);
    }
}
