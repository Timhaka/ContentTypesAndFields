using ContentTypesAndFields.CodeExamples;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentTypesAndFields
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ClientContext ctx = Common.Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/Tim"))
            {

                //ContentTypeFun.MyFirstContentType(ctx);
                //ContentTypeAssignemnt.CreateBookCT(ctx);
                //ContentTypeAssignemnt.CreateCV(ctx);
                ContentTypeAssignemnt.RenameTitleFieldonCV(ctx);
            }

            Console.WriteLine("Enter to Continue");
            Console.ReadKey();

        }
    }
}
