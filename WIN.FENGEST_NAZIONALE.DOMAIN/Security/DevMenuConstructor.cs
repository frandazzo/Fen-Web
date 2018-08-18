using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.MENU_CUSTOMIZER;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Security
{
    public class DevMenuConstructor : IMenuWidgetConstructor
    {
        private DevExpress.Web.ASPxMenu _menu;
        private RepresentationHandler h;

        public DevMenuConstructor(DevExpress.Web.ASPxMenu menu)
        {
            if (menu == null)
                throw new ArgumentException("Il widget Mennu non può essere nullo");

            _menu = menu;
        }



        public void ConstructMenu(MenuRepresentation representation)
        {

            _menu.Items.Clear();

            h = new RepresentationHandler(representation);


            IList<Node> nodes = h.GetNodes(0, "");

            foreach (Node elem in nodes)
            {
                DevExpress.Web.MenuItem i = new DevExpress.Web.MenuItem(elem.Text, "", elem.UrlImage , elem.Url);
                i.DataItem = elem.Child;
                _menu.Items.Add(i);
            }



            //inserisco gli item degli altri livelli
            foreach (DevExpress.Web.MenuItem elem in _menu.Items)
            {
                AddChild(elem, 1, elem.DataItem.ToString());
            }

        }




        private void AddChild(DevExpress.Web.MenuItem item, int level, string parent)
        {
            IList<Node> nodes = h.GetNodes(level, parent);

            if (nodes.Count == 0)
                return;

            //inserisco gli item a livello 0;
            foreach (Node elem in nodes)
            {
                DevExpress.Web.MenuItem i = new DevExpress.Web.MenuItem(elem.Text, "", elem.UrlImage, elem.Url);
                i.DataItem = elem.Child;
                item.Items.Add(i);
            }



            ////inserisco gli item degli altri livelli
            foreach (DevExpress.Web.MenuItem elem in item.Items)
            {
                AddChild(elem, level + 1, elem.DataItem.ToString());
            }
        }


    }
}
