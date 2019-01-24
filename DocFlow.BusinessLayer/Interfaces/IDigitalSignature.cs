using DocFlow.BusinessLayer.Models.DigitalSignature;

namespace DocFlow.BusinessLayer.Interfaces
{
    public interface IDigitalSignature
    {
        byte[] SetDigitalSignature(DigitalSignatureViewModel digitalSignature, int reportId);
    }
}
