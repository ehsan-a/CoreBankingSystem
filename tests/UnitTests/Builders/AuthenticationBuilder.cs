using CoreBanking.Domain.Entities;

namespace UnitTests.Builders
{
    public class AuthenticationBuilder
    {
        private Authentication _authentication;

        public string NationalCode => "0024264788";
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
            return _authentication = new Authentication(NationalCode, CentralBankCreditCheckPassed, CivilRegistryVerified, PoliceClearancePassed);
        }
    }
}
