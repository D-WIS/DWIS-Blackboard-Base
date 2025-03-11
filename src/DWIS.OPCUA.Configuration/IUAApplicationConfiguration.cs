namespace DWIS.OPCUA.Configuration
{
    public interface IUAApplicationConfiguration
    {
        string LicenseFilePath { get; set; }
        string ApplicationName { get; set; }
        string ProductName { get; set; }
        string CertificateStorePath { get; set; }
        string CertificateSubjectName { get; set; }
        string TrustedCertificateStore { get; set; }
        string IssuerCertificateStore { get; set; }
        string RejectedCertificatesStore { get; set; }
    }
}