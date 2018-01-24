using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static void CreateCV(ClientContext ctx)
        {

            string cVCT = "0x010100A959F697950047DF80D85119D99F8CA7";

            Web web = ctx.Site.RootWeb;

            if (!web.ContentTypeExistsById(cVCT))
            {
                web.CreateContentType("CV", cVCT, "Davids ContentType");

            }


            string picFieldId = "{98A1C95C-AA0F-4D2C-92C8-5407594C440F}";

            if (!web.FieldExistsById(new Guid(picFieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.URL);
                info.Id = picFieldId.ToGuid();
                info.InternalName = "DAV_Pic";
                info.DisplayName = "Picture";
                info.Group = "Tims Columns";

                FieldUrl picfield = web.CreateField<FieldUrl>(info);
                picfield.DisplayFormat = UrlFieldFormatType.Image;
                picfield.Update();
                ctx.ExecuteQuery();

            }

            string userFieldId = "{B0C1EFC4-189E-4626-A1DC-1CCC4693C097}";

            if (!web.FieldExistsById(new Guid(userFieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.User);
                info.Id = userFieldId.ToGuid();
                info.InternalName = "DAV_User";
                info.DisplayName = "User";
                info.Group = "Tims Columns";
                FieldUser userfield = web.CreateField<FieldUser>(info);
                ctx.ExecuteQuery();

            }

            string activeFieldId = "{2CB24A28-3F5B-49AE-9F54-5FD8747DBF19}";

            if (!web.FieldExistsById(new Guid(activeFieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.Boolean);
                info.Id = activeFieldId.ToGuid();
                info.InternalName = "DAV_Active";
                info.DisplayName = "Active";
                info.Group = "Tims Columns";
                web.CreateField(info);

            }

            web.AddFieldToContentTypeById(cVCT, picFieldId);
            web.AddFieldToContentTypeById(cVCT, userFieldId);
            web.AddFieldToContentTypeById(cVCT, activeFieldId);


            if (!web.ListExists("CVs"))
            {
                List list = web.CreateList(ListTemplateType.DocumentLibrary, "CVs", true, enableContentTypes: true);
                list.AddContentTypeToListById(cVCT);
            }

            List CVList = web.GetListByTitle("CVs");

            FileCreationInformation fileinfo = new FileCreationInformation();
            System.IO.FileStream fileStream = System.IO.File.OpenRead(@"C:\Users\timha\source\repos\Officedeveloper1\ContentTypesAndFields\ContentTypesAndFields\TextFile1.txt");
            fileinfo.Content = ReadFully(fileStream);
            fileinfo.Url = "file1.txt";
            Microsoft.SharePoint.Client.File files = CVList.RootFolder.Files.Add(fileinfo);
            ctx.ExecuteQuery();

            User user = web.EnsureUser("Tim@folkis2017.onmicrosoft.com");
            ctx.Load(user);
            ctx.ExecuteQuery();


            ListItem item = files.ListItemAllFields;
            
            item["Title"] = "Tim";
            item["ContentTypeId"] = cVCT;

            FieldUrlValue picvalue = new FieldUrlValue();
            picvalue.Description = "Tim";
            picvalue.Url = "https://images.pexels.com/photos/104827/cat-pet-animal-domestic-104827.jpeg?w=940&h=650&auto=compress&cs=tinysrgb";
            item["DAV_Pic"] = picvalue;

            item["DAV_User"] = user.Id;
            item["DAV_Active"] = true;
    

            item.Update();
            ctx.ExecuteQuery();



        }

        public static void RenameTitleFieldonCV(ClientContext ctx)
        {
            string cVCT = "0x010100A959F697950047DF80D85119D99F8CA7";

            

            List list = ctx.Web.GetListByTitle("CVs");

            ContentType ct = list.GetContentTypeByName("CV");
            ctx.Load(ct.FieldLinks, flinks => flinks.Include(flink => flink.Name, flink => flink.DisplayName));
            ctx.ExecuteQuery();

            

            foreach (var fl in ct.FieldLinks)
            {
                //ctx.Load(fl);
                //ctx.ExecuteQuery();
                Console.WriteLine(fl.Name);
                Console.WriteLine(fl.DisplayName);
                


                if (fl.Name == "Title")
                {
                    fl.DisplayName = "CV Description";
                    ct.Update(false);
                    ctx.ExecuteQuery();
                       
                }


            }

            Console.ReadLine();
            
        }

        public static byte[] ReadFully(System.IO.Stream input)
        {
            byte[] buffer = new byte[input.Length];
            //byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

    }
}
