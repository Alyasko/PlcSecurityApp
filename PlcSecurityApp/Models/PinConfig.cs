using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;

namespace PlcSecurityApp.Models
{
    public class PinConfig
    {
        public short ByteAddress { get; set; }
        public byte PinAddress { get; set; }
    }
}
