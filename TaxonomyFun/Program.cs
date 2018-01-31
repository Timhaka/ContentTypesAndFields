﻿using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using OfficeDevPnP.Core.Entities;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxonomyFun
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ClientContext ctx = Common.Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/Tim"))
            {


                //CreateTerms(ctx);
                //CreateTaxonomyField(ctx);

                //////Testing The first Template,
                //XMLFileSystemTemplateProvider prov = new XMLFileSystemTemplateProvider(@"C:\Users\timha\source\repos\Officedeveloper1\ContentTypesAndFields\TaxonomyFun\", "");
                //string name = "Template2.xml";
                //ProvisioningTemplate template = prov.GetTemplate(name);
                //ctx.Web.ApplyProvisioningTemplate(template);


                //ReadingFromTaxonomyField(ctx);


                //Testing The first Template,
                //XMLFileSystemTemplateProvider prov = new XMLFileSystemTemplateProvider(@"C:\Users\timha\source\repos\Officedeveloper1\ContentTypesAndFields\TaxonomyFun", "");
                //string name = "SuperHeroes.xml";
                //ProvisioningTemplate template = prov.GetTemplate(name);
                //ctx.Web.ApplyProvisioningTemplate(template);


                DisplayListAndTaxanomyRead(ctx);



            }

            Console.WriteLine("Press Enter");
            Console.ReadKey();


        }

        static void DisplayListAndTaxanomyRead(ClientContext ctx)
        {

            List list = ctx.Web.GetListByTitle("Super Heroes");
            ListItemCollection listcol = list.GetItems(CamlQuery.CreateAllItemsQuery());
            ctx.Load(listcol);
            ctx.ExecuteQuery();

            foreach (var items in listcol)
            {
                Console.WriteLine("############################");
                TaxonomyFieldValue taxValue = items["TIM_SuperPower"] as TaxonomyFieldValue;
                TaxonomyFieldValueCollection TaxValueList = items["TIM_Weapon"] as TaxonomyFieldValueCollection;


                Console.WriteLine(items["Title"].ToString());
                Console.WriteLine(taxValue.Label);
                foreach (var item in TaxValueList)
                {
                    Console.WriteLine("----------------------");
                    Console.WriteLine("  " + item.Label);
                }
            }



        }
        static void ReadingFromTaxonomyField(ClientContext ctx)
        {
            // i have added one document manually after the xml importnat
            List list = ctx.Web.GetListByTitle("Important Document");

            ListItem item = list.GetItemById(2);
            
            ctx.Load(item);
            ctx.ExecuteQuery();

            ////Have to update this part so it works.
            //TermStore store = ctx.Site.GetDefaultSiteCollectionTermStore();
            //var term = store.GetTermInTermSet();

            //item.SetTaxonomyFieldValue("{5A6B931A-B085-402F-AD7C-AA9638F33CCF}".ToGuid(), "Policy",);
            //item.Update();
            //ctx.ExecuteQuery();

            TaxonomyFieldValue taxValue = item["TIM_DocType"] as TaxonomyFieldValue;

            Console.WriteLine(taxValue.Label);
            Console.ReadLine();


        }

        static void CreateTaxonomyField(ClientContext ctx)
        {
            //ctx.Web.GetFieldById("{4306F426-A772-4D1A-91D1-07F4CAA8884D}".ToGuid()).DeleteObject();

            TermStore store = ctx.Site.GetDefaultSiteCollectionTermStore();
            //guid is from the termset i created below
            Microsoft.SharePoint.Client.Taxonomy.TermSet term =  store.GetTermSet("{FCB857B8-8F82-4EDD-B49A-5A5A5D492174}".ToGuid());

            ctx.Load(term);
            ctx.ExecuteQuery();

            TaxonomyFieldCreationInformation info = new TaxonomyFieldCreationInformation();
            info.DisplayName = "Animal";
            //field term id. new guid
            info.Id = "{4306F426-A772-4D1A-91D1-07F4CAA8884D}".ToGuid();
            info.InternalName = "TIM_TaxAnimal";
            //connect it to the termset we created below
            info.TaxonomyItem = term;
            info.Group = "Tims Fields";

            ctx.Web.CreateTaxonomyField(info);
        }

        static void CreateTerms(ClientContext ctx)
        {
            TermStore store = ctx.Site.GetDefaultSiteCollectionTermStore();
            Microsoft.SharePoint.Client.Taxonomy.TermGroup group = store.GetTermGroupByName("Tim");

            if (group == null)
            {
                group = store.CreateTermGroup("Tim", "{9285FBFD-F1B1-44CA-ACFF-8CF4B271A5C2}".ToGuid());
            }

            Microsoft.SharePoint.Client.Taxonomy.TermSet term = group.EnsureTermSet("Animals", "{FCB857B8-8F82-4EDD-B49A-5A5A5D492174}".ToGuid(), 1033);

            term.CreateTerm("Dog", 1033, "{E6251A5B-8341-4FC9-9544-30670C5E115B}".ToGuid());
            Microsoft.SharePoint.Client.Taxonomy.Term cat = term.CreateTerm("Cat", 1033, "{F1D30452-0DD1-4B74-89CA-62167065BFF6}".ToGuid());
            term.CreateTerm("Horse", 1033, "{25E73090-CFEF-4A83-997D-56FB127F0B82}".ToGuid());

            ctx.ExecuteQuery();

            cat.CreateLabel("Katt", 1053, false);
            cat.CreateLabel("Feline", 1033, false);
            ctx.ExecuteQuery();




        }


    }
}
