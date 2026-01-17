using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Constants
{
    public class BaseUrlConfiguration
    {
        public const string CONFIG_NAME = "baseUrls";

        public string CivilRegistryBaseAddress { get; set; }
        public string CentralBankCreditCheckBaseAddress { get; set; }
        public string PoliceClearanceBaseAddress { get; set; }
    }
}
