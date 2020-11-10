using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LSPWebAPI.APP_Context
{
    public class Keys
    {
        public Keys Instance { get; } = new Keys();

        public const string LspPubKey = "<RSAKeyValue><Modulus>oEgGmFjqFS6vL6vrYvu6Q2kSd3fimLRz3JE2QVM/SGW7V9XfH1RjPoEJA9tDWfZAMasDBqrSRcd5d/FC4USPJRY7oDwYCJH07Jcgf2gS+6vdWPciNx/W7hiX2HbzU/D3t6h9aAj+SKSR8ve3kMQTxq4QdriAgEhjUzm+pwAQH30=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        public const string APIPriKey = "<RSAKeyValue><Modulus>1sa7PrEiVW80ggiffDDOawIb7b3KhMD1AueqKhYLgYHRydJ7rmvEn3rtDC8PKuchvrRsT5V6JjbUefbZbo3iP+5iFpjEXB1sfOy2Z8mfhehhR2AH0UF+H48cwh+u9uSfJL/K0hzFtFrRpJ6Z1rgwlNUPUVyA5vRTf1i4cf7JRK8=</Modulus><Exponent>AQAB</Exponent><P>12YLrZ0+/M6M6nu6yRCHEDVbMXQBwk2dEjV9tBbZopwGQU+BEG4KAcqVI4vlNlpwdupa6XReJihVRLg5Q4W7ZQ==</P><Q>/0Kn8EkKZeAYQC6mC4VhMbG3fPgDuCc0uV7YvUvsVVXwn4eDuOenh1rlC3YO5RHvcYjLePoAIJjlDBMSzrrggw==</Q><DP>rG6UireG5Pq09EF4ld0VQnR0PHKRtepMA3eu2awxLWuZ1k6/E1gDystR+NLU+14LCicyABGYDRPcrtaLgPJdwQ==</DP><DQ>6/QNkQuzVOCFCi8UxemRIoKIfjg0F/IFxqRp7PFVkLxUJOL7W9ym+3OF7cY/lnexwl0U2MsfewJaF4M6C2arSQ==</DQ><InverseQ>s4VCHsBJCl0JjwupNW1DM6zDcvS/Vl68FpV8zxoDhBMwf3o/JfcypZZShSvyz3EebMaqGCt1fNFOfWQ43srwtg==</InverseQ><D>AGiIt80yA1DFApZHnCUJNdTfZR7rDdn1qheMqd9rqC36AYgGUJLHeriK6NU4eMMCiNB8M26IuLfgxLz+aG8zbFDMbm6+eCSljCdEF8mxefynpAZoBTj2QJD/NABoO3n66xwCAXSHwWM3dseXIoj2KokxbTd9srrTWcQ/fcbEWVk=</D></RSAKeyValue>";


    }
}