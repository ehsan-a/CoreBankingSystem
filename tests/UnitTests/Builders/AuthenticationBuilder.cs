using CoreBanking.Domain.Entities;

namespace UnitTests.Builders
{
    public class AuthenticationBuilder
    {
        private Authentication _authentication;

        public string NationalCode => "0024264788";
        public Guid UserId => new Guid("4692D21F-3B31-4B41-A5D4-2A9AF937147E");
        public bool CentralBankCreditCheckPassed => true;
        public bool CivilRegistryVerified => true;
        public bool PoliceClearancePassed => true;

        public AuthenticationBuilder()
        {
            _authentication = WithDefaultValue();
        }

        public Authentication Build()
        {
            return _authentication;
        }

        public Authentication WithDefaultValue()
        {
            return _authentication = Authentication.Create(NationalCode, CentralBankCreditCheckPassed, CivilRegistryVerified, PoliceClearancePassed, UserId);
        }
    }
}
