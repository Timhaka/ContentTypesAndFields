using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using OfficeDevPnP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentTypesAndFields.CodeExamples
{
    class ContentTypeFun
    {

        public static void MyFirstContentType(ClientContext ctx)
        {
            Web rootWeb = ctx.Site.RootWeb;

            //string carsID = "0x01001703716880F147E38BD026AA8DDD14D5";

            //if (rootWeb.ContentTypeExistsById(carsID))
            //{
            //    rootWeb.DeleteContentTypeById(carsID);
            //}

            //rootWeb.CreateContentType("Cars", carsID, "Tims Columns");


            //FieldCreationInformation brandField = new FieldCreationInformation(FieldType.Text);
            //brandField.DisplayName = "Brand";
            //brandField.Id = new Guid("{F4D37EFA-3CD5-4426-8F49-D2F48F841653}");
            //brandField.InternalName = "CMS_Brand";
            //brandField.Group = "Tims Columns";

            //if (rootWeb.FieldExistsById(brandField.Id))
            //{
            //    rootWeb.RemoveFieldById(brandField.Id.ToString());
            //}


            //rootWeb.CreateField(brandField);

            //rootWeb.AddFieldToContentTypeById(carsID, "{F4D37EFA-3CD5-4426-8F49-D2F48F841653}", false);

            //FieldCreationInformation yearField = new FieldCreationInformation(FieldType.Number);
            //yearField.DisplayName = "Year";
            //yearField.Id = new Guid("{F1CE6B6C-652B-470B-8299-5081E16A8CB2}");
            //yearField.InternalName = "CMS_Year";
            //yearField.Group = "Tims Columns";

            //if (rootWeb.FieldExistsById(yearField.Id))
            //{
            //    rootWeb.RemoveFieldById(yearField.Id.ToString());
            //}

            //rootWeb.CreateField(yearField);

            //rootWeb.AddFieldToContentTypeById(carsID, "{F1CE6B6C-652B-470B-8299-5081E16A8CB2}", false);



            //FieldCreationInformation colorField = new FieldCreationInformation(FieldType.Choice);
            //colorField.DisplayName = "Color";
            //colorField.Id = new Guid("{9EC39186-A1E6-4DA3-8343-84A7C7137714}");
            //colorField.InternalName = "CMS_Color";
            //colorField.Group = "Tims Columns";

            //if (rootWeb.FieldExistsById(colorField.Id))
            //{
            //    rootWeb.RemoveFieldById(colorField.Id.ToString());

            //}



            //var fc = rootWeb.CreateField<FieldChoice>(colorField);

            //fc.Choices = new string[] { "red", "green", "blue" };

            ////rootWeb.CreateField(colorField);
            //fc.Update();
            //ctx.ExecuteQuery();

            //rootWeb.AddFieldToContentTypeById(carsID, "{9EC39186-A1E6-4DA3-8343-84A7C7137714}", false);


            //List list = rootWeb.GetListByTitle("Tims List From Pnp");
            //list.AddContentTypeToList(rootWeb.GetContentTypeById(carsID), true);

            //for (int i = 0; i < 5; i++)
            //{
            //    list.CreateDocument("dokument " + i, list.RootFolder, DocumentTemplateType.Word);
            //    Console.WriteLine("done"+i);
            //}
            //list.DefaultView.ViewFields.Add("CMS_Brand");
            //list.DefaultView.ViewFields.Add("CMS_Year");
            //list.DefaultView.ViewFields.Add("CMS_Color");
            //list.DefaultView.Update();

            //list.Update();
            //ctx.ExecuteQuery();


            //this is another way to do it using xml
            //rootWeb.Fields.AddFieldAsXml("<field DisplayName = "brand"></fields>)

            string booksID = "0x01003DCFC9B08E1E4DD18AB8BD9053D7F49E";

            if (rootWeb.ContentTypeExistsById(booksID))
            {
                rootWeb.DeleteContentTypeById(booksID);
            }

            rootWeb.CreateContentType("Books", booksID, "Tims Columns");

            

            FieldCreationInformation bookType = new FieldCreationInformation(FieldType.Choice);
            bookType.DisplayName = "Book Type";
            bookType.Id = new Guid("{F0C6D85D-C4DD-48F2-806E-5794ABDCAAB0}");
            bookType.InternalName = "CMS_BookType";
            bookType.Group = "Tims Columns";

            var fcs = rootWeb.CreateField<FieldChoice>(bookType);

            fcs.Choices = new string[] { "Large", "Medium", "Small" };

            fcs.Update();
            ctx.ExecuteQuery();


            FieldCreationInformation author = new FieldCreationInformation(FieldType.Text);
            author.DisplayName = "Author";
            author.Id = new Guid("{A95D3D0B-F076-4675-A99A-7D8EED002481}");
            author.InternalName = "CMS_Author";
            author.Group = "Tims Columns";

            FieldCreationInformation dateReleased = new FieldCreationInformation(FieldType.DateTime);
            dateReleased.DisplayName = "Date released";
            dateReleased.Id = new Guid("{7C81173A-141C-4008-B2E6-A8763F0850DF}");
            dateReleased.InternalName = "CMS_dateReleased";
            dateReleased.Group = "Tims Columns";

            FieldCreationInformation description = new FieldCreationInformation(FieldType.Note);
            description.DisplayName = "Description";
            description.Id = new Guid("{B65E6297-0182-4CA8-A7DE-45E05FE61086}");
            description.InternalName = "CMS_description";
            description.Group = "Tims Columns";


            //rootWeb.CreateField(bookType);
            rootWeb.CreateField(author);
            rootWeb.CreateField(dateReleased);
            rootWeb.CreateField(description);
            rootWeb.AddFieldToContentTypeById(booksID, "{F0C6D85D-C4DD-48F2-806E-5794ABDCAAB0}", false);
            rootWeb.AddFieldToContentTypeById(booksID, "{A95D3D0B-F076-4675-A99A-7D8EED002481}", false);
            rootWeb.AddFieldToContentTypeById(booksID, "{7C81173A-141C-4008-B2E6-A8763F0850DF}", false);
            rootWeb.AddFieldToContentTypeById(booksID, "{B65E6297-0182-4CA8-A7DE-45E05FE61086}", false);

            List list = rootWeb.CreateList(ListTemplateType.GenericList, "books", false, true, "TimsGenericList", true);

            list.AddContentTypeToListById(booksID, true);



           

        }
    }
}
