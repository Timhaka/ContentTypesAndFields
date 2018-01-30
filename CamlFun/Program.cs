using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamlFun
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ClientContext ctx = Common.Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/Tim"))
            {
                //XMLFileSystemTemplateProvider prov = new XMLFileSystemTemplateProvider(@"C:\Users\timha\source\repos\Officedeveloper1\ContentTypesAndFields\CamlFun\", "");
                //string name = "Product.xml";
                //ProvisioningTemplate template = prov.GetTemplate(name);
                //ctx.Web.ApplyProvisioningTemplate(template);


                //GetItemsPriceBigger(ctx);



                //write a query using the help of query designer to show all items that are not 
                //expired and have a date greater then Year created.

                //ShowAllItemsThatAreNotExpired(ctx);

                //Write another query where product type = "your choice" 
                //and the number in stock > 10(or your choice)

                //ShowItemsOfProductTypeAndNumberInStore(ctx);

                //Return all items that start with an "A" then
                //order by title ascending
               // OrderAllItemsByTitleAndStartWithA(ctx);



            }
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Enter to Continue");
            Console.ReadKey();


        }

 
        public static void OrderAllItemsByTitleAndStartWithA(ClientContext ctx)
        {
            List spList = ctx.Web.Lists.GetByTitle("Products");

            ctx.Load(spList);
            ctx.ExecuteQuery();

            if (spList != null && spList.ItemCount > 0)
            {
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml =
                  @"<View>  
                        <Query> 
                           <Where><BeginsWith><FieldRef Name='Title' /><Value Type='Text'>B</Value></BeginsWith></Where><OrderBy><FieldRef Name='Title' /></OrderBy> 
                        </Query> 
                         <ViewFields><FieldRef Name='Title' /></ViewFields> 
                  </View>";

                ListItemCollection listItems = spList.GetItems(camlQuery);
                ctx.Load(listItems);
                ctx.ExecuteQuery();

                foreach (var item in listItems)
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine(item["Title"].ToString());
                    //Console.WriteLine(item["PROD_Type"].ToString());
                    //Console.WriteLine(item["PROD_InStock"].ToString());
                   
                }
            }
        }

        public static void ShowItemsOfProductTypeAndNumberInStore(ClientContext ctx)
        {
            List spList = ctx.Web.Lists.GetByTitle("Products");

            ctx.Load(spList);
            ctx.ExecuteQuery();

            if (spList != null && spList.ItemCount > 0)
            {
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml =
                   @"<View>  
                    <Query> 
                      <Where><And><Eq><FieldRef Name='PROD_Type' /><Value Type='Choice'>Dairy</Value></Eq><Geq><FieldRef Name='PROD_InStock' /><Value Type='Number'>5</Value></Geq></And></Where> 
                    </Query> 
                      <ViewFields><FieldRef Name='Title' /><FieldRef Name='PROD_Type' /><FieldRef Name='PROD_InStock' /></ViewFields> 
                     </View>";

                ListItemCollection listItems = spList.GetItems(camlQuery);
                ctx.Load(listItems);
                ctx.ExecuteQuery();

                foreach (var item in listItems)
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine(item["Title"].ToString());
                    Console.WriteLine(item["PROD_Type"].ToString());
                    Console.WriteLine(item["PROD_InStock"].ToString());
                  
                }
            }
        }

        public static void ShowAllItemsThatAreNotExpired(ClientContext ctx)
        {
            List spList = ctx.Web.Lists.GetByTitle("Products");

            ctx.Load(spList);
            ctx.ExecuteQuery();

            if (spList != null && spList.ItemCount > 0)
            {
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml =
                    @"<View>  
                        <Query> 
                            <Where><And><Geq><FieldRef Name='PROD_Expiry' /><Value Type='DateTime'><Today /></Value></Geq><Geq><FieldRef Name='PROD_Year' /><Value Type='Number'>2018</Value></Geq></And></Where> 
                        </Query> 
                         <ViewFields><FieldRef Name='Title' /><FieldRef Name='PROD_Year' /><FieldRef Name='PROD_Expiry' /></ViewFields> 
                    </View>";

                ListItemCollection listItems = spList.GetItems(camlQuery);
                ctx.Load(listItems);
                ctx.ExecuteQuery();

                foreach (var item in listItems)
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine(item["Title"].ToString());
                    Console.WriteLine(item["PROD_Expiry"].ToString());
                    Console.WriteLine(item["PROD_Year"].ToString());
                   
                }
            }
        }

        public static void GetItemsPriceBigger(ClientContext ctx)
        {

            List spList = ctx.Web.Lists.GetByTitle("Products");

            ctx.Load(spList);
            ctx.ExecuteQuery();

            if (spList != null && spList.ItemCount > 0)
            {
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml =
                   @"<View>  
                     <Query> 
                         <Where><Geq><FieldRef Name='PROD_Price' /><Value Type='Number'>30</Value></Geq></Where>
                         <OrderBy><FieldRef Name='PROD_Price' Ascending='FALSE' /></OrderBy> 
                     </Query> 
                        <ViewFields><FieldRef Name='Title' /><FieldRef Name='PROD_Type' /><FieldRef Name='PROD_Price' /></ViewFields> 
                    </View>";

                ListItemCollection listItems = spList.GetItems(camlQuery);
                ctx.Load(listItems);
                ctx.ExecuteQuery();

                foreach (var item in listItems)
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine(item["Title"].ToString());
                    Console.WriteLine(item["PROD_Type"].ToString());
                    Console.WriteLine(item["PROD_Price"].ToString());
                    
                }
            }

        }
    }
  }
