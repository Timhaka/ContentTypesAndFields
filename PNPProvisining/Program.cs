﻿using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Connectors;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;
using OfficeDevPnP.Core.Entities;
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



                //Microsoft.SharePoint.Client.Field field = ctx.Web.GetFieldByInternalName("PicTest");

                //Console.WriteLine(field.SchemaXml);
                //Console.ReadLine();

                //Microsoft.SharePoint.Client.Field field2 = ctx.Web.GetFieldByInternalName("LinkIn");

                //Console.WriteLine(field2.SchemaXml);
                //Console.ReadLine();

                ////Testing The first Template, Hockeyplayers
                //XMLFileSystemTemplateProvider prov = new XMLFileSystemTemplateProvider(@"C:\Users\timha\source\repos\Officedeveloper1\ContentTypesAndFields\PNPProvisining\", "");
                //string name = "Template.xml";
                //ProvisioningTemplate template = prov.GetTemplate(name);
                //ctx.Web.ApplyProvisioningTemplate(template);

                ////testing the secound Xml Template. About Animals.
                //XMLFileSystemTemplateProvider prov2 = new XMLFileSystemTemplateProvider(@"C:\Users\timha\source\repos\Officedeveloper1\ContentTypesAndFields\PNPProvisining\", "");
                //string name2 = "Template -LookupTest.xml";
                //ProvisioningTemplate template2 = prov2.GetTemplate(name2);
                //ctx.Web.ApplyProvisioningTemplate(template2);

                List list = ctx.Web.GetListByTitle("Employee");
                ctx.Load(list);

                Microsoft.SharePoint.Client.User users = ctx.Site.RootWeb.EnsureUser("tim@folkis2017.onmicrosoft.com");
                ctx.Load(users);
                ctx.ExecuteQuery();


                ListItem item1 = list.AddItem(new ListItemCreationInformation());
                item1["Title"] = "Manager";
                item1["TIM_Employee"] = users;
                item1["TIM_Picture"] = "http://www.catster.com/wp-content/uploads/2017/06/small-kitten-meowing.jpg";
                item1["TIM_Linkedin"] = "https://www.linkedin.com/feed/";
                item1["TIM_Age"] = 30;
                item1["TIM_Education"] = "Basic";
                item1.Update();
                

                ListItem item2 = list.AddItem(new ListItemCreationInformation());
                item2["Title"] = "Staff";
                item2["TIM_Employee"] = users;
                item2["TIM_Picture"] = "https://d2btg9txypwkc4.cloudfront.net/media/catalog/category/Kampanjer.jpg";
                item2["TIM_Linkedin"] = "https://www.linkedin.com/feed/";
                item2["TIM_Age"] = 20;
                item2["TIM_Education"] = "Highschool";
                item2.Update();
               

                ListItem item3 = list.AddItem(new ListItemCreationInformation());
                item3["Title"] = "Staff";
                item3["TIM_Employee"] = users;
                item3["TIM_Picture"] = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/27/Tim_Studio2104.jpg/1200px-Tim_Studio2104.jpg";
                item3["TIM_Linkedin"] = "https://www.linkedin.com/feed/";
                item3["TIM_Age"] = 55;
                item3["TIM_Education"] = "University";
                item3.Update();
                ctx.ExecuteQuery();



                //testing the 3 xml template. about assignemnt.
                //XMLFileSystemTemplateProvider prov3 = new XMLFileSystemTemplateProvider(@"c:\users\timha\source\repos\officedeveloper1\contenttypesandfields\pnpprovisining\", "");
                //string name3 = "template-lookupassignment.xml";
                //ProvisioningTemplate template3 = prov3.GetTemplate(name3);
                //ctx.Web.ApplyProvisioningTemplate(template3);


                //ListItemCollection list = ctx.Web.Lists.GetByTitle("Employee").GetItems(CamlQuery.CreateAllItemsQuery());
                //ctx.Load(list);
                //ctx.ExecuteQuery();

                //foreach (var item in list)
                //{
                //    Console.WriteLine();
                //}


            }

            Console.WriteLine("Enter to Continue");
            Console.ReadKey();


        }
    }
}
