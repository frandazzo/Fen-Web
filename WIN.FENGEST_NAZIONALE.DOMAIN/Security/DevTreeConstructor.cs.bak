using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.MENU_CUSTOMIZER;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Security
{
    public class DevTreeConstructor:IMenuWidgetConstructor
    {
        private DevExpress.Web.ASPxTreeView.ASPxTreeView _menu;
        private RepresentationHandler h;

        public DevTreeConstructor(DevExpress.Web.ASPxTreeView.ASPxTreeView menu)
        {
            if (menu == null)
                throw new ArgumentException("Il widget TreeView non può essere nullo");

            _menu = menu;
        }



        public void ConstructMenu(MenuRepresentation representation)
        {

            _menu.Nodes.Clear();

            h = new RepresentationHandler(representation);


            IList<Node> nodes = h.GetNodes(0, "");

            foreach (Node elem in nodes)
            {
                DevExpress.Web.ASPxTreeView.TreeViewNode i = new DevExpress.Web.ASPxTreeView.TreeViewNode(elem.Text, "",elem.UrlImage, elem.Url);
                i.DataItem = elem.Child;
                _menu.Nodes.Add(i);
            }



            //inserisco gli item degli altri livelli
            foreach (DevExpress.Web.ASPxTreeView.TreeViewNode elem in _menu.Nodes)
            {
                AddChild(elem, 1, elem.DataItem.ToString());
            }

        }




        private void AddChild(DevExpress.Web.ASPxTreeView.TreeViewNode item, int level, string parent)
        {
            IList<Node> nodes = h.GetNodes(level, parent);

            if (nodes.Count == 0)
                return;

            //inserisco gli item a livello 0;
            foreach (Node elem in nodes)
            {
                DevExpress.Web.ASPxTreeView.TreeViewNode i = new DevExpress.Web.ASPxTreeView.TreeViewNode(elem.Text, "", elem.UrlImage , elem.Url);
                i.DataItem = elem.Child;
                item.Nodes.Add(i);
            }



            ////inserisco gli item degli altri livelli
            foreach (DevExpress.Web.ASPxTreeView.TreeViewNode elem in item.Nodes)
            {
                AddChild(elem, level + 1, elem.DataItem.ToString());
            }
        }


    }

}
