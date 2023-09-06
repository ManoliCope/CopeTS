using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectX.Interfaces
{
    public interface IDocumentService
    {
        byte[] GeneratePdfFromString();
        byte[] GeneratePdfFromRazorView(int policyid);

    }
}
