using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentTypesAndFields.CodeExamples
{
    public class ContentTypeAssignemnt
    {
        public static void CreateBookCT(ClientContext ctx)
        {

            string bookCT = "0x01000E870749A9444905BB8A362E475B0798";

            Web web = ctx.Site.RootWeb;
            
            //web.GetListByTitle("Books2").DeleteObject();
            //ctx.ExecuteQuery();
            //web.DeleteContentTypeById(bookCT);

            if (!web.ContentTypeExistsById(bookCT))
            {
                web.CreateContentType("David Books", bookCT, "Davids ContentType");

            }

            string bookTypeFieldId = "{DBB24705-0DEA-4C4F-8C2A-95CB6F0DE25E}";

            if (!web.FieldExistsById(new Guid(bookTypeFieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.Choice);
                info.Id = bookTypeFieldId.ToGuid();
                info.InternalName = "DAV_BookType";
                info.DisplayName = "Book Type";
                info.Group = "Tims Columns";


                FieldChoice field = web.CreateField<FieldChoice>(info);
                field.Choices = new string[] { "Romance", "Drama", "Horror", "Thriller" };
                field.Update();
                ctx.ExecuteQuery();
            }


            string authorFieldId = "{D6996667-0BEA-4C9F-9904-DEB21CC5AA84}";

            if (!web.FieldExistsById(new Guid(authorFieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.Text);
                info.Id = authorFieldId.ToGuid();
                info.InternalName = "DAV_Author";
                info.DisplayName = "Author";
                info.Group = "Tims Columns";


                Field field = web.CreateField(info);

            }

            string releaseDateFieldId = "{84716863-06CA-4D31-BAA0-7D099FC501E7}";

            if (!web.FieldExistsById(new Guid(releaseDateFieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.DateTime);
                info.Id = releaseDateFieldId.ToGuid();
                info.InternalName = "DAV_Realesedate";
                info.DisplayName = "ReleaseDate";
                info.Group = "Tims Columns";


                FieldDateTime field = web.CreateField<FieldDateTime>(info);
                field.DisplayFormat = DateTimeFieldFormatType.DateOnly;
                field.Update();
                ctx.ExecuteQuery();
            }


            string descriptionDateFieldId = "{4BD3F599-4D5C-412D-8431-6ECD36AEB015}";
           // web.RemoveFieldById(descriptionDateFieldId);

            if (!web.FieldExistsById(new Guid(descriptionDateFieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.Note);
                info.Id = descriptionDateFieldId.ToGuid();
                info.InternalName = "DAV_description";
                info.DisplayName = "Description";
                info.Group = "Tims Columns";
                info.Required = true;
                FieldMultiLineText field = web.CreateField<FieldMultiLineText>(info);

                field.RichText = true;
                field.NumberOfLines = 10;
                field.AllowHyperlink = true;
                field.Update();
                ctx.ExecuteQuery();

            }

            web.AddFieldToContentTypeById(bookCT, bookTypeFieldId);
            web.AddFieldToContentTypeById(bookCT, authorFieldId);
            web.AddFieldToContentTypeById(bookCT, releaseDateFieldId);
            web.AddFieldToContentTypeById(bookCT, descriptionDateFieldId, true);


            if (!web.ListExists("Books2"))
            {
                List list = web.CreateList(ListTemplateType.GenericList, "Books2", false, urlPath: "lists/books2", enableContentTypes: true);
                list.AddContentTypeToListById(bookCT, true);

                View listView = list.DefaultView;
                listView.ViewFields.Add("DAV_BookType");
                listView.ViewFields.Add("DAV_Author");
                listView.ViewFields.Add("DAV_Realesedate");
                listView.ViewFields.Add("DAV_description");
                listView.Update();
                ctx.ExecuteQueryRetry();
            }

            List bookList = web.GetListByTitle("Books2");

           ListItem item = bookList.AddItem(new ListItemCreationInformation());

            item["Title"] = "MistBorn";
            item["DAV_BookType"] = "Fantasy";
            item["DAV_Author"] = "Brandon Sanderson";
            item["DAV_Realesedate"] = DateTime.Parse("2001-02-12");
            item["DAV_description"] = "This is a decription \n\n is this a new line?";

            item.Update();
            ctx.ExecuteQuery();


            //ListItemCollection items = bookList.GetItems(CamlQuery.CreateAllItemsQuery());
            //ctx.Load(items);
            //ctx.ExecuteQuery();
            




        }


    }
}
