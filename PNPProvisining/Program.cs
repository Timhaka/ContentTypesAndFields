using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Connectors;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPProvisining
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ClientContext ctx = Common.Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/Tim"))
            {



                //Microsoft.SharePoint.Client.Field field = ctx.Web.GetFieldByInternalName("ChoiceTest");

                //Console.WriteLine(field.SchemaXml);
                //Console.ReadLine();


                XMLFileSystemTemplateProvider prov = new XMLFileSystemTemplateProvider(@"C:\Users\timha\source\repos\Officedeveloper1\ContentTypesAndFields\PNPProvisining\", "");
                string name = "Template.xml";
                ProvisioningTemplate template = prov.GetTemplate(name);
                ctx.Web.ApplyProvisioningTemplate(template);

            }

            Console.WriteLine("Enter to Continue");
            Console.ReadKey();


        }
    }
}
