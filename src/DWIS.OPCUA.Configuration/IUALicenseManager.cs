namespace DWIS.OPCUA.Configuration
{ 
    public interface IUALicenseManager
    {
        bool ManageLicense(IUAApplicationConfiguration configuration = null);
    }
}
