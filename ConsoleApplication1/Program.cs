using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.ServiceReference1;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateIndiciumRequest createRequest = new CreateIndiciumRequest();
            CreateIndiciumResponse createResponse = new CreateIndiciumResponse();
            SwsimV50SoapClient client = new SwsimV50SoapClient("SwsimV50Soap", "https://swsim.testing.stamps.com/swsim/swsimv50.asmx");
            client.c
        }
    }
}
