using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.MENU_CUSTOMIZER;
using System.Web.UI.WebControls;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Security
{
    public class MenuConstructor : IMenuWidgetConstructor
    {
        private System.Web.UI.WebControls.Menu _menu;
        private RepresentationHandler h;

        public MenuConstructor(System.Web.UI.WebControls.Menu menu)
        {
            if (menu == null)
                throw new ArgumentException("Il widget Mennu non può essere nullo");

            _menu = menu;
        }


        #region IMenuWidgetConstructor Membri di

        public void ConstructMenu(MenuRepresentation representation)
        {

            _menu.Items.Clear();

            h = new RepresentationHandler(representation);


            IList<Node> nodes = h.GetNodes(0, "");

            foreach (Node elem in nodes)
            {
                MenuItem i = new MenuItem(elem.Text, "", "", elem.Url);
                i.Value = elem.Child;
                _menu.Items.Add(i);
            }



            ////inserisco gli item degli altri livelli
            foreach (MenuItem elem in _menu.Items)
            {
                AddChild(elem,  1, elem.Value.ToString());
            }
            
        }




        private void AddChild(MenuItem item, int level, string parent)
        {
            IList<Node> nodes = h.GetNodes(level, parent);

            if (nodes.Count == 0)
                return;

            //inserisco gli item a livello 0;
            foreach (Node elem in nodes)
            {
                MenuItem i = new MenuItem(elem.Text, "", "", elem.Url);
                i.Value = elem.Child;
                item.ChildItems.Add(i);
            }



            ////inserisco gli item degli altri livelli
            foreach (MenuItem elem in item.ChildItems)
            {
                AddChild(elem, level + 1, elem.Value.ToString());
            }
        }


        #endregion
    }
}
